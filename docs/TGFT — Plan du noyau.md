# TGFT — Plan du noyau

Oct 26, 2026 · @Cédric

## 1. Ordre de réalisation

On part du schéma de la base, puis on construit les fonctionnalités dessus. Validé le 2026-10-26.

1. Intégration continue (GitHub Actions), puis schéma de la base de données du noyau.
2. Utilisateurs (connexion Discord reprise de la V0, pseudo et avatar), groupes, création de groupe, rôles et permissions.
3. Ce qu'est un module : manifeste, catalogue.
4. Activation des modules par groupe. Les modules de base sont activés par défaut mais peuvent être désactivés.

Modules de base et modules externes suivent le même principe et les mêmes contrats. Seule l'exécution diffère : un module de base est écrit en C#, compilé avec la plateforme, et ne passe pas par le bac à sable.

L'isolation entre groupes et ses tests arrivent dans la même étape que la première donnée appartenant à un groupe, jamais après.

## 2. Schéma : utilisateurs et groupes

Cinq tables, validées dans leur principe le 2026-10-26. Les colonnes exactes seront fixées à l'écriture des migrations.

| Table | Contenu | Remarque |
| --- | --- | --- |
| utilisateurs | identifiant, identifiant Discord (unique), pseudo, avatar, date d'inscription | Pas liée à un groupe. Les jetons Discord ne sont pas conservés : ils ne servent qu'à la connexion. |
| groupes | identifiant, nom, visibilité (public ouvert, public sur candidature, privé), date de création |  |
| adhésions | groupe, utilisateur, rôle, date d'entrée | Une ligne par utilisateur et par groupe. Rôle parmi les quatre rôles fixes. |
| invitations | groupe, empreinte du jeton, créée par, date d'expiration, révoquée | Seule l'empreinte du jeton est stockée, comme pour un mot de passe. |
| demandes d'adhésion | groupe, utilisateur, statut (en attente, acceptée, refusée), date | Pour les groupes publics sur candidature. |

Règles garanties par la base elle-même :

- **Un groupe a exactement un fondateur.** Un index unique sur le groupe, restreint aux adhésions de rôle fondateur, interdit d'en avoir deux. Le groupe et son fondateur sont créés en une seule opération, ce qui interdit d'en avoir zéro.
- **Une seule adhésion par utilisateur et par groupe.** La clé de la table est le couple groupe et utilisateur.

Le fondateur n'est pas une colonne de la table des groupes : c'est une adhésion avec le rôle fondateur. Transférer la propriété revient à changer deux rôles.

La connexion ne donne aucun droit. Contrairement à la V0, le premier utilisateur connecté ne devient pas administrateur de l'instance : on devient fondateur en créant un groupe, membre en y entrant.

## 3. Accès des modules aux données de la plateforme

Un module externe a deux sources de données : l'API publique de la plateforme et son propre classeur SQLite. Il ne recopie jamais une donnée qui existe déjà sur la plateforme : il la lit, et il enregistre dans son classeur seulement ce qu'il ajoute. Validé le 2026-10-26.

| Données | Accès du module externe | Exemple |
| --- | --- | --- |
| Gestion des utilisateurs et des groupes (rôles, invitations, réglages) | Aucun | Un module ne voit ni qui est administrateur, ni les liens d'invitation. |
| Liste des membres du groupe | Lecture seule, en pseudonymes | Un module Tournoi voit les membres pour les inscrire. |
| Contenu du groupe (compos, tier lists…) | Lecture et écriture, groupe en cours uniquement | Un autre module de tier list affiche et modifie la vraie tier list du groupe, sans copie. |
| Son propre classeur | Tout | Les données que la plateforme ne prévoit pas. |

### Comment l'« API publique » fonctionne pour un module externe

Le module n'appelle jamais l'API pendant qu'il tourne : il n'a pas de réseau, et c'est une décision déjà tranchée. Il déclare dans son manifeste les données qu'il lit et modifie. La plateforme met les données lues dans son paquet, et la réponse contient les modifications demandées. Le kit de développement présente cela comme une API (`plateforme.tierList("compos")`), ce qui le rend invisible pour le développeur.

### Principe de sécurité : un module n'a pas de porte à lui

Une modification demandée par un module externe passe par exactement le même cas d'usage qu'une modification faite depuis le site, avec les mêmes vérifications. Un module ne peut rien faire qu'un membre ne pourrait faire lui-même avec ses propres droits.

- **Le groupe est fixé par la plateforme**, jamais par le module.
- **Le membre agit avec ses propres droits** : si un lecteur clique, la modification est refusée, module ou pas.
- **Le module n'a que les droits qu'il a déclarés** et que l'administrateur a vus avant de l'activer.
- **Seuil plancher fixé par la plateforme.** Pour chaque action sur ses données, la plateforme fixe un rôle minimum (par exemple, contributeur ou plus pour modifier une tier list). Un module peut exiger davantage, jamais moins. L'API applique le plancher et refuse toute tentative de le contourner. Le module doit aussi masquer l'action dans son interface pour le confort de l'utilisateur, mais c'est l'API qui fait foi.
- Un seuil relevé par un module ne vaut que dans ce module : depuis le site ou un autre module, c'est le plancher de la plateforme qui s'applique.
- Les noms des contributeurs sont remplacés par les pseudonymes propres au module, et seules les données du groupe en cours figurent dans le paquet.
- Le module enregistre des références (« la compo numéro 12 »), pas des copies. Une référence vers une donnée supprimée est à gérer par le module.

**Changement par rapport au cahier des charges :** la couche 3 disait qu'un module ne modifie que son propre classeur. Elle devient : un module modifie son classeur, plus le contenu du groupe pour lequel il a reçu le droit, dans la limite des droits du membre.

## 4. Schéma : données du jeu et contenu des groupes

On reprend les tables de la V0, adaptées au multi-groupe. La première version ne contient que le minimum pour vérifier que la chaîne fonctionne ; la fiche de compo complète (priorité 2) viendra par des migrations ultérieures. Validé le 2026-10-26.

### Données du jeu (communes à tous les groupes)

Rangées par set, en lecture seule pour tout le monde, remplies par la synchronisation quotidienne. Source : Data Dragon, avec un point de contrôle (voir l'annexe).

| Table | Contenu |
| --- | --- |
| versions du jeu | un set : numéro, nom, version courante ou non |
| unités | identifiant Riot (`TFT17_Xayah`), nom, coût, sort, icône |
| traits | identifiant Riot, nom, description, icône |
| unités ↔ traits | quels traits porte chaque unité |
| seuils des traits | nombre d'unités par palier, couleur du palier |
| objets | identifiant Riot, nom, catégorie (composant, composable, emblème, forgé…), icône |
| augments | identifiant Riot, nom, rareté, texte de l'effet, icône |

### Contenu du groupe (première version)

| Table | Contenu |
| --- | --- |
| compos | groupe, set, nom, unité-icône, tier, ordre, auteur, dates |
| classement des augments | groupe, set, augment, tier, ordre, note |
| classement des objets | groupe, set, objet, tier, ordre, note. Les onglets composables, emblèmes et forgés filtrent cette même table par catégorie d'objet. |

La tier list des compos n'est pas une table à part : c'est le tier de chaque compo. Une donnée vit à un seul endroit.

### Règles

- **L'identifiant de groupe figure sur chaque table de contenu**, y compris les futures tables enfants (unités d'une compo…). La base vérifie qu'un enfant appartient au même groupe que son parent. La protection de la base se réduit à une seule règle, identique partout : « cette ligne appartient-elle au groupe en cours ? ».
- **L'auteur passe à vide quand son compte est supprimé** : la contribution reste et n'est plus rattachée à personne (anonymisation).
- **Tout le contenu est rattaché à un set** : une tier list vaut pour un set donné.
- Fiche de compo complète de la V0 (unités et positions, objets, augments, tier lists internes, tags, conditions, early game) : priorité 2, ajoutée plus tard sous forme de tables enfants, sans modifier l'existant.

## 5. Isolation entre groupes

**Le groupe en cours est dans l'adresse** : `/api/groups/{id}/…`. Validé le 2026-10-26. Le groupe donné par le navigateur n'est qu'une demande, jamais une preuve.

Chaîne d'une requête (détails techniques tranchés par Claude, à relire) :

1. **Vérification d'appartenance.** Avant tout le reste, l'API vérifie que l'utilisateur connecté est membre du groupe de l'adresse et lit son rôle. Sinon : refus, avec la même réponse que pour un groupe inexistant, pour ne pas révéler qu'il existe.
2. **Contexte du groupe.** Le groupe et le rôle vérifiés sont rangés dans un objet « groupe en cours », valable pour cette seule requête. Tout le reste du code le lit là, jamais dans l'adresse.
3. **Premier rempart : filtres globaux EF Core.** Chaque table de contenu filtre automatiquement sur le groupe en cours, y compris dans les requêtes écrites plus tard.
4. **Second rempart : Row Level Security.** Au début de chaque transaction, l'API transmet le groupe en cours à PostgreSQL (variable de session limitée à la transaction). Chaque table de contenu a une règle : une ligne n'est visible et modifiable que si son groupe est celui de la variable. Si un filtre EF est oublié, la base refuse quand même.
5. **Rôles PostgreSQL séparés.** Les migrations tournent avec le rôle propriétaire des tables ; l'application se connecte avec un autre rôle, sans droit de modifier le schéma. La protection est en plus forcée sur le propriétaire, qui sinon y échapperait.

Cas particuliers, qui ne dépendent pas d'un groupe en cours :

- **Mes groupes** (basculer d'un groupe à l'autre) : l'utilisateur lit ses propres adhésions, et seulement les siennes.
- **Groupes publics** (parcourir et demander à rejoindre) : on lit le nom et la visibilité des groupes publics, jamais leur contenu.
- **Données du jeu** : communes, en lecture seule, sans filtre de groupe.

## 6. Authentification

**Cookie de session pour le site, clé de service pour le bot.** Validé le 2026-10-26.

- **Site** : après la connexion Discord (parcours repris de la V0), le serveur dépose un cookie illisible par le JavaScript. Une exclusion ou une suppression de compte prend effet immédiatement en supprimant la session côté serveur. Protection contre les requêtes forgées depuis un autre site (CSRF) activée.
- **Bot Discord** : il n'utilise jamais la session d'un membre. Il appelle l'API avec une clé de service, sur le réseau interne de Docker, en indiquant l'identifiant Discord du membre qui a tapé la commande. L'API retrouve l'utilisateur et applique les mêmes vérifications que pour le site.
- **Le rôle n'est jamais stocké dans la session** : il est relu à chaque requête, donc un changement de rôle s'applique tout de suite.
- **Plus tard, si besoin** : un jeton dédié pour des outils extérieurs agissant au nom d'un membre, ajouté à côté du cookie sans le remplacer.

## 7. Permissions

**Grille fixe, identique pour tous les groupes.** Validé le 2026-10-26. Une grille personnalisable par groupe est une évolution possible, seulement une fois tout le reste terminé.

- Les quatre rôles (fondateur, administrateur, contributeur, lecteur) et leurs droits sont écrits dans le code, conformément à la matrice du cahier des charges.
- Les droits sont des permissions nommées (par exemple « modifier le contenu », « gérer les membres », « gérer les modules », « supprimer le groupe »). Chaque point d'accès de l'API déclare la permission qu'il exige.
- **Test d'exhaustivité** : il parcourt tous les points d'accès et échoue si l'un d'eux ne déclare aucune permission. Un oubli devient impossible.
- **Test de la matrice** : pour chaque rôle, il vérifie qu'il obtient exactement ce que la grille prévoit, et notamment que le contributeur n'a aucun droit sur les membres ni sur les modules.
- Les seuils plancher des modules (section 3) s'appuient sur cette grille : un module peut exiger un rôle plus élevé, jamais plus bas.

## 8. Migrations et répartition dans les couches

**Migrations EF Core.** Validé le 2026-10-26. Le modèle C# est la seule description du schéma ; les tests d'isolation appliquent ces mêmes migrations à la base jetable de Testcontainers.

Détails tranchés par Claude (à relire) :

- Ce qu'EF Core ne sait pas décrire est écrit en SQL à la main, à l'intérieur des migrations : règles de Row Level Security, rôles PostgreSQL, protection forcée sur le propriétaire.
- **Les migrations ne sont pas appliquées par l'API au démarrage.** Une étape séparée les applique avec le rôle propriétaire. L'API ne détient jamais les identifiants de ce rôle, seulement ceux du rôle applicatif.

| Projet | Contenu pour le noyau |
| --- | --- |
| Domain | Entités : utilisateur, groupe, adhésion, invitation, demande d'adhésion, données du jeu, compo, classements. Les rôles, les permissions et la grille qui les relie. Les règles : un seul fondateur, un groupe naît avec son fondateur. |
| Application | Cas d'usage (se connecter avec Discord, créer un groupe, changer un rôle, exclure, inviter, accepter une invitation, demander à rejoindre…). Ports : dépôts, groupe en cours, utilisateur connecté, client OAuth Discord, source des données du jeu. |
| Infrastructure | Contexte EF Core et ses filtres de groupe, configurations des tables, migrations, dépôts, transmission du groupe en cours à PostgreSQL pour la Row Level Security, client OAuth Discord repris de la V0, synchronisation Data Dragon. |
| Api | Points d'accès, cookie de session, vérification d'appartenance au groupe de l'adresse, vérification des permissions, authentification de service du bot. |

## 9. Dépendances

Liste validée le 2026-10-26. Versions choisies à l'installation, avec la vérification des paquets vulnérables.

| Paquet | Projet | Usage |
| --- | --- | --- |
| Npgsql.EntityFrameworkCore.PostgreSQL | Infrastructure | EF Core et son pilote PostgreSQL |
| Microsoft.EntityFrameworkCore.Design | Api, développement seulement | Générer les migrations, non embarqué dans l'application déployée |
| Testcontainers.PostgreSql | Tests | Base PostgreSQL jetable pour les tests d'isolation |
| Microsoft.AspNetCore.Mvc.Testing | Tests | API démarrée en mémoire pour les tests d'autorisation |

Rien à installer pour le cookie de session et la protection CSRF (intégrés à ASP.NET Core), ni pour la connexion Discord (client de la V0 repris). Dapper et Discord.Net attendent d'être utiles. Domain reste sans aucune dépendance.

## 10. Tests

Tous bloquants en intégration continue, au même titre que la compilation. Détails tranchés par Claude (à relire) :

| Famille | Outil | Ce qui est vérifié |
| --- | --- | --- |
| Domaine | xUnit | Un seul fondateur par groupe, un groupe naît avec son fondateur, transfert de propriété, grille des permissions |
| Isolation par l'API | Testcontainers + API en mémoire | Deux groupes A et B : un membre de A ne lit ni ne modifie rien de B, sur chaque point d'accès |
| Isolation par la base | Testcontainers | Filtres EF désactivés exprès, groupe en cours = A : aucune ligne de B n'est visible ni modifiable. Sans groupe en cours : rien n'est visible. Le rôle applicatif ne peut ni modifier ni supprimer une règle de protection. |
| Autorisations | API en mémoire + connexion de test | Exhaustivité (chaque point d'accès déclare une permission) et matrice (chaque rôle obtient exactement ce que la grille prévoit) |
| Architecture | NetArchTest (existant) | Les cinq règles actuelles, complétées au besoin |

Le test « isolation par la base » est celui qui prouve que les deux remparts sont indépendants : il retire volontairement le premier pour montrer que le second tient seul.

## 11. Données personnelles dans les modules

Le contrat de cycle de vie des données est fixé avant le premier module. Validé le 2026-10-26.

|  | Purger un groupe | Anonymiser un utilisateur | Exporter les données d'un utilisateur |
| --- | --- | --- | --- |
| Noyau | Suppression de toutes les lignes du groupe | L'auteur des contributions passe à vide | Profil, adhésions, contributions dont il est l'auteur |
| Modules de base | Obligatoire, vérifié à la compilation | Obligatoire, vérifié à la compilation | **Ajouté au contrat** : obligatoire, vérifié à la compilation |
| Modules externes | La plateforme supprime les classeurs du groupe | La plateforme supprime la correspondance entre l'utilisateur et ses pseudonymes | La plateforme extrait elle-même, de chaque classeur, les entrées contenant l'un des pseudonymes de l'utilisateur |

Pour les modules externes, rien n'est demandé au module et rien ne dépend de son honnêteté : c'est la plateforme qui écrit dans les classeurs et qui connaît les pseudonymes. Export au format JSON, lisible par une machine. Limite assumée, comme pour la purge : un texte libre où un membre en cite un autre par son nom n'est pas retrouvé.

## 12. Étapes

Validé le 2026-10-26. Chaque étape se termine par un commit, tous les tests au vert dans l'intégration continue. Chaque étape livre son back-end et ses écrans Angular ensemble : en méthode agile, chaque étape apporte une valeur visible à l'utilisateur (validé le 2026-10-26).

| # | L'utilisateur peut… | Back-end | Écrans | Tests ajoutés |
| --- | --- | --- | --- | --- |
| 0 | *(tâche technique)* | GitHub Actions compile et lance les tests à chaque envoi | — | Les tests d'architecture existants deviennent bloquants |
| 1 | se connecter avec Discord et voir son compte | Paquets, contexte EF Core, les deux rôles PostgreSQL, étape séparée qui applique les migrations, base jetable pour les tests ; table des utilisateurs, connexion Discord reprise de la V0, cookie de session, connexion de test | Accueil avec « Se connecter avec Discord » et mention « non approuvé par Riot » ; mon compte (pseudo, avatar, déconnexion) | Les migrations s'appliquent ; le rôle applicatif ne peut pas modifier le schéma ; connexion, déconnexion, session refusée après suppression |
| 2 | créer ses groupes et passer de l'un à l'autre | Groupes, adhésions, groupe dans l'adresse, vérification d'appartenance, grille des permissions, isolation | Mes groupes, sélecteur de groupe, création, réglages (nom, visibilité) | Un seul fondateur ; exhaustivité et matrice des permissions ; premiers tests d'isolation |
| 3 | gérer les membres de son groupe | Liste des membres, changement de rôle, exclusion | Membres du groupe | Contributeur et lecteur refusés ; un exclu perd l'accès immédiatement |
| 4 | faire entrer de nouveaux membres | Liens d'invitation, groupes publics, demandes d'adhésion | Générer un lien, page d'arrivée du lien, groupes publics, demandes à accepter ou refuser | Lien expiré ou révoqué refusé ; un groupe privé n'apparaît pas dans la liste publique |
| 5 | classer les compos, les augments et les objets | Données du jeu et synchronisation Data Dragon derrière son port ; compos avec leur tier, classements des augments et des objets | Tier lists des compos, des augments et des objets (composables, emblèmes, forgés), version simple | La synchronisation n'écrit que si la version a changé ; isolation par l'API et par la base, filtres EF désactivés exprès |
| 6 | exercer ses droits sur ses données | Export, suppression du compte avec anonymisation, suppression du groupe | Mon compte (exporter, supprimer), suppression dans les réglages du groupe, politique de confidentialité et conditions d'utilisation | Après suppression, plus aucune donnée personnelle ; contributions restées en place sans auteur |

L'isolation commence dès l'étape 2 et est retestée à chaque nouvelle table. Le système de modules (manifeste, catalogue, activation, seuils) aura son propre plan, une fois le noyau terminé.

## Annexe — Détails tranchés, changements à reporter, points à traiter

### Détails tranchés par Claude (à relire)

- Le noyau définit seulement la permission « gérer les modules ». L'action d'activer un module, et le test qui vérifie qu'un contributeur ne peut pas la faire, arrivent avec l'activation des modules (étape 4).
- Les tests automatiques utilisent une connexion réservée aux tests, puisqu'ils ne peuvent pas passer par Discord. Elle n'existe pas dans l'application déployée.
- Les jetons Discord ne sont pas conservés, et les jetons d'invitation sont stockés sous forme d'empreinte.

### Changements par rapport au cahier des charges, reportés dans Projet Complet.md (2026-10-26)

- ~~Les modules externes accèdent aux données de la plateforme selon trois niveaux, écriture comprise sur le contenu du groupe (section 3).~~ Reporté en §6.4 bis.
- ~~La couche 3 « réponse vérifiée » est étendue en conséquence.~~ Reporté en §6.4 et §14.

### Points à traiter plus tard dans ce plan

- **Données du jeu : Data Dragon uniquement.** Décidé le 2026-10-26, conformément au cahier des charges. Vérifié le même jour sur la version 16.21.1 : les fichiers TFT ne donnent ni les traits de chaque unité, ni les seuils des traits. **Point de contrôle avant de commencer le Team Builder** : revérifier Data Dragon. Si ces données manquent toujours, le calcul des synergies est impossible et il faudra trancher (CommunityDragon, saisie manuelle ou combinaison des deux). La synchronisation passe par un port de la couche Application, pour qu'un changement de source ne touche que l'Infrastructure.
- ~~Écrans Angular du noyau~~ : réglé en section 12, livrés avec chaque étape.
- ~~Export des données dans les modules~~ : réglé en section 11.
- ~~Rôles PostgreSQL~~ : réglé en section 5.
- ~~Intégration continue~~ : mise en place maintenant, en première étape du noyau (décidé le 2026-10-26).
