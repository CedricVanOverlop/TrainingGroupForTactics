# Graph Report - .  (2026-10-21)

## Corpus Check
- 2 files · ~13,831 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 234 nodes · 291 edges · 21 communities (13 shown, 8 thin omitted)
- Extraction: 96% EXTRACTED · 4% INFERRED · 0% AMBIGUOUS · INFERRED: 11 edges (avg confidence: 0.82)
- Token cost: 93,509 input · 0 output

## Community Hubs (Navigation)
- [[_COMMUNITY_Flux d appel et salle des machines|Flux d appel et salle des machines]]
- [[_COMMUNITY_Cibles de build Angular|Cibles de build Angular]]
- [[_COMMUNITY_Pile Docker locale|Pile Docker locale]]
- [[_COMMUNITY_Configuration angular.json|Configuration angular.json]]
- [[_COMMUNITY_Dépendances Angular|Dépendances Angular]]
- [[_COMMUNITY_Profils de lancement API|Profils de lancement API]]
- [[_COMMUNITY_Tests d architecture (code)|Tests d architecture (code)]]
- [[_COMMUNITY_Projets .NET (csproj)|Projets .NET (csproj)]]
- [[_COMMUNITY_Classeur, données et exigences|Classeur, données et exigences]]
- [[_COMMUNITY_Clean Architecture et ports|Clean Architecture et ports]]
- [[_COMMUNITY_Composants Angular|Composants Angular]]
- [[_COMMUNITY_Règles de travail et graphify|Règles de travail et graphify]]
- [[_COMMUNITY_Sources de référence et marketplace|Sources de référence et marketplace]]
- [[_COMMUNITY_Marqueur assembly Api|Marqueur assembly Api]]
- [[_COMMUNITY_Marqueur assembly Application|Marqueur assembly Application]]
- [[_COMMUNITY_Marqueur assembly Domain|Marqueur assembly Domain]]
- [[_COMMUNITY_Marqueur assembly Infrastructure|Marqueur assembly Infrastructure]]
- [[_COMMUNITY_Template API WeatherForecast|Template API WeatherForecast]]
- [[_COMMUNITY_README frontend|README frontend]]
- [[_COMMUNITY_Template racine Angular|Template racine Angular]]
- [[_COMMUNITY_Routage Angular|Routage Angular]]

## God Nodes (most connected - your core abstractions)
1. `TGFT.ArchitectureTests` - 11 edges
2. `LayerDependencyTests` - 9 edges
3. `CLAUDE.md (Projet TGFT)` - 8 edges
4. `8 couches de protection` - 8 edges
5. `TGFT.Web` - 7 edges
6. `Bac a sable WebAssembly` - 7 edges
7. `Classeur (SQLite par module et par groupe)` - 7 edges
8. `Clean Architecture (Domain/Application/Infrastructure/Api)` - 7 edges
9. `tests/TGFT.ArchitectureTests (xUnit + NetArchTest, 5 regles)` - 7 edges
10. `http` - 6 edges

## Surprising Connections (you probably didn't know these)
- `docker-compose.yml (tgft-platform)` --implements--> `Docker / Docker Compose`  [INFERRED]
  docker-compose.yml → docs/Projet Complet.md
- `Défis techniques et points d'intérêt (8 challenges)` --references--> `Les tests sont la protection`  [EXTRACTED]
  docs/Cedric_Van_Overlop_-_Cahier_des_Charges_Projet_integration_V2.pdf → docs/Projet Complet.md
- `Diagram: zone de confiance / salle des machines call flow` --references--> `Salle des machines`  [EXTRACTED]
  docs/Cedric_Van_Overlop_-_Cahier_des_Charges_Projet_integration_V2.pdf → docs/Projet Complet.md
- `Diagram: zone de confiance / salle des machines call flow` --references--> `Zone de confiance (API)`  [EXTRACTED]
  docs/Cedric_Van_Overlop_-_Cahier_des_Charges_Projet_integration_V2.pdf → docs/Projet Complet.md
- `caddy service (127.0.0.1:8081)` --implements--> `Caddy (proxy inverse)`  [INFERRED]
  docker-compose.yml → docs/Projet Complet.md

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Deroule d'un appel de module externe** — docs_projet_complet_paquet, docs_projet_complet_zone_de_confiance, docs_projet_complet_salle_des_machines, docs_projet_complet_reponse, docs_projet_complet_classeur [EXTRACTED 1.00]
- **Isolation multi-tenant structurelle a double rempart** — docs_projet_complet_multi_tenant_isolation, docs_projet_complet_ef_core_global_filters, docs_projet_complet_postgresql_rls, docs_projet_complet_testcontainers [EXTRACTED 1.00]
- **Frontieres d'architecture verifiees par tests** — docs_projet_complet_clean_architecture, docs_projet_complet_netarchtest_boundaries, docs_projet_complet_architecture_tests_project, docs_projet_complet_tests_as_security [EXTRACTED 1.00]
- **Pile locale tgft-platform (caddy -> api -> postgres)** — docker_compose_caddy_service, docker_compose_api_service, docker_compose_postgres_service, docker_compose_pgdata_volume [EXTRACTED 1.00]

## Communities (21 total, 8 thin omitted)

### Community 0 - "Flux d appel et salle des machines"
Cohesion: 0.08
Nodes (34): Diagram: zone de confiance / salle des machines call flow, Licence AGPL-3.0 + DCO, Angular, chargement differe, Compilation attestee (M4), Interface declarative, Kit de developpement TGFT, GitHub Actions (CI), Suite de modules hostiles (M10) (+26 more)

### Community 1 - "Cibles de build Angular"
Cohesion: 0.08
Nodes (26): build, serve, test, builder, configurations, defaultConfiguration, options, development (+18 more)

### Community 2 - "Pile Docker locale"
Cohesion: 0.13
Nodes (23): docker-compose.yml (tgft-platform), api service (TGFT.Api), caddy service (127.0.0.1:8081), pgdata volume, POSTGRES_PASSWORD (.env), postgres service (postgres:18-alpine), Défis techniques et points d'intérêt (8 challenges), ASP.NET Core (C#) (+15 more)

### Community 3 - "Configuration angular.json"
Cohesion: 0.09
Nodes (22): cli, packageManager, newProjectRoot, projects, TGFT.Web, $schema, style, @schematics/angular:component (+14 more)

### Community 4 - "Dépendances Angular"
Cohesion: 0.10
Nodes (19): dependencies, @angular/common, @angular/compiler, @angular/core, @angular/forms, @angular/platform-browser, @angular/router, rxjs (+11 more)

### Community 5 - "Profils de lancement API"
Cohesion: 0.13
Nodes (15): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, applicationUrl, commandName (+7 more)

### Community 6 - "Tests d architecture (code)"
Cohesion: 0.27
Nodes (6): Assembly, TGFT.ArchitectureTests, Fact, string, TestResult, LayerDependencyTests

### Community 7 - "Projets .NET (csproj)"
Cohesion: 0.23
Nodes (11): TGFT.Api, TGFT.Application, TGFT.Domain, TGFT.Infrastructure, net10.0, Microsoft.NET.Test.Sdk (17.14.1), NetArchTest.Rules (1.3.2), xunit (2.9.3) (+3 more)

### Community 8 - "Classeur, données et exigences"
Cohesion: 0.21
Nodes (12): Atlassian Forge SQL (reference), B8 suppression du groupe, Budget < 10 EUR/mois, un serveur x86, Serialisation des appels, Classeur (SQLite par module et par groupe), D11 contrat purge/anonymisation natif, Pas de Redis, Paquet (+4 more)

### Community 9 - "Clean Architecture et ports"
Cohesion: 0.24
Nodes (11): tests/TGFT.ArchitectureTests (xUnit + NetArchTest, 5 regles), AssemblyMarker (classe vide par projet), Clean Architecture (Domain/Application/Infrastructure/Api), Correctif CVE-2026-49451 (Microsoft.OpenApi), IClasseurStore (port), IModuleRunner (port), Frontieres NetArchTest (D8), Depot git separe dans Desktop/TGFT (+3 more)

### Community 10 - "Composants Angular"
Cohesion: 0.33
Nodes (4): Component, App, appConfig, routes

### Community 11 - "Règles de travail et graphify"
Cohesion: 0.43
Nodes (8): CLAUDE.md (Projet TGFT), Regle de mise a jour graphify (une seule mise a jour juste avant le commit, code et doc), Regle n1 : ne jamais coder sans demande explicite, Cahier des charges V2, graphify-out versionne dans git, Decision 2026-10-21 : graphify mis a jour une seule fois avant le commit, Projet Complet.md (memoire vivante), Hierarchie des sources (CdC V2 > Projet Complet > claude.md)

### Community 12 - "Sources de référence et marketplace"
Cohesion: 0.29
Nodes (8): Cahier des charges V2 (TGFT specification), Data Dragon (Riot), Modules externes, Marketplace ouverte, Odoo (reference), Regles Riot Games, TGFT platform, Module Tournoi (M16)

## Knowledge Gaps
- **95 isolated node(s):** `WeatherForecast`, `$schema`, `commandName`, `dotnetRunMessages`, `launchBrowser` (+90 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **8 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `8 couches de protection` connect `Flux d appel et salle des machines` to `Classeur, données et exigences`, `Règles de travail et graphify`?**
  _High betweenness centrality (0.064) - this node is a cross-community bridge._
- **Why does `TGFT.Web` connect `Configuration angular.json` to `Cibles de build Angular`?**
  _High betweenness centrality (0.050) - this node is a cross-community bridge._
- **Why does `devDependencies` connect `Configuration angular.json` to `Dépendances Angular`?**
  _High betweenness centrality (0.045) - this node is a cross-community bridge._
- **What connects `WeatherForecast`, `$schema`, `commandName` to the rest of the system?**
  _98 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Flux d appel et salle des machines` be split into smaller, more focused modules?**
  _Cohesion score 0.0784313725490196 - nodes in this community are weakly interconnected._
- **Should `Cibles de build Angular` be split into smaller, more focused modules?**
  _Cohesion score 0.08307692307692308 - nodes in this community are weakly interconnected._
- **Should `Pile Docker locale` be split into smaller, more focused modules?**
  _Cohesion score 0.13043478260869565 - nodes in this community are weakly interconnected._