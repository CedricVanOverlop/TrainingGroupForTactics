# TGFT — Projet Complet

> **Fichier de mémoire vivante.** Source de vérité du projet pour Claude et pour moi.
> En cas de contradiction avec `claude.md`, **ce fichier fait foi**.
> En cas de contradiction avec le **cahier des charges V2** (`docs/Cedric_Van_Overlop_-_Cahier_des_Charges_Projet_integration_V2.pdf`), **le cahier des charges fait foi** — et il faut corriger ce fichier.
>
> **Dernière mise à jour :** 2026-10-14 · **Version :** 1.4

**Sommaire**
[1. Le projet en une page](#1-le-projet-en-une-page) ·
[2. Problème et proposition](#2-problème-et-proposition) ·
[3. Modèle d'exploitation](#3-modèle-dexploitation) ·
[4. Rôles et permissions](#4-rôles-et-permissions) ·
[5. Les modules](#5-les-modules) ·
[6. La marketplace](#6-la-marketplace--le-cœur-du-projet) ·
[7. Stack technique](#7-stack-technique) ·
[8. Principes d'architecture](#8-principes-darchitecture-non-négociables) ·
[9. Cadre légal](#9-cadre-légal) ·
[10. User stories](#10-user-stories-condensées) ·
[11. Décisions tranchées](#11-décisions-tranchées--ne-pas-reproposer) ·
[12. Questions ouvertes](#12-questions-ouvertes) ·
[13. État d'avancement](#13-état-davancement) ·
[14. Journal](#14-journal-des-décisions-et-changements) ·
[15. Vocabulaire](#15-vocabulaire) ·
[16. Maintenir ce fichier](#16-comment-maintenir-ce-fichier)

---

## 1. Le projet en une page

**TGFT** est une plateforme web pour les **communautés organisées de joueurs de Teamfight Tactics** (serveurs Discord, associations, groupes d'entraînement). Chaque communauté — appelée **groupe** — y construit sa propre base de connaissance et ses propres outils.

C'est mon **projet de fin de bachelier** (projet d'intégration), auteur **Cédric Van Overlop**.

**Ce qui est évalué n'est pas la liste des fonctionnalités, mais l'architecture qui les porte :**

| Brique | Rôle |
|---|---|
| **Noyau minimal** | Comptes, groupes, membres, rôles, permissions. Rien d'autre. |
| **Modules** | Les fonctionnalités, activables / désactivables / configurables **par groupe**. |
| **Marketplace ouverte** | N'importe quel développeur publie un module contenant du **vrai code**, **sans relecture du mainteneur ni redéploiement de la plateforme**. |

Les modèles de référence sont **Odoo** (activation de modules par tenant) et **Shopify Functions** (exécution de code tiers sur l'infrastructure de l'hébergeur).

> **Pourquoi une marketplace alors qu'aucun contributeur externe n'est attendu ?**
> Sa valeur première n'est pas l'ouverture : c'est la **contrainte qu'elle impose au développeur lui-même**. Quand toutes les fonctionnalités passent par des contrats publics, on ne peut pas dériver vers le monolithe accidentel — chaque raccourci devient un franchissement de frontière que les tests d'architecture refusent. L'ouverture à des tiers est une **conséquence** de cette discipline, pas sa justification.

---

## 2. Problème et proposition

### Le domaine
TFT (Riot Games) est un *auto-battler* : 8 joueurs, chacun sur son plateau hexagonal. On achète des personnages dans une boutique aléatoire et on les place ; **le combat se résout automatiquement**. Toute la compétence s'exerce *avant* le combat. Trois savoir-faire : **composition** (quels personnages pour quelles synergies), **économie** (quand dépenser, quand épargner), **positionnement** (où placer chaque unité).

### Le problème
Les communautés organisées partagent le même besoin — progresser collectivement — et n'ont **aucun outil adapté**. Elles improvisent avec des captures d'écran Discord (non modifiables), des tableurs partagés (jamais à jour) et des explications en vocal (qui disparaissent et qu'il faut répéter à chaque nouveau membre). Résultat : **perte d'information permanente**, aucune trace consultable.

### La proposition : 4 fonctionnalités de base
| Module | Statut | Objet |
|---|---|---|
| **Team Builder** | **MVP — engagé** | Éditeur de plateau, synergies calculées en temps réel, **état complet encodé dans l'URL** : le lien *est* l'objet partagé, rien n'est stocké. |
| **Tier Lists** | **MVP — engagé** | Classement collaboratif sur 5 catégories (compositions, augments, objets composables, emblèmes, objets forgés), réordonnable par glisser-déposer. |
| **Compos** | P2 — différé | Fiches de composition documentées : plateau de référence, objets recommandés, conditions de viabilité, conseils de jeu. |
| **Spotchecks** | P2/P3 — différé | Situations de choix soumises au débat du groupe, pour travailler la prise de décision. |

> **Pourquoi exactement deux modules natifs engagés ?** Un seul module ne démontre pas une architecture modulaire — il démontre une séparation. Avec deux, on vérifie qu'un module s'ajoute sans toucher à l'autre, que le jeu de commandes du bot varie réellement d'un groupe à l'autre selon les activations, et que le contrat de l'épique D s'applique à un ensemble non trivial. Au-delà, chaque module supplémentaire consomme du temps destiné à la marketplace sans apporter de preuve architecturale. **Team Builder** pour sa richesse technique et sa valeur de démonstration visuelle, **Tier Lists** parce que c'est le module le moins coûteux du catalogue.

---

## 3. Modèle d'exploitation

- **Instance unique**, hébergée par moi, **multi-tenant**. Les utilisateurs s'inscrivent et rejoignent les groupes de leur choix.
- Un utilisateur peut appartenir à **plusieurs groupes**, avec un **rôle différent dans chacun**.
- Les données de chaque groupe sont **strictement isolées** de celles des autres.
- **Le code des modules externes s'exécute aussi sur cette instance** : un développeur n'a **aucun serveur à héberger**.
- **Budget d'hébergement < 10 €/mois**, sur **un seul serveur virtuel x86**.
  → Exclut structurellement toute architecture à **un conteneur ou une base de données par groupe ou par module**. Cette contrainte explique une grande partie des choix techniques.
- **Prolongement Discord** : un bot (consultation des tier lists et des plateaux, publication automatique des fiches de composition). **Pas prioritaire, mais conditionne l'adoption réelle de l'outil.**

---

## 4. Rôles et permissions

Quatre rôles, **par groupe** : **Fondateur**, **Administrateur**, **Contributeur**, **Lecteur**.

| Droit | Fondateur | Admin | Contributeur | Lecteur |
|---|:---:|:---:|:---:|:---:|
| Consulter le contenu des modules | ✓ | ✓ | ✓ | ✓ |
| Créer / modifier / supprimer du contenu | ✓ | ✓ | ✓ | — |
| Inviter, exclure un membre | ✓ | ✓ | — | — |
| Attribuer un rôle | ✓ | ✓ | — | — |
| Consulter les données des membres | ✓ | ✓ | — | — |
| Activer, désactiver, configurer un module | ✓ | ✓ | — | — |
| Modifier l'identité du groupe | ✓ | ✓ | — | — |
| Transférer la propriété du groupe | ✓ | — | — | — |
| Supprimer le groupe et purger ses données | ✓ | — | — | — |

**Point structurant :** le **contributeur produit du contenu et rien d'autre**. Aucun droit sur les membres, aucun droit sur l'activation ou la configuration des modules (US **C7**, **C8**). Ces deux exigences sont formulées **négativement** à dessein : elles décrivent ce qui ne doit *jamais* être permis, et se vérifient par des **tests d'autorisation sur l'ensemble des points d'accès**, pas par un écran.

De même, **C10** (« ne jamais voir les données d'un autre groupe ») est une **exigence transverse** : une propriété du système vérifiée par tests automatisés, pas une fonctionnalité visible.

---

## 5. Les modules

|  | **Modules natifs** | **Modules externes** |
|---|---|---|
| Exemples | Team Builder, Tier Lists | Tournoi (module de démonstration, **M16**) |
| Auteur | Le mainteneur du projet | N'importe quel développeur |
| Technologie | **C# + Angular**, dans le code de la plateforme | **JavaScript compilé en WebAssembly**, dans le dépôt du développeur |
| Publication | Avec une nouvelle version de la plateforme | **À tout moment**, sans relecture ni redéploiement |
| Confiance | Totale (code relu, testé, compilé avec le noyau) | **Aucune** : traité comme potentiellement hostile |

### Contrainte de conception fondatrice
**Les deux catégories partagent exactement les mêmes contrats :**
- un **manifeste** qui déclare le module, ses écrans, ses réglages et ses commandes Discord ;
- l'**activation par groupe** ;
- les **permissions par rôle** ;
- l'**obligation de purge et d'anonymisation**.

**Seule l'exécution diffère** : les natifs tournent avec la plateforme, les externes dans un bac à sable.

### Détail des deux modules natifs engagés
- **Team Builder** — grille hexagonale en glisser-déposer (souris **et doigt**), synergies en temps réel, niveaux d'étoiles et objets par unité, **deux plateaux en vis-à-vis** pour raisonner sur une confrontation. **État complet encodé dans l'URL** → un lien reçu doit reconstruire *exactement* le plateau d'origine. Les identifiants du jeu changeant à chaque set (~4 mois), **l'encodage doit être versionné** pour qu'un ancien lien reste interprétable.
- **Tier Lists** — 5 tier lists distinctes par groupe, réordonnables par glisser-déposer, **visibles du seul groupe**.

---

## 6. La marketplace — le cœur du projet

### 6.1 Le problème à résoudre
Un module externe est **du code écrit par un inconnu**, exécuté sur l'instance partagée par toutes les communautés. Malveillant ou simplement bogué, il ne doit pouvoir abîmer **que son propre périmètre** : jamais lire les données d'un autre groupe, ni les secrets de la plateforme, ni ralentir le service pour les autres.

**.NET n'offre plus aucun moyen d'isoler du code tiers au sein d'un même processus** (*Code Access Security* a été abandonné). Charger un module externe comme une simple bibliothèque lui donnerait les droits de la plateforme elle-même — donc l'accès aux données de tous les groupes.

### 6.2 Le principe : une boîte fermée
Le module est compilé en **WebAssembly** et s'exécute dans un **bac à sable**. Par construction, il n'a accès à rien : ni fichiers, ni réseau, ni base, ni mémoire de la plateforme. **Il ne peut que calculer.**

Surtout : **il ne demande rien pendant son exécution.** La plateforme lui donne tout au départ ; il rend une réponse ; la plateforme décide de ce qu'elle en fait. *(Modèle Shopify Functions.)*

> Chaque question qu'un module pourrait poser serait une porte supplémentaire à surveiller. En lui donnant tout au départ, la seule chose à contrôler est **sa réponse**.

### 6.3 Déroulé d'un appel
| # | Étape | Où | Ce qui se passe |
|---|---|---|---|
| 1 | **Événement** | Navigateur ou Discord | Un membre clique dans l'interface du module, ou tape une de ses commandes. |
| 2 | **Préparation** | API — *zone de confiance* | La plateforme ouvre le **classeur** du module pour ce groupe et construit le **paquet** : l'événement + le contenu du classeur + le contexte (pseudonyme et rôle du membre, données de jeu utiles). |
| 3 | **Exécution** | **Salle des machines** — *zone isolée* | Le module tourne dans sa boîte Wasm, avec plafonds de **calcul (fuel)** et de **mémoire**. Il rend une **réponse** : modifications de son classeur, description de l'écran à afficher, messages éventuels. |
| 4 | **Vérification & application** | API — *zone de confiance* | La réponse est validée (format, taille, **portée limitée au classeur de ce module pour ce groupe**), puis appliquée et affichée. |

Les appels d'un même module pour un même groupe sont **sérialisés** (un par un) → aucune écriture concurrente dans un classeur. Pour le développeur, rien de ce mécanisme n'est visible : le kit lui fournit des fonctions comme `enregistrer()`, `supprimer()`, `afficher()` qui **alimentent simplement la réponse**.

### 6.4 Les 8 couches de protection
*Elles sont **indépendantes** : la défaillance de l'une ne suffit pas à compromettre le système.*

| # | Couche | Ce qu'elle garantit |
|---|---|---|
| 1 | **Bac à sable WebAssembly** (Wasmtime) | Accès au seul paquet. Boucle infinie → coupée par le plafond de calcul. Mémoire excessive → coupée par le plafond mémoire. |
| 2 | **Salle des machines séparée** | Conteneur Docker **distinct de l'API** : sans secret, sans réseau sortant, sans accès aux bases, système de fichiers en lecture seule, CPU et mémoire plafonnés. Un module qui parviendrait à sortir de sa boîte **n'y trouverait rien d'exploitable**. Seule l'API peut l'appeler. |
| 3 | **Réponse vérifiée** | Un module ne peut modifier **que son propre classeur**, **pour le groupe en cours**, dans des limites de taille fixées. |
| 4 | **Classeur par module et par groupe** | Un fichier **SQLite** séparé, **choisi par la plateforme et jamais par le module**, plafonné en taille. **Le module n'écrit jamais de SQL** : la plateforme lit et écrit pour lui. |
| 5 | **Pseudonymes** | Le module ne connaît les membres que par un pseudonyme **propre à lui** → il ne peut pas suivre un membre d'un module à l'autre. La correspondance reste dans PostgreSQL, hors de sa portée. |
| 6 | **Interface déclarative** | Le module **décrit** son écran avec les composants de la plateforme, Angular l'affiche. **Aucun code tiers ne s'exécute dans le navigateur.** (Cadre isolé pour développeurs vérifiés : **P3** seulement, M15.) |
| 7 | **Niveaux de confiance** | Non vérifié (avec avertissement) · vérifié · certifié. Plus un **arrêt d'urgence global en une action** (M7). |
| 8 | **Suite de modules hostiles** | Une suite de modules malveillants rejoue **chaque attaque connue** en intégration continue. **Le build échoue si une seule réussit.** |

### 6.5 Cycle de vie d'un module externe
1. **Écriture** — en JavaScript, dans le dépôt du développeur, avec le **kit de développement TGFT** (types, fonctions de réponse, composants d'interface).
2. **Compilation** — **Javy** transforme le JS en fichier `.wasm`. *MVP :* le développeur compile et dépose lui-même le fichier avec son manifeste. *P2 (M4) :* compilation par **GitHub Actions depuis le dépôt public**, avec **attestation d'origine** vérifiée par la plateforme.
3. **Publication** — la plateforme valide le manifeste (nom, version, écrans, réglages, commandes, taille de classeur demandée) et le fichier, puis ajoute la version au catalogue **sans redéploiement**.
4. **Activation** — l'administrateur d'un groupe voit le niveau de confiance et **les données que le module recevra**, puis l'active pour son groupe.
5. **Exécution** — selon §6.3. Le fichier compilé est conservé en cache.
6. **Retrait** — désactivation avec conservation des données (D5, P2), purge (D10, P2), ou arrêt d'urgence sur toute la plateforme (M7, MVP).

### 6.6 Discord pour les modules externes (P3)
Le module déclare ses commandes dans son manifeste : **une commande par module, avec des sous-commandes**. Le bot relaie la commande comme un **événement** ; **c'est la plateforme qui poste** le message décrit par le module.
- Réponse différée (`DeferAsync`), puis message de suivi.
- **Toutes les mentions désactivées.**
- Les pseudonymes sont remplacés par les vrais noms **juste avant l'envoi**.
- **Le module ne voit jamais le jeton du bot.**

### 6.7 RGPD, rendu simple par l'architecture
*Rien n'est exigé des modules externes :*
- **Purger un groupe (B8)** → supprimer ses classeurs. Aucune action demandée au module.
- **Anonymiser un utilisateur (A5)** → supprimer la correspondance utilisateur ↔ pseudonymes. Ses contributions restent en place, attachées à un pseudonyme qui ne désigne plus personne.
- **Limite assumée** : un texte libre saisi par un membre (un nom cité dans un commentaire) n'est pas couvert par ce mécanisme. Les **conditions de publication interdisent donc aux modules de solliciter des données personnelles** ; la purge d'un groupe, elle, reste complète.
- **Modules natifs (D11)** : ils doivent **implémenter** l'anonymisation et la purge, **vérifié à la compilation**. D11 conditionne A4, A5 et B8 — et doit être posé **avant d'écrire le premier module**, car il ne peut pas être rattrapé une fois le catalogue constitué.

### 6.8 Modèles de référence
| Brique TGFT | Modèle en production |
|---|---|
| Code tiers en Wasm, paquet en entrée, opérations en sortie | **Shopify Functions** |
| Compilation JavaScript → WebAssembly | **Javy** (créé par Shopify, maintenu par la Bytecode Alliance) |
| Une base de données séparée par installation de module | **Atlassian Forge SQL** |
| Interface décrite avec les composants de la plateforme | **Shopify UI extensions** |

### 6.9 Limites assumées
- **Pas de lecture en cours d'exécution** : tout ce dont le module a besoin doit figurer dans le paquet.
- **Classeur plafonné** (quelques mégaoctets) puisqu'il est chargé à chaque appel. Largement suffisant pour un tournoi ou une tier list.
- **Pas d'accès réseau**, pas de temps réel, pas d'appel à un service extérieur.
- **Intégration visuelle limitée** aux composants de la plateforme (sauf cadre isolé, M15/P3).
- **Aucune isolation n'est infaillible.** En avril 2026, Wasmtime a corrigé deux failles critiques permettant de sortir du bac à sable (limitées à un compilateur secondaire et aux processeurs ARM, sur toutes les versions supportées). D'où la **salle des machines séparée (couche 2)** et **trois règles d'exploitation** : serveur **x86**, **compilateur par défaut**, **application rapide des mises à jour de sécurité**.

### 6.10 Périmètre marketplace dans le projet
| **Implémenté (MVP)** | **Conçu et documenté, implémenté si le MVP est tenu** |
|---|---|
| Salle des machines Wasmtime, plafonds calcul / mémoire | Compilation depuis le dépôt avec attestation (**M4**) |
| Format d'échange paquet / réponse + kit de développement JS | Vérification et certification des développeurs (**M11**) |
| Classeurs SQLite par module et par groupe, pseudonymes | Accord de l'administrateur pour les mises à jour (**M6**) |
| Interface déclarative et composants de base | Outil de test local (**M2**) |
| Publication par dépôt du fichier compilé | Désactivation automatique (**M9**), commandes Discord (**M14**) |
| Niveau non vérifié, avertissement, arrêt d'urgence | Cadre isolé pour les développeurs vérifiés (**M15**) |
| Suite de modules hostiles en intégration continue | |
| Module de démonstration **Tournoi** (**M16**) | |

*Les mécanismes différés sont **conçus et documentés** dans le cahier des charges même s'ils ne sont pas implémentés : **l'isolation entre groupes ne dépend d'aucun d'eux**.*

### 6.11 Premier jalon technique et plan de repli
**Premier livrable de la marketplace = un prototype de bout en bout** : un module JavaScript compilé avec Javy, exécuté depuis .NET avec ses plafonds.

**S'il échoue → bascule sur Jint** (interpréteur JavaScript écrit en C#), dans la **même salle des machines isolée**. Ses auteurs précisant qu'il ne constitue pas à lui seul une frontière de sécurité, la couche 2 deviendrait alors la protection principale. **Tout le reste est inchangé** : paquet, réponse, classeurs, pseudonymes, interface.

---

## 7. Stack technique

| Couche | Technologie | Raison déterminante |
|---|---|---|
| **Back-end** | **ASP.NET Core (C#)**, Clean Architecture | Typage fort, écosystème mature. **Avantage décisif : le bot Discord et la salle des machines sont écrits dans le même langage** → contrats de données partagés et vérifiés **à la compilation**. Avec un bot Python ou JS, ces contrats seraient dupliqués à la main et la moindre évolution de l'API casserait le bot silencieusement. |
| **Front-end** | **Angular (TypeScript)** | Chargement différé **aligné sur la modularité** : les écrans d'un module désactivé ne sont pas chargés → **un module inactif ne coûte rien à l'utilisateur**. Affiche l'écran déclaré par un module externe avec ses propres composants → **aucun code inconnu dans le navigateur**. |
| **Base de données** | **PostgreSQL** | (1) **Row Level Security** = second rempart **indépendant du code applicatif** : même si une requête oubliait son filtre, la base ne renverrait pas les données d'un autre groupe. (2) **JSONB** indexable pour la configuration des modules (schéma variable par nature, appelé à évoluer sans modification du noyau). Conserve aussi la correspondance **utilisateur ↔ pseudonymes**, hors de portée des modules. |
| **Accès aux données** | **EF Core + Dapper** | **Filtres de requête globaux** déclarés une fois par entité, appliqués automatiquement à **toutes** les requêtes, y compris celles écrites plus tard → l'isolation devient **structurelle, pas déclarative**. Dapper réservé aux quelques lectures complexes, **via la couche d'accès dédiée**. |
| **Temps réel** | **SignalR** | Intégré nativement au back-end, repli automatique selon les capacités du client. |
| **Bot Discord** | **Discord.Net (C#)** | Écosystème plus petit que discord.js / discord.py, **choix assumé** : le partage de code avec l'API prime. |
| **Rendu d'images** | **ImageSharp** | Purement managé, sans dépendance native → conteneur léger. Images de plateau exploitables par le site **et** par le bot. |
| **Cache** | **Mémoire applicative + disque** | Données de jeu chargées une fois, rechargées seulement si la vérification quotidienne détecte un changement. Images de plateau générées une fois puis conservées sur disque, sous un **nom dérivé du contenu du plateau et de la version du jeu** → un plateau modifié produit automatiquement un nouveau fichier. Même cache disque pour les modules externes compilés. **Pas de Redis** : n'apporterait quelque chose qu'en déploiement multi-instances, ce qui n'est pas le modèle retenu. |
| **Exécution des modules externes** | **Wasmtime pour .NET, version 48 (LTS)** | Bytecode Alliance, moteur conçu pour exécuter du code non fiable, avec processus public de publication des failles et de leurs correctifs. **LTS = correctifs de sécurité garantis 24 mois.** Bibliothèque .NET officielle → salle des machines écrite en C#, sans nouveau langage dans la pile. |
| **Compilation des modules externes** | **Javy** | JS → Wasm. Le moteur JavaScript est **compilé dans le module lui-même** : un défaut de ce moteur reste **confiné dans la boîte**. JavaScript abaisse la barrière d'entrée pour les développeurs de la communauté. |
| **Stockage des modules externes** | **SQLite (Microsoft.Data.Sqlite)** | Un fichier par module et par groupe : séparation **physique** des données, purge par simple suppression de fichier, quota par taille, **aucun serveur supplémentaire à exploiter**. |
| **Conteneurisation** | **Docker / Docker Compose** | Reproductibilité de l'environnement et démarrage de toute la pile en une commande — condition d'une démonstration fiable. La salle des machines tourne dans **son propre conteneur**. |
| **Proxy inverse** | **Caddy** | Point d'entrée public unique ; **l'API n'est jamais exposée directement**, la salle des machines jamais joignable de l'extérieur. Gère seul les certificats HTTPS, obtention et renouvellement (source classique de pannes), et sert les fichiers statiques Angular ainsi que les images générées → évite un second serveur web dédié. |
| **Intégration continue** | **GitHub Actions** | Compilation, tests et contrôles d'architecture à chaque contribution. À terme (M4) : compile aussi les modules externes depuis leur dépôt public et produit l'attestation d'origine. |
| **Tests** | **xUnit · Testcontainers · NetArchTest** | xUnit pour les tests unitaires du domaine ; **Testcontainers pour lancer une vraie base jetable**, condition nécessaire pour tester réellement l'isolation entre groupes et la vraie salle des machines face à la suite de modules hostiles ; **NetArchTest pour exprimer les règles d'architecture sous forme de tests**. |
| **Données de jeu** | **Data Dragon (Riot)** | Dépôt de données statiques officiel, contenu public ne requérant **aucune clé d'API** → aucune dépendance à une autorisation d'accès, aucun risque de révocation d'identifiant. Version publiée vérifiée **une fois par jour**, données rechargées uniquement en cas de changement effectif. |

### Versions ancrées *(état au 2026-10-14)*

| Outil | Version | Note |
|---|---|---|
| .NET SDK | 10.0.300 *(dernière : 10.0.402)* | .NET 10 en support actif, **EOL 2028-11-14** — couvre toute la durée du projet. |
| Cible des projets | `net10.0` | — |
| Node.js | **24.20.0** | **Minimum 24.15.0** imposé par Angular 22. |
| Angular | **22.2.2** | TypeScript 6.0, **Vitest** comme lanceur de tests (remplace Karma/Jasmine). |
| Wasmtime .NET | 48 (LTS) | Pas encore ajouté — arrive avec le prototype Q6. |

---

## 8. Principes d'architecture (non négociables)

1. **Isolation multi-tenant structurelle** — filtres globaux EF Core **+** RLS PostgreSQL. **Ne jamais compter sur un filtre ajouté à la main requête par requête** : une seule omission suffit, et elle est silencieuse.
2. **Clean Architecture** — le domaine et les cas d'usage **ignorent la technologie d'exécution des modules**. Les **ports** sont dans `Application`, les **implémentations** dans `Infrastructure`. (C'est ce qui rend le plan de repli Javy → Jint possible sans rien casser ailleurs.)

   **Rôle de chaque couche :**

   | Projet | Contient | Dépend de | Exemples TGFT |
   |---|---|---|---|
   | `TGFT.Domain` | Objets métier et règles vraies **indépendamment de toute technologie** | **rien** | `Group`, `Member`, `Role`, `Module`, `Classeur`. La règle « un contributeur ne peut pas activer un module » (C8). |
   | `TGFT.Application` | Cas d'usage + **ports** (interfaces décrivant un besoin, pas une implémentation) | Domain | `IModuleRunner`, `IGroupRepository`, `IClasseurStore`. « Activer un module pour un groupe », « exécuter un module externe ». |
   | `TGFT.Infrastructure` | **Implémentations** des ports | Application | `EfGroupRepository`, `WasmtimeModuleRunner`, `SqliteClasseurStore`, client Data Dragon, Discord.Net. |
   | `TGFT.Api` | HTTP, authentification, injection de dépendances | App + Infra | Le seul projet qui connaît les deux côtés. |

   *Repère : `Domain` = ce qui resterait vrai en changeant de base de données, de framework web **et** de moteur d'exécution. `Application` = ce que l'application **fait**, exprimé sans dire **avec quoi**.*

   Un découpage plus courant en 3 projets (Core / Infrastructure / Api) fusionne Domain et Application. Il est scindé ici pour deux raisons précises : **(a)** le port `IModuleRunner` vivant dans `Application`, la bascule Javy → Jint ne touche qu'`Infrastructure` au lieu de remonter dans le métier ; **(b)** `Domain` ne référençant rien, NetArchTest peut l'affirmer mécaniquement (**D8**) — un `Core` mixte se vérifie beaucoup moins nettement.
3. **Frontières vérifiées par NetArchTest** — un module natif ne référence **ni la couche de données du noyau, ni un autre module**. Le build échoue sinon (**D8**).
4. **Les tests sont le mécanisme de sécurité** — les tests d'isolation, d'architecture et la suite de modules hostiles **ne vérifient pas la protection : ils *sont* la protection**. Chaque attaque connue est rejouée à chaque changement. Ils sont **bloquants en intégration continue, au même titre que la compilation**. **Ne jamais proposer de les désactiver ou de les contourner.**

---

## 9. Cadre légal

### Licence
- **AGPL-3.0**, contributions sous **DCO** pour le dépôt principal.
- *Argument déterminant : la **clause réseau**, propre à l'AGPL.* Elle étend l'obligation de publication au cas où un logiciel modifié est **mis à disposition via un réseau** sans être redistribué — exactement le risque pour un projet destiné à être exploité comme service en ligne. Une GPL classique ne couvrirait pas ce cas, une licence permissive encore moins.
- **Modules natifs** : compilés dans le même binaire que le noyau → œuvre liée → même licence (copyleft par liaison de code).
- **Modules externes** : vivent dans le dépôt de leur auteur et ne communiquent avec la plateforme que par **échange de données** → ce lien ne permet pas de les considérer avec certitude comme des œuvres liées → la licence du noyau ne s'impose pas à eux. **Une règle de publication prend le relais** : un module n'est accepté que si **son code source est public, sous une licence libre reconnue par l'OSI**. C'est aussi cette condition qui rend possible la compilation attestée depuis le dépôt (M4).

### Riot Games
- Usage **strictement non commercial**.
- Mention **« non approuvé par Riot » visible**.
- **Aucune interaction avec le client de jeu.**
- **Aucun système reproduisant ou estimant le classement officiel** — explicitement interdit par leurs conditions d'utilisation.
- **Ces règles s'imposent aussi aux modules externes** et figurent dans les conditions de publication de la marketplace.

### RGPD
L'exploitation d'une instance unique hébergeant les données de plusieurs communautés fait de moi le **responsable de traitement**. Les droits d'accès, de portabilité et à l'effacement, ainsi que l'information préalable des utilisateurs (**A3 → A6**), sont **MVP pour raison légale, non fonctionnelle** : sans eux, la plateforme **ne peut pas être exploitée légalement**. Voir aussi §6.7.

---

## 10. User stories (condensées)

**Niveaux :** **MVP** = sans cette fonctionnalité le produit n'existe pas, engagement ferme · **P2** = attendu, développé si le MVP est tenu · **P3** = bonus, hors engagement.
**Aucune fonctionnalité du bot Discord n'est MVP** : le bot est développé indépendamment de l'application web, une fois le socle web stabilisé.

### Application web

**Épique A — Compte et accès**
| Réf | Story | Prio |
|---|---|---|
| A1 | Connexion avec son compte **Discord** (pas de nouveaux identifiants) | MVP |
| A2 | Pseudo et avatar Discord repris automatiquement | MVP |
| A3 | Consulter et **exporter** ses données (accès + portabilité) | MVP |
| A4 | **Supprimer définitivement** son compte (effacement) | MVP |
| A5 | Supprimer son compte **en gardant ses contributions anonymisées** | MVP |
| A6 | Consulter politique de confidentialité et CGU **avant** inscription | MVP |
| A7 | Rester connecté entre les visites | P2 |

**Épique B — Groupes**
| Réf | Story | Prio |
|---|---|---|
| B1 | Créer un groupe | MVP |
| B2 | Définir la visibilité : public ouvert / public sur candidature / privé sur invitation | MVP |
| B3 | Appartenir à plusieurs groupes et basculer facilement de l'un à l'autre | MVP |
| B4 | Générer un **lien d'invitation** | MVP |
| B5 | Parcourir les groupes publics et demander à en rejoindre un | MVP |
| B6 | Personnaliser l'identité visuelle du groupe (nom, logo, couleur) | P2 |
| B7 | Transférer la propriété du groupe à un administrateur | P2 |
| B8 | **Supprimer définitivement le groupe et toutes ses données** | MVP |

> **B8 ne peut pas être différée.** Outre son fondement légal, c'est **la seule story qui *exerce réellement*** le contrat de purge imposé aux modules natifs (D11) et la suppression des classeurs des modules externes (M13). Sans elle, ces garanties seraient déclarées, compilées, mais **jamais exécutées — donc jamais vérifiées**. *Une garantie qu'on n'exerce pas est une garantie qu'on ne possède pas.*

**Épique C — Membres, rôles et permissions**
| Réf | Story | Prio |
|---|---|---|
| C1 | Consulter la liste des membres et leur rôle | MVP |
| C2 | Attribuer un rôle à un membre | MVP |
| C3 | Exclure un membre | MVP |
| C4 | Contributeur : créer et modifier le contenu des modules activés | MVP |
| C5 | Contributeur : supprimer un contenu devenu obsolète | MVP |
| C6 | Contributions **attribuées à leur auteur** | MVP |
| C7 | **Le contributeur n'a aucun droit sur les membres** (ni consultation, ni rôles, ni exclusion) | MVP |
| C8 | **Le contributeur ne peut ni activer, ni désactiver, ni configurer un module** | MVP |
| C9 | Lecteur : consulter le contenu sans pouvoir le modifier | MVP |
| C10 | **Ne jamais voir les données d'un autre groupe** (exigence transverse) | MVP |
| C11 | Journal des actions d'administration | P3 |

**Épique D — Système de modules** *(cœur du projet avec M)*
| Réf | Story | Prio |
|---|---|---|
| D1 | Consulter le catalogue des modules disponibles avec leur description | MVP |
| D2 | Activer / désactiver un module pour son groupe | MVP |
| D3 | Ne voir dans la navigation que les modules **actifs** du groupe | MVP |
| D4 | Configurer chaque module activé depuis une page dédiée | MVP |
| D5 | Une désactivation **conserve** les données du module | P2 |
| D6 | Dév. externe : **documentation complète** du kit et du format d'échange | MVP |
| D7 | Dév. externe : **module d'exemple fonctionnel** servant de gabarit | MVP |
| D8 | **Le build échoue si un module natif franchit une frontière d'architecture** | MVP |
| D9 | Voir les données qu'un module recevra et les commandes qu'il ajoute **avant** de l'activer | P2 |
| D10 | Purger définitivement les données d'un module sans supprimer le groupe | P2 |
| D11 | **Tout module natif implémente obligatoirement anonymisation + purge, vérifié à la compilation** | MVP |

**Épique M — Marketplace et développeurs externes** *(prolonge directement D)*
| Réf | Story | Prio |
|---|---|---|
| M1 | Écrire un module en **JS** avec le kit fourni et le compiler en Wasm **en une seule commande** | MVP |
| M2 | Tester son module en local avec un paquet d'exemple, sans installer la plateforme | P2 |
| M3 | **Publier une nouvelle version immédiatement**, sans relecture ni redéploiement | MVP |
| M4 | Compilation **attestée depuis le dépôt public** par une chaîne automatisée | P2 |
| M5 | Voir le **niveau de confiance** d'un module et un avertissement clair s'il n'est pas vérifié | MVP |
| M6 | Rester sur la version installée tant qu'une mise à jour plus gourmande n'est pas acceptée | P2 |
| M7 | **Arrêt d'urgence** d'un module sur toute la plateforme en une action | MVP |
| M8 | Un module qui dépasse ses limites (calcul, mémoire, stockage, taille de réponse) est **interrompu sans effet sur les autres modules ni sur les autres groupes** | MVP |
| M9 | Désactivation automatique d'un module en échec répété, auteur prévenu | P3 |
| M10 | **Suite de modules hostiles rejouée à chaque changement ; le build échoue si une attaque réussit** | MVP |
| M11 | Vérification d'identité du développeur pour obtenir le niveau vérifié | P2 |
| M12 | **Les modules externes ne connaissent jamais l'identité réelle d'un membre**, seulement un pseudonyme propre à chaque module | MVP |
| M13 | **Un classeur séparé par module et par groupe**, choisi par la plateforme, jamais par le module | MVP |
| M14 | Dév. externe : déclarer des commandes Discord relayées par le bot sur les serveurs des groupes qui l'ont activé | P3 |
| M15 | Dév. **vérifié** : afficher une interface libre dans un cadre isolé | P3 |
| M16 | **Module de démonstration Tournoi**, écrit dans un dépôt séparé comme le ferait un développeur externe | MVP |

> **M8, M10, M12 et M13 sont des propriétés du système**, comme C10 : elles ne se traduisent pas par un écran, elles se vérifient par la **suite de modules hostiles (M10)**, où chaque tentative — lire un autre classeur, dépasser un plafond, renvoyer une réponse malformée — **doit échouer**.
>
> **M4 est classée P2 sans affaiblir la sécurité** : sans elle, la publication passe par le dépôt manuel du fichier compilé — **le bac à sable protège exactement de la même façon**. Seule la *traçabilité de l'origine du code* est différée.

**Épique E — Team Builder (engagé)**
| Réf | Story | Prio |
|---|---|---|
| E1 | Placer des personnages sur une grille hexagonale par glisser-déposer | MVP |
| E2 | Voir **en temps réel** les synergies activées par sa composition | MVP |
| E3 | Attribuer un niveau d'étoiles et des objets à chaque unité | MVP |
| E4 | **Partager un plateau par simple lien**, sans rien enregistrer | MVP |
| E5 | **Un lien reçu reconstruit exactement le plateau d'origine** | MVP |
| E6 | Afficher **deux plateaux en vis-à-vis** pour raisonner sur une confrontation | MVP |
| E7 | Éditeur utilisable **au doigt** (mobile / tablette) | P2 |
| E8 | Enregistrer un plateau dans la bibliothèque du groupe avec un titre | P2 |
| E9 | Annoter un plateau enregistré | P3 |

**Épique G — Tier Lists (engagé)**
| Réf | Story | Prio |
|---|---|---|
| G1 | Classer des éléments par tiers en les faisant glisser | MVP |
| G2 | **Cinq tier lists distinctes** (compositions, augments, objets composables, emblèmes, objets forgés) | MVP |
| G3 | Consulter les tier lists de son groupe | MVP |
| G4 | **Classements visibles du seul groupe** | MVP |
| G5 | Justifier un classement par une note courte | P2 |

**Épique F — Compos (différé)** : F1 → F4 **P2** (créer une fiche depuis un plateau de référence ; objets recommandés et conseils de jeu ; consultation par les membres ; conditions de viabilité) · F5 → F7 **P3** (ouvertures de partie recommandées ; historique des modifications au fil des patchs ; commentaires).

**Épique H — Spotchecks (différé)** : H1 → H2 **P2** (créer un spotcheck présentant une situation et plusieurs choix ; le consulter pour s'entraîner) · H3 → H4 **P3** (donner son avis ; réponse argumentée publiée après un délai, pour que la discussion ait lieu avant la correction).

**Épique I — Modules bonus (non engagés)** : Entraînement (exercices de positionnement), Tournois *(version minimale = M16)*, Progression (suivi de l'activité individuelle), Calendrier (sessions et disponibilités), Fil d'activité (journal des contributions). **Vocation à être écrits comme modules externes** — ce sont eux qui prouveront que la plateforme est réellement extensible.

### Bot Discord *(aucune story MVP ; consomme l'API de l'application web)*
- **Épique J — Consultation** : J1 afficher un plateau depuis son lien · J2 afficher une tier list du groupe · J3 tiers attribué à un ou plusieurs augments · J4 tiers attribué à un ou plusieurs objets · J5 autocomplétion des noms d'augments et d'objets *(tous P2)* · J6 afficher un spotcheck dans un salon *(P3)*.
- **Épique K — Publication et synchronisation** : K1 publier les fiches de composition dans Discord · K2 le bot crée et organise lui-même les salons nécessaires · K3 republier sans créer de doublon · K4 **préserver les discussions humaines d'un salon lors d'une republication** *(tous P2)* · K5 exporter un plateau du site vers un salon *(P3)*.
- **Épique L — Administration du bot** : L1 lier son serveur Discord à son groupe depuis le site · L2 fonctionnement indépendant sur le serveur de chaque groupe · L3 configurer les salons utilisés · **L4 les commandes disponibles correspondent aux modules activés par le groupe** · L5 commandes d'administration réservées aux rôles autorisés *(tous P2)*.

> **L4 est la story la plus structurante du bot** : elle impose que les modules — natifs **comme** externes — déclarent leurs commandes dans leur manifeste, et que le jeu de commandes enregistré **varie d'un serveur à l'autre**. C'est la preuve que la modularité traverse **toute** la plateforme, pas seulement le web.

---

## 11. Décisions tranchées — NE PAS REPROPOSER

| Décision | Raison |
|---|---|
| ❌ **Pas de modules externes en DLL** chargées dans le processus de l'API | .NET n'offre plus d'isolation en processus (CAS abandonné) → le module aurait les droits de la plateforme. |
| ❌ **Pas de conteneur par module** (ni par groupe) | Budget < 10 €/mois, un seul serveur virtuel. |
| ❌ **Pas de base de données par groupe ou par module** | Même raison. |
| ❌ **Pas de Rust**, pas de **contrat WIT**, pas de **ComponentizeJS** | Outils expérimentaux ; JS + Javy abaisse la barrière d'entrée pour la communauté. |
| ❌ **Pas d'Extism** | Sa partie .NET n'est pas à jour pour les correctifs de sécurité. |
| ❌ **Pas de fonctions hôtes appelées par le module pendant son exécution** | Chaque porte serait à surveiller. Le module **reçoit un paquet et rend une réponse**, point. |
| ❌ **Pas de SQL écrit par les modules externes** | La plateforme lit et écrit dans le classeur pour eux. |
| ❌ **Pas de Redis** | N'apporterait quelque chose qu'en multi-instances — pas le modèle retenu. |
| ❌ **Pas de code tiers exécuté dans le navigateur** | Interface déclarative uniquement. Cadre isolé = P3, développeurs vérifiés seulement. |
| ❌ **Pas de système reproduisant ou estimant le classement officiel** | Explicitement interdit par les conditions de Riot. |
| ❌ **Ne jamais désactiver ou contourner** les tests d'isolation, d'architecture et la suite hostile | **Ils sont la protection**, pas sa simple vérification. |

*Si une de ces décisions pose un vrai problème : le dire avec des arguments, ne rien changer de soi-même.*

---

## 12. Questions ouvertes

*Section vivante. À remplir au fil des sessions. Format : question → options envisagées → décision + date (puis report en §14).*

| # | Question | Statut |
|---|---|---|
| Q1 | **Format exact d'encodage du plateau dans l'URL** (versionné, compact, résistant au changement de set) : numéro de version, alphabet, compression ? | **Ouvert** |
| Q2 | **Format précis du paquet et de la réponse** : schéma, versionnement, limites de taille chiffrées | **Ouvert** |
| Q3 | **Vocabulaire des composants de l'interface déclarative** : lesquels au MVP ? | **Ouvert** |
| Q4 | **Valeurs des plafonds** : fuel, mémoire Wasm, taille de classeur, taille de réponse, durée d'un appel | **Ouvert** |
| Q5 | **Découpage des projets .NET** : noms et dépendances de la solution Clean Architecture | ✅ **Tranché le 2026-10-14** — voir §13. Reste ouvert : le projet de la salle des machines (`TGFT.ModuleHost`), différé au prototype Q6. |
| Q6 | **Le prototype Javy + Wasmtime tient-il ?** → détermine si le plan de repli Jint s'active | **Non testé** |
| Q7 | **Stratégie de versionnement des données de jeu entre sets** : anciens liens, anciennes tier lists | **Ouvert** |

---

## 13. État d'avancement

**Phase actuelle : cadrage terminé, aucun code écrit.**

| Jalon | État |
|---|---|
| Cahier des charges V2 | ✅ Fait |
| Ce fichier de mémoire | ✅ Fait (2026-10-14) |
| Graphe de connaissance `graphify-out/` | ✅ Fait (2026-10-15) — versionné dans git ; `/graphify . --update` après chaque modification du code, et une seule fois avant le commit pour la doc et la configuration |
| **Squelette .NET** — `TGFT.slnx` + 4 projets `net10.0` dans `backend/`, références câblées, build vert | ✅ Fait (2026-10-14) |
| **Squelette Angular 22** — `frontend/`, SCSS, routing, sans SSR, build vert | ✅ Fait (2026-10-14) |
| `TGFT.ModuleHost` (salle des machines) | ⬜ À faire — avec le prototype Q6 |
| Docker Compose + Caddy (local) — projet `tgft-platform`, `docker compose up --build` → http://localhost:8081 | ✅ Fait (2026-10-15) |
| **Prototype marketplace de bout en bout** (Javy + Wasmtime) — *premier jalon technique* | ⬜ À faire |
| Noyau : comptes, groupes, membres, rôles, permissions | ⬜ À faire |
| Module natif Team Builder | ⬜ À faire |
| Module natif Tier Lists | ⬜ À faire |
| Module de démonstration Tournoi (M16) | ⬜ À faire |
| Suite de modules hostiles (M10) | ⬜ À faire |

---

## 14. Journal des décisions et changements

*Le plus récent en haut. Une ligne par décision tranchée, nouveauté ou changement de cap.*

| Date | Changement |
|---|---|
| 2026-10-15 | **Projet Compose renommé `tgft` → `tgft-platform`, port 8080 → 8081.** L'ancien projet `Desktop\Mes projets\TGFT` (MySQL, web, bot) porte déjà le nom `tgft`, que Docker déduit de son dossier, et publie aussi `127.0.0.1:8080`. Avec un nom identique, Docker a fusionné les deux projets et le nouveau service `api` a écrasé l'ancien conteneur `tgft-api-1` et son image. Aucune donnée n'a été perdue : les volumes `tgft_mysql-data` et `tgft_images` sont intacts, et l'ancienne API se reconstruit depuis son dossier. ⚠️ **Toujours un `name:` explicite et unique dans le compose.** |
| 2026-10-15 | **Graphe graphify créé et versionné** (`graphify-out/`, ~600 Ko) : il est disponible dans toute nouvelle session sans reconstruction (~250 000 tokens). **Règle de mise à jour assouplie** : le code est retraité après chaque modification (analyse syntaxique, gratuite), mais la doc et la configuration une seule fois avant le commit, parce que chaque fichier de doc est ré-extrait en entier par un LLM (~100 000 tokens pour ce fichier). |
| 2026-10-15 | **Pile Docker locale.** `docker-compose.yml` : `caddy` (seul port publié, `127.0.0.1:8080`, devenu 8081 : voir l'entrée ci-dessus), `api` (image `aspnet:10.0`, utilisateur non-root) et `postgres` (18, volume `pgdata`, healthcheck : l'API attend que la base soit prête). Caddy sert Angular et relaie `/api/*` vers l'API, qui n'est jamais exposée directement. Mot de passe dans `.env` (ignoré par git), modèle dans `.env.example`. Salle des machines pas encore incluse (prototype Q6). |
| 2026-10-14 | **Dépôt git initialisé dans `Desktop/TGFT`**, premier commit poussé sur `https://github.com/CedricVanOverlop/TrainingGroupForTactics` (branche `main`, commit `90fb241`, 37 fichiers). ⚠️ **`C:\Users\User` est lui-même un dépôt git** dont `origin` pointe sur `Padle-Site` — TGFT a donc son propre dépôt pour ne pas committer dedans. Toujours vérifier `git rev-parse --show-toplevel` avant un `git add`. |
| 2026-10-14 | **Arborescence : `backend/` + `frontend/`** au lieu d'un `src/` unique. Les 4 projets .NET sont dans `backend/`, l'application Angular **est** `frontend/`. `TGFT.slnx` régénéré. Les deux builds repassent verts. |
| 2026-10-14 | **Squelette créé.** Solution `TGFT.slnx` (nouveau format XML, défaut du SDK .NET 10) + `TGFT.Domain` → `TGFT.Application` → `TGFT.Infrastructure` → `TGFT.Api`, tous en `net10.0`. Angular **22.2.2** dans `frontend/` (créé d'abord dans `src/TGFT.Web`, déplacé ensuite, voir l'entrée ci-dessus) (SCSS, routing, **sans SSR** car Caddy sert les statiques). Projets de tests différés : rien à tester tant que le domaine est vide. `TGFT.ModuleHost` différé au prototype Q6. |
| 2026-10-14 | **Correctif sécurité au scaffold.** Le template `dotnet new webapi` tire `Microsoft.OpenApi` 2.0.0, vulnérable (**CVE-2026-49451**, CVSS 7.5, DoS par récursion non contrôlée sur références circulaires). `Microsoft.AspNetCore.OpenApi` monté 10.0.8 → **10.0.13** → résout `Microsoft.OpenApi` **2.12.2**. Plus aucun package vulnérable. **À re-vérifier à chaque ajout de dépendance** : `dotnet list package --vulnerable --include-transitive`. |
| 2026-10-14 | **Node minimal : 24.15.0.** Angular 22 refuse de démarrer en dessous (`^22.22.3 \|\| ^24.15.0 \|\| >=26`). Machine passée en 24.20.0. |
| 2026-10-14 | **Graphify différé** jusqu'au squelette de solution. Un graphe de connaissance construit sur 2 documents n'apporte rien : il sert à retrouver la structure d'un code qu'on ne lit pas en entier. Tant que le projet tient dans ce fichier, il est redondant. |
| 2026-10-14 | Création de ce fichier. Synthèse du cahier des charges V2 + `claude.md`. Aucune décision nouvelle. |

---

## 15. Vocabulaire

| Terme | Sens |
|---|---|
| **Groupe** | Une communauté — l'unité de *tenant*. |
| **Classeur** | Le fichier SQLite d'un module externe **pour un groupe donné**. |
| **Paquet** | Les données envoyées au module pour un appel (événement + classeur + contexte). |
| **Réponse** | Ce que le module renvoie : modifications du classeur, écran à afficher, messages. |
| **Salle des machines** | Le service .NET **isolé** qui exécute les modules WebAssembly. |
| **Zone de confiance** | L'API : prépare le paquet, vérifie et applique la réponse. |
| **Module natif** | C# + Angular, dans le code de la plateforme, confiance totale. |
| **Module externe** | JS → Wasm, dans le dépôt de son auteur, **confiance nulle**. |
| **Manifeste** | Déclaration d'un module : écrans, réglages, commandes Discord, taille de classeur demandée. |
| **Pseudonyme** | L'identité d'un membre telle qu'un module externe la voit — **propre à chaque module**. |
| **Set** | Une saison de TFT (~4 mois) ; les identifiants du jeu changent à chaque set. |

---

## 16. Comment maintenir ce fichier

**Pour Claude :**
- **Le lire en début de session**, après une longue conversation, et dès qu'une demande touche une partie du projet non encore vue.
- **Le mettre à jour sans demander** quand : une question est tranchée (§12 → §14), une nouveauté ou un changement de cap est décidé (§14 + section concernée), un jalon avance (§13).
- Mettre à jour **la date et la version** en tête à chaque modification.
- **Signaler** toute contradiction entre ce fichier, `claude.md` et le cahier des charges V2.
- Mettre ce fichier à jour **n'est pas écrire du code** → ne tombe pas sous la règle n°1 de `claude.md`, au même titre que la mise à jour du graphe graphify.

**Hiérarchie des sources :** cahier des charges V2 **>** `docs/Projet Complet.md` **>** `claude.md`.
