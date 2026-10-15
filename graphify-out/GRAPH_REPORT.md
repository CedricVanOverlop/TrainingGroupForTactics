# Graph Report - .  (2026-10-15)

## Corpus Check
- Corpus is ~13,097 words - fits in a single context window. You may not need a graph.

## Summary
- 202 nodes · 247 edges · 17 communities (13 shown, 4 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 16 edges (avg confidence: 0.82)
- Token cost: 104,967 input · 0 output

## Community Hubs (Navigation)
- [[_COMMUNITY_Projets .NET (csproj)|Projets .NET (csproj)]]
- [[_COMMUNITY_Template API WeatherForecast|Template API WeatherForecast]]
- [[_COMMUNITY_Profils de lancement API|Profils de lancement API]]
- [[_COMMUNITY_Configuration angular.json|Configuration angular.json]]
- [[_COMMUNITY_Outillage dev frontend|Outillage dev frontend]]
- [[_COMMUNITY_Cibles de build Angular|Cibles de build Angular]]
- [[_COMMUNITY_Dépendances Angular|Dépendances Angular]]
- [[_COMMUNITY_Composants Angular|Composants Angular]]
- [[_COMMUNITY_Sources de référence et règles|Sources de référence et règles]]
- [[_COMMUNITY_Noyau, isolation et tests|Noyau, isolation et tests]]
- [[_COMMUNITY_Flux d appel et salle des machines|Flux d appel et salle des machines]]
- [[_COMMUNITY_README frontend|README frontend]]
- [[_COMMUNITY_Template racine Angular|Template racine Angular]]
- [[_COMMUNITY_Routage Angular|Routage Angular]]
- [[_COMMUNITY_Pile Docker locale|Pile Docker locale]]
- [[_COMMUNITY_Modules natifs, externes et Discord|Modules natifs, externes et Discord]]
- [[_COMMUNITY_Classeur, données et exigences|Classeur, données et exigences]]

## God Nodes (most connected - your core abstractions)
1. `Classeur (SQLite par module et par groupe)` - 8 edges
2. `8 couches de protection` - 8 edges
3. `TGFT.Web` - 7 edges
4. `CLAUDE.md (Projet TGFT)` - 7 edges
5. `docker-compose.yml (tgft-platform)` - 7 edges
6. `Salle des machines` - 7 edges
7. `http` - 6 edges
8. `https` - 6 edges
9. `options` - 6 edges
10. `development` - 6 edges

## Surprising Connections (you probably didn't know these)
- `docker-compose.yml (tgft-platform)` --implements--> `Docker / Docker Compose`  [INFERRED]
  docker-compose.yml → docs/Projet Complet.md
- `postgres service (postgres:18-alpine)` --conceptually_related_to--> `PostgreSQL Row Level Security`  [INFERRED]
  docker-compose.yml → docs/Projet Complet.md
- `Diagram: zone de confiance / salle des machines call flow` --references--> `Salle des machines`  [EXTRACTED]
  docs/Cedric_Van_Overlop_-_Cahier_des_Charges_Projet_integration_V2.pdf → docs/Projet Complet.md
- `CLAUDE.md (Projet TGFT)` --references--> `8 couches de protection`  [EXTRACTED]
  claude.md → docs/Projet Complet.md
- `caddy service (127.0.0.1:8081)` --implements--> `Caddy (proxy inverse)`  [INFERRED]
  docker-compose.yml → docs/Projet Complet.md

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Deroule d'un appel de module externe** — docs_projet_complet_zone_de_confiance, docs_projet_complet_paquet, docs_projet_complet_salle_des_machines, docs_projet_complet_reponse, docs_projet_complet_classeur [EXTRACTED 1.00]
- **Defense en profondeur de l'isolation multi-tenant** — docs_projet_complet_ef_core_global_filters, docs_projet_complet_postgresql_rls, docs_projet_complet_testcontainers, docs_projet_complet_tests_as_security [INFERRED 0.85]
- **Pile locale tgft-platform (caddy -> api -> postgres)** — docker_compose_caddy_service, docker_compose_api_service, docker_compose_postgres_service, docker_compose_pgdata_volume [EXTRACTED 1.00]

## Communities (17 total, 4 thin omitted)

### Community 5 - "Projets .NET (csproj)"
Cohesion: 0.19
Nodes (9): net10.0, Microsoft.AspNetCore.OpenApi (10.0.13), Microsoft.NET.Sdk.Web, net10.0, Microsoft.NET.Sdk, net10.0, Microsoft.NET.Sdk, net10.0 (+1 more)

### Community 3 - "Profils de lancement API"
Cohesion: 0.13
Nodes (15): $schema, profiles, http, commandName, dotnetRunMessages, launchBrowser, applicationUrl, environmentVariables (+7 more)

### Community 8 - "Configuration angular.json"
Cohesion: 0.15
Nodes (12): $schema, version, newProjectRoot, projects, TGFT.Web, projectType, schematics, @schematics/angular:component (+4 more)

### Community 10 - "Outillage dev frontend"
Cohesion: 0.20
Nodes (10): cli, packageManager, devDependencies, @angular/build, @angular/cli, @angular/compiler-cli, jsdom, prettier (+2 more)

### Community 1 - "Cibles de build Angular"
Cohesion: 0.08
Nodes (26): architect, build, builder, options, browser, tsConfig, inlineStyleLanguage, assets (+18 more)

### Community 2 - "Dépendances Angular"
Cohesion: 0.10
Nodes (19): name, version, scripts, ng, start, build, watch, test (+11 more)

### Community 11 - "Composants Angular"
Cohesion: 0.33
Nodes (4): appConfig, routes, App, Component

### Community 7 - "Sources de référence et règles"
Cohesion: 0.23
Nodes (13): Cahier des charges V2 (TGFT specification), CLAUDE.md (Projet TGFT), Regle n1 : ne jamais coder sans demande explicite, Regle de mise a jour graphify (code apres chaque modif, doc/config une fois avant commit), Projet Complet.md (memoire vivante), Hierarchie des sources (CdC V2 > Projet Complet > claude.md), TGFT platform, Cahier des charges V2 (+5 more)

### Community 4 - "Noyau, isolation et tests"
Cohesion: 0.14
Nodes (15): Défis techniques et points d'intérêt (8 challenges), Noyau minimal, Groupe (tenant), Isolation multi-tenant structurelle, Roles (Fondateur, Admin, Contributeur, Lecteur), Suite de modules hostiles (M10), Clean Architecture (Domain/Application/Infrastructure/Api), IClasseurStore (port) (+7 more)

### Community 0 - "Flux d appel et salle des machines"
Cohesion: 0.10
Nodes (26): Diagram: zone de confiance / salle des machines call flow, Budget < 10 EUR/mois, un serveur x86, Bac a sable WebAssembly, Paquet, Reponse, Zone de confiance (API), Salle des machines, Classeur (SQLite par module et par groupe) (+18 more)

### Community 6 - "Pile Docker locale"
Cohesion: 0.24
Nodes (14): docker-compose.yml (tgft-platform), caddy service (127.0.0.1:8081), api service (TGFT.Api), postgres service (postgres:18-alpine), pgdata volume, POSTGRES_PASSWORD (.env), Compose project rename tgft -> tgft-platform (port 8080 -> 8081), Commandes Discord des modules externes (P3) (+6 more)

### Community 12 - "Modules natifs, externes et Discord"
Cohesion: 0.25
Nodes (9): Modules natifs, Team Builder, Encodage d'URL versionne, Tier Lists, RGPD, D11 contrat purge/anonymisation natif, B8 suppression du groupe, Data Dragon (Riot) (+1 more)

### Community 9 - "Classeur, données et exigences"
Cohesion: 0.22
Nodes (10): Modules externes, Contrats partages des modules, Manifeste, Module Tournoi (M16), Niveaux de confiance + arret d'urgence, Javy, Cycle de vie d'un module externe, Compilation attestee (M4) (+2 more)

## Knowledge Gaps
- **90 isolated node(s):** `WeatherForecast`, `$schema`, `commandName`, `dotnetRunMessages`, `launchBrowser` (+85 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **4 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `8 couches de protection` connect `Flux d appel et salle des machines` to `Classeur, données et exigences`, `Noyau, isolation et tests`, `Sources de référence et règles`?**
  _High betweenness centrality (0.078) - this node is a cross-community bridge._
- **Why does `TGFT.Web` connect `Configuration angular.json` to `Cibles de build Angular`?**
  _High betweenness centrality (0.067) - this node is a cross-community bridge._
- **Why does `devDependencies` connect `Outillage dev frontend` to `Dépendances Angular`?**
  _High betweenness centrality (0.061) - this node is a cross-community bridge._
- **Are the 2 inferred relationships involving `Classeur (SQLite par module et par groupe)` (e.g. with `Budget < 10 EUR/mois, un serveur x86` and `IClasseurStore (port)`) actually correct?**
  _`Classeur (SQLite par module et par groupe)` has 2 INFERRED edges - model-reasoned connections that need verification._
- **What connects `WeatherForecast`, `$schema`, `commandName` to the rest of the system?**
  _90 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Profils de lancement API` be split into smaller, more focused modules?**
  _Cohesion score 0.13333333333333333 - nodes in this community are weakly interconnected._
- **Should `Cibles de build Angular` be split into smaller, more focused modules?**
  _Cohesion score 0.08307692307692308 - nodes in this community are weakly interconnected._