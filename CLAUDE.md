# CLAUDE.md — Projet TGFT

## Règle n°1 : ne jamais coder sans demande explicite

**Tu n'écris, ne modifies et ne supprimes aucun fichier de code de ta propre initiative.** C'est la règle la plus importante de ce fichier.

- Tu n'écris du code **que si je te le demande explicitement** (« écris », « code », « implémente », « corrige ce fichier », « crée le fichier X »).
- Une question (« comment je ferais X ? », « c'est quoi le mieux pour Y ? », « pourquoi ça plante ? ») n'est **pas** une demande de code. Réponds par une explication, éventuellement un court extrait d'illustration dans ta réponse, mais **sans toucher aux fichiers**.
- Quand tu repères un bug ou une amélioration, **signale-le et propose**, puis attends mon accord.
- Quand je te demande du code, limite-toi **exactement** au périmètre demandé : pas de refactoring annexe, pas de fichiers en plus, pas de dépendances ajoutées sans me le dire.
- Avant un changement qui touche plusieurs fichiers ou l'architecture, **présente d'abord un plan court** et attends ma validation.
- Tu peux librement lire les fichiers, chercher dans le code et lancer des commandes **en lecture seule** (build, tests, `git status`, `git diff`, `git log`) pour comprendre. Pas de `git commit`, `git push`, installation de paquet ou migration de base sans demande explicite.

Je suis étudiant et ce projet est évalué : je dois comprendre et maîtriser tout ce qui est écrit. Quand tu codes, explique brièvement tes choix.

## Langue

Réponds en **français**, de façon claire et concise. Le code, les noms de classes, de méthodes et de variables restent en **anglais**.

## Mémoire du projet

Trois sources te permettent de garder le projet en tête. Utilise-les **avant de répondre** à une question d'architecture ou de conception, et chaque fois que tu as un doute.

### `docs/Projet Complet.md`

Ce fichier décrit le projet en entier : objectifs, architecture, marketplace, user stories, stack et décisions. **Tu peux le relire à tout moment** pour te remémorer le projet, notamment en début de session, après une longue conversation, ou quand une demande touche une partie que tu n'as pas encore vue. En cas de contradiction avec ce `CLAUDE.md`, c'est **`docs/Projet Complet.md` qui fait foi**. Signale-moi la contradiction.

### `docs/TGFT — Plan du noyau.md`

Plan détaillé du noyau, validé le 2026-10-26 : schéma, isolation entre groupes, authentification, permissions, tests, étapes 0 à 6. **Relis-le avant de coder une étape du noyau.** Ses décisions sont reportées dans `Projet Complet.md`.

### Graphify

Le projet utilise **graphify** pour maintenir un graphe de connaissance du code et de la documentation. Ce graphe est stocké dans `graphify-out/`.

- **En début de session**, lis `graphify-out/GRAPH_REPORT.md` pour retrouver la structure du projet avant d'explorer les fichiers un par un.
- Pour comprendre où se trouve quelque chose ou comment deux éléments sont liés, interroge d'abord le graphe (`/graphify query "..."`, `/graphify path "A" "B"`, `/graphify explain "X"`), avant de parcourir tout le code.
- **Mets le graphe à jour une seule fois, juste avant le commit**, avec `/graphify . --update`, pour le code comme pour la documentation et la configuration. Jamais après chaque modification.
  - Le code (C#, TypeScript) est analysé par sa syntaxe, sans appel au LLM, mais chaque mise à jour consomme quand même environ 15 000 à 20 000 tokens de contexte dans la session (instructions du skill, commandes).
  - La documentation et la configuration (`.md`, `.pdf`, `.json`, `.yml`…) sont ré-extraites en entier par un LLM : `Projet Complet.md` seul coûte environ 100 000 tokens.
- Le graphe versionné dans git (`graphify-out/`) doit toujours correspondre au commit qui le contient.
- Si `graphify-out/` n'existe pas encore, propose-moi de lancer `/graphify .`.

Mettre à jour le graphe n'est pas écrire du code : tu peux le faire sans demander, et c'est la seule exception à la règle n°1.

## Le projet en bref

**TGFT** est une plateforme web destinée aux communautés organisées de joueurs de **Teamfight Tactics** (TFT, Riot Games) : serveurs Discord, associations, groupes d'entraînement. Chaque groupe y construit sa base de connaissance et ses outils de travail.

C'est mon **projet de fin de bachelier** (projet d'intégration). Le document de référence est le **cahier des charges V2**, complété par ses évolutions dans `docs/Cahier des charges — évolutions/` (V2.1…) : en cas de doute ou de contradiction, c'est lui qui fait foi. Demande-moi de te le fournir si tu en as besoin.

**L'intérêt évalué n'est pas les fonctionnalités, mais l'architecture** :
- un **noyau minimal** (comptes, groupes, membres, rôles, permissions) ;
- des **modules** activables par groupe ;
- une **marketplace** où n'importe quel développeur publie des modules contenant du vrai code, sans relecture ni redéploiement.

### Modèle d'exploitation

- **Instance unique**, hébergée par moi, **multi-tenant** : chaque groupe a ses données strictement isolées.
- Un utilisateur peut être dans plusieurs groupes, avec un rôle différent dans chacun.
- **Budget d'hébergement < 10 € par mois**, un seul serveur virtuel (x86). Pas de conteneur ni de base de données par groupe ou par module.
- Licence **AGPL-3.0**, contributions sous **DCO**.

### Rôles

Fondateur, Administrateur, Contributeur, Lecteur. Le contributeur produit du contenu mais n'a **aucun** droit sur les membres ni sur l'activation ou la configuration des modules.

- **Grille fixe** dans le code, identique pour tous les groupes. Permissions nommées ; **chaque point d'accès déclare la permission qu'il exige** (test d'exhaustivité + test de matrice).
- Le rôle est porté par l'**adhésion** (utilisateur + groupe + rôle). Le fondateur est une adhésion, pas une colonne du groupe ; un seul fondateur, garanti par la base.
- La connexion ne donne aucun droit. Le rôle n'est jamais stocké dans la session : il est relu à chaque requête.

### Modules

| | Modules natifs | Modules externes |
|---|---|---|
| Exemples | Team Builder, Tier Lists | Tournoi (démonstration) |
| Technologie | C# + Angular, dans le code de la plateforme | JavaScript compilé en WebAssembly, dans le dépôt du développeur |
| Confiance | Totale | Aucune : traité comme potentiellement hostile |

- **Team Builder** (MVP) : éditeur de plateau hexagonal en glisser-déposer, synergies calculées en temps réel, état complet encodé dans l'URL (le lien est l'objet partagé, rien n'est stocké). Les identifiants du jeu changent à chaque set : un ancien lien doit rester interprétable (encodage versionné).
- **Tier Lists** (MVP) : cinq tier lists par groupe (compositions, augments, objets composables, emblèmes, objets forgés), réordonnables par glisser-déposer.
- **Compos** et **Spotchecks** : différés (P2).
- Les deux catégories partagent les mêmes **contrats** : un manifeste (écrans, réglages, commandes Discord, données de la plateforme lues et modifiées), l'activation par groupe, les permissions par rôle, l'obligation de purge, d'anonymisation et d'export. Seule l'exécution diffère.
- Les modules natifs sont aussi appelés **modules de base** : activés par défaut dans un groupe, désactivables.

## Architecture de la marketplace (modules externes)

C'est le cœur du projet. Le modèle est celui des **Shopify Functions** : le module reçoit un paquet et rend une réponse ; **il ne peut rien demander pendant son exécution**.

### Déroulé d'un appel

1. **Événement** : un membre clique dans l'interface d'un module ou tape une de ses commandes Discord.
2. **Préparation** (API, zone de confiance) : la plateforme ouvre le **classeur** du module pour ce groupe et construit le **paquet** : l'événement, le contenu du classeur, le contexte (pseudonyme et rôle du membre, données de jeu utiles), les données de la plateforme déclarées dans le manifeste.
3. **Exécution** (salle des machines, zone isolée) : le module tourne dans une boîte WebAssembly, avec des plafonds de calcul (fuel) et de mémoire. Il rend une **réponse** : les modifications de son classeur, la description de l'écran à afficher, les messages éventuels.
4. **Vérification et application** (API) : la réponse est validée (format, taille, portée limitée au classeur du module et au contenu du groupe qu'il a le droit de modifier, pour ce groupe, dans la limite des droits du membre), puis appliquée.

### Accès aux données de la plateforme (validé le 2026-10-26)

| Données | Accès du module externe |
|---|---|
| Gestion des utilisateurs et des groupes (rôles, invitations, réglages) | Aucun |
| Liste des membres | Lecture seule, en pseudonymes |
| Contenu du groupe (compos, tier lists…) | Lecture et écriture, groupe en cours uniquement |
| Son classeur | Tout |

**Un module n'a pas de porte à lui** : une modification demandée par un module passe par le même cas d'usage que depuis le site. Groupe fixé par la plateforme, droits du membre, droits déclarés dans le manifeste, **seuil plancher** de rôle fixé par la plateforme (un module peut l'élever, jamais l'abaisser). Le module ne recopie jamais une donnée de la plateforme : il stocke des références.

Les appels d'un même module pour un même groupe sont **sérialisés** (un par un).

### Les couches de protection

1. **Bac à sable WebAssembly** (Wasmtime) : aucun accès aux fichiers, au réseau, aux bases ni à la mémoire de la plateforme. Plafonds de calcul et de mémoire à chaque appel.
2. **Salle des machines séparée** : un conteneur Docker distinct de l'API, sans secret, sans réseau sortant, sans accès aux bases, en lecture seule, avec CPU et mémoire plafonnés. Seule l'API peut l'appeler.
3. **Réponse vérifiée** : un module ne peut modifier que son propre classeur et le contenu du groupe pour lequel il a reçu le droit, pour le groupe en cours, dans la limite des droits du membre. *(Étendue le 2026-10-26 : évolution V2.1 du cahier des charges, voir `docs/Cahier des charges — évolutions/V2.1.md`.)*
4. **Classeur par module et par groupe** : un fichier **SQLite** choisi par la plateforme, jamais par le module, et plafonné en taille. **Le module n'écrit jamais de SQL** : la plateforme lit et écrit pour lui.
5. **Pseudonymes** : le module ne connaît les membres que par un pseudonyme propre à lui. La correspondance est stockée dans PostgreSQL.
6. **Interface déclarative** : le module décrit son écran avec les composants de la plateforme, et Angular l'affiche. **Aucun code tiers ne s'exécute dans le navigateur.** Une iframe isolée pour les développeurs vérifiés est prévue en P3 seulement.
7. **Niveaux de confiance** (non vérifié avec avertissement, vérifié, certifié) et **arrêt d'urgence** global.
8. **Suite de modules hostiles** exécutée en CI : le build échoue si une attaque réussit.

### Cycle de vie d'un module externe

Écriture en JavaScript avec le kit de développement TGFT → compilation en WebAssembly avec **Javy** → publication du fichier `.wasm` et de son manifeste (MVP : dépôt manuel ; P2 : compilation par GitHub Actions depuis le dépôt public, avec attestation) → activation par l'administrateur d'un groupe → exécution.

### Discord pour les modules externes (P3)

Le module déclare ses commandes dans son manifeste, sous la forme d'**une commande par module avec des sous-commandes**. Le bot relaie la commande comme un événement, et c'est la plateforme qui poste le message décrit par le module. Concrètement :
- la réponse est différée (`DeferAsync`), puis suivie d'un message ;
- **toutes les mentions sont désactivées** ;
- les pseudonymes sont remplacés par les vrais noms juste avant l'envoi ;
- le module ne voit jamais le jeton du bot.

### RGPD

- **Purge d'un groupe** : suppression de ses classeurs.
- **Anonymisation d'un utilisateur** : suppression de la correspondance avec ses pseudonymes.
- **Export des données d'un utilisateur** : la plateforme extrait elle-même des classeurs les entrées contenant ses pseudonymes (JSON).
- **Modules natifs** : ils doivent implémenter l'anonymisation, la purge et l'export, et c'est vérifié à la compilation.

### Plan de repli

Si le prototype Javy + Wasmtime échoue, l'exécution bascule sur **Jint** (interpréteur JavaScript en C#), dans la même salle des machines isolée. Tout le reste est inchangé.

### Décisions déjà tranchées : ne pas les reproposer

- Pas de chargement de modules externes en DLL dans le processus de l'API : .NET n'offre plus d'isolation en processus.
- Pas de conteneur par module (budget).
- Pas de Rust, pas de contrat WIT, pas de ComponentizeJS (outil expérimental).
- Pas d'Extism (sa partie .NET n'est pas à jour pour les correctifs de sécurité).
- Pas de fonctions hôtes appelées par le module pendant son exécution : il ne fait que recevoir un paquet et rendre une réponse.
- Pas de SQL écrit par les modules externes.
- Groupe en cours dans l'adresse (`/api/groups/{id}/…`), pas dans un en-tête.
- Cookie de session pour le site (pas de JWT), clé de service pour le bot.
- Grille de permissions fixe, pas personnalisable par groupe (pour l'instant).
- Migrations EF Core appliquées par une étape séparée avec le rôle propriétaire, jamais par l'API au démarrage.

Si tu penses qu'une de ces décisions pose un vrai problème, dis-le avec des arguments, mais ne change rien de toi-même.

## Stack technique

| Couche | Technologie |
|---|---|
| Back-end | ASP.NET Core (C#), Clean Architecture |
| Front-end | Angular (TypeScript), chargement différé par module |
| Base de données | PostgreSQL (Row Level Security, JSONB pour la configuration des modules) |
| Accès aux données | Entity Framework Core (filtres de requête globaux par groupe) + Dapper pour quelques lectures complexes |
| Temps réel | SignalR |
| Bot Discord | Discord.Net (C#) |
| Rendu d'images | ImageSharp |
| Cache | Mémoire applicative + disque (images de plateau, modules compilés). Pas de Redis |
| Modules externes | Wasmtime pour .NET (version LTS 48), Javy, SQLite (Microsoft.Data.Sqlite) |
| Conteneurisation | Docker / Docker Compose |
| Proxy inverse | Caddy (HTTPS, fichiers statiques) |
| CI | GitHub Actions |
| Tests | xUnit, Testcontainers, NetArchTest |
| Données de jeu | Data Dragon (Riot), sans clé d'API, vérifié une fois par jour, derrière un port d'Application. Traits et seuils absents au 2026-10-26 : point de contrôle avant le Team Builder |
| Authentification | Connexion Discord (reprise de la V0), cookie de session + CSRF pour le site, clé de service pour le bot. Jetons Discord non conservés |
| Migrations | EF Core Migrations, SQL à la main dedans pour la RLS et les rôles |

### Principes d'architecture à respecter

- **Isolation multi-tenant structurelle** : filtres globaux EF Core + RLS PostgreSQL. Ne jamais compter sur un filtre ajouté à la main requête par requête. Chaîne : groupe dans l'adresse → vérification d'appartenance (refus identique à « groupe inexistant ») → contexte « groupe en cours » → filtres EF → RLS par variable de transaction. Deux rôles PostgreSQL (propriétaire pour les migrations, applicatif pour l'API), RLS forcée. Identifiant de groupe sur chaque table de contenu, enfants compris. L'isolation et ses tests arrivent avec la première donnée de groupe, jamais après.
- **Clean Architecture** : le domaine et les cas d'usage ignorent la technologie d'exécution des modules. Les ports sont dans Application, les implémentations dans Infrastructure.
- **Frontières vérifiées par NetArchTest** : un module natif ne référence ni la couche de données du noyau, ni un autre module.
- **Les tests sont le mécanisme de sécurité** : les tests d'isolation, d'architecture et la suite de modules hostiles sont bloquants en CI. Ne jamais proposer de les désactiver ou de les contourner.

## Cadre Riot Games

Usage non commercial, mention « non approuvé par Riot » visible, aucune interaction avec le client de jeu, et **aucun système reproduisant ou estimant le classement officiel**. Ces règles s'appliquent aussi aux modules externes.

## Priorités

- **MVP** : engagement ferme.
- **P2** : si le MVP est tenu.
- **P3** : bonus.

Le bot Discord n'a **aucune** fonctionnalité MVP. Ne propose pas d'avancer sur du P2 ou du P3 tant que le MVP concerné n'est pas terminé, sauf si je le demande.

## Vocabulaire du projet

- **Groupe** : une communauté (un tenant).
- **Classeur** : le fichier SQLite d'un module externe pour un groupe donné.
- **Paquet** : les données envoyées au module pour un appel.
- **Réponse** : ce que le module renvoie (modifications, écran, messages).
- **Salle des machines** : le service .NET isolé qui exécute les modules WebAssembly.
- **Adhésion** : un utilisateur dans un groupe, avec un rôle.
- **Groupe en cours** : le groupe vérifié pour une requête, lu par tout le code et transmis à PostgreSQL.
- **Module de base** : synonyme de module natif.
- **Seuil plancher** : rôle minimum fixé par la plateforme pour une action sur ses données.
- **V0** : l'ancien projet TGFT (`Desktop\Mes projets\TGFT`).