# Graph Report - .  (2026-10-15)

## Corpus Check
- 2 files · ~12,968 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 201 nodes · 259 edges · 16 communities (12 shown, 4 thin omitted)
- Extraction: 93% EXTRACTED · 7% INFERRED · 0% AMBIGUOUS · INFERRED: 19 edges (avg confidence: 0.83)
- Token cost: 111,287 input · 0 output

## Community Hubs (Navigation)
- [[_COMMUNITY_Cibles de build Angular|Cibles de build Angular]]
- [[_COMMUNITY_Classeur, données et exigences|Classeur, données et exigences]]
- [[_COMMUNITY_Sources de référence et règles|Sources de référence et règles]]
- [[_COMMUNITY_Pile Docker locale|Pile Docker locale]]
- [[_COMMUNITY_Dépendances Angular|Dépendances Angular]]
- [[_COMMUNITY_Profils de lancement API|Profils de lancement API]]
- [[_COMMUNITY_Projets .NET (csproj)|Projets .NET (csproj)]]
- [[_COMMUNITY_Clean Architecture et ports|Clean Architecture et ports]]
- [[_COMMUNITY_Configuration angular.json|Configuration angular.json]]
- [[_COMMUNITY_Modules natifs, externes et Discord|Modules natifs, externes et Discord]]
- [[_COMMUNITY_Outillage dev frontend|Outillage dev frontend]]
- [[_COMMUNITY_Composants Angular|Composants Angular]]
- [[_COMMUNITY_Template API WeatherForecast|Template API WeatherForecast]]
- [[_COMMUNITY_README frontend|README frontend]]
- [[_COMMUNITY_Template racine Angular|Template racine Angular]]
- [[_COMMUNITY_Routage Angular|Routage Angular]]

## God Nodes (most connected - your core abstractions)
1. `Classeur (SQLite par module et par groupe)` - 9 edges
2. `Les 8 couches de protection` - 9 edges
3. `Salle des machines` - 8 edges
4. `TGFT.Web` - 7 edges
5. `CLAUDE.md (Projet TGFT)` - 7 edges
6. `Clean Architecture (Domain/Application/Infrastructure/Api)` - 7 edges
7. `http` - 6 edges
8. `https` - 6 edges
9. `options` - 6 edges
10. `development` - 6 edges

## Surprising Connections (you probably didn't know these)
- `api service` --implements--> `ASP.NET Core (C#)`  [INFERRED]
  docker-compose.yml → docs/Projet Complet.md
- `postgres service (postgres:18-alpine)` --implements--> `PostgreSQL Row Level Security`  [INFERRED]
  docker-compose.yml → docs/Projet Complet.md
- `Cahier des charges V2` --references--> `Cahier des charges V2 (TGFT specification)`  [INFERRED]
  docs/Projet Complet.md → docs/Cedric_Van_Overlop_-_Cahier_des_Charges_Projet_integration_V2.pdf
- `Diagram: zone de confiance / salle des machines call flow` --references--> `Salle des machines`  [EXTRACTED]
  docs/Cedric_Van_Overlop_-_Cahier_des_Charges_Projet_integration_V2.pdf → docs/Projet Complet.md
- `Diagram: zone de confiance / salle des machines call flow` --references--> `Zone de confiance (API)`  [EXTRACTED]
  docs/Cedric_Van_Overlop_-_Cahier_des_Charges_Projet_integration_V2.pdf → docs/Projet Complet.md

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Deroule d'un appel de module externe (paquet -> salle des machines -> reponse verifiee)** — docs_projet_complet_zone_de_confiance, docs_projet_complet_paquet, docs_projet_complet_salle_des_machines, docs_projet_complet_wasm_sandbox, docs_projet_complet_reponse, docs_projet_complet_classeur [EXTRACTED 1.00]
- **Tests bloquants en CI comme mecanisme de securite** — docs_projet_complet_tests_as_security, docs_projet_complet_hostile_module_suite, docs_projet_complet_netarchtest_boundaries, docs_projet_complet_testcontainers, docs_projet_complet_multi_tenant_isolation [EXTRACTED 1.00]
- **RGPD rendu simple par l'architecture (purge, anonymisation)** — docs_projet_complet_rgpd, docs_projet_complet_b8_group_deletion, docs_projet_complet_d11_native_purge_contract, docs_projet_complet_pseudonymes, docs_projet_complet_classeur [EXTRACTED 1.00]
- **Pile Docker locale (Caddy -> API -> PostgreSQL)** — docker_compose_caddy_service, docker_compose_api_service, docker_compose_postgres_service, docker_compose_pgdata_volume [EXTRACTED 1.00]

## Communities (16 total, 4 thin omitted)

### Community 0 - "Cibles de build Angular"
Cohesion: 0.08
Nodes (26): build, serve, test, builder, configurations, defaultConfiguration, options, development (+18 more)

### Community 1 - "Classeur, données et exigences"
Cohesion: 0.12
Nodes (22): Licence AGPL-3.0 + DCO, Atlassian Forge SQL, Compilation attestee depuis le depot (M4), B8 : suppression definitive du groupe, Budget < 10 EUR/mois, un serveur x86, Serialisation des appels par module et groupe, Classeur (SQLite par module et par groupe), D11 : contrat purge/anonymisation des modules natifs (+14 more)

### Community 2 - "Sources de référence et règles"
Cohesion: 0.15
Nodes (20): CLAUDE.md (Projet TGFT), Regle de mise a jour graphify (code apres chaque modif, doc/config une fois avant commit), Regle n1 : ne jamais coder sans demande explicite, Cahier des charges V2 (TGFT specification), Défis techniques et points d'intérêt (8 challenges), Projet Complet.md (memoire vivante), Cahier des charges V2, Filtres globaux EF Core (+12 more)

### Community 3 - "Pile Docker locale"
Cohesion: 0.16
Nodes (20): docker-compose.yml (pile locale tgft), api service, caddy service, pgdata volume, POSTGRES_PASSWORD (.env), postgres service (postgres:18-alpine), Diagram: zone de confiance / salle des machines call flow, Caddy (proxy inverse) (+12 more)

### Community 4 - "Dépendances Angular"
Cohesion: 0.10
Nodes (19): dependencies, @angular/common, @angular/compiler, @angular/core, @angular/forms, @angular/platform-browser, @angular/router, rxjs (+11 more)

### Community 5 - "Profils de lancement API"
Cohesion: 0.13
Nodes (15): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, applicationUrl, commandName (+7 more)

### Community 6 - "Projets .NET (csproj)"
Cohesion: 0.19
Nodes (9): net10.0, net10.0, Microsoft.NET.Sdk, net10.0, Microsoft.NET.Sdk, net10.0, Microsoft.NET.Sdk, Microsoft.AspNetCore.OpenApi (10.0.13) (+1 more)

### Community 7 - "Clean Architecture et ports"
Cohesion: 0.17
Nodes (13): Angular avec chargement differe par module, ASP.NET Core (C#), Clean Architecture (Domain/Application/Infrastructure/Api), Correctif CVE-2026-49451 (Microsoft.OpenApi), Interface declarative, IClasseurStore (port), IModuleRunner (port), Questions ouvertes (Q1-Q7) (+5 more)

### Community 8 - "Configuration angular.json"
Cohesion: 0.15
Nodes (12): newProjectRoot, projects, TGFT.Web, $schema, style, @schematics/angular:component, prefix, projectType (+4 more)

### Community 9 - "Modules natifs, externes et Discord"
Cohesion: 0.22
Nodes (11): Data Dragon (Riot), Bot Discord (Discord.Net), Commandes Discord des modules externes (P3, M14), Modules externes (JS -> Wasm), Manifeste, Modules natifs (C# + Angular), Cadre Riot Games, Contrats partages des modules (+3 more)

### Community 10 - "Outillage dev frontend"
Cohesion: 0.20
Nodes (10): cli, packageManager, devDependencies, @angular/build, @angular/cli, @angular/compiler-cli, jsdom, prettier (+2 more)

### Community 11 - "Composants Angular"
Cohesion: 0.33
Nodes (4): Component, App, appConfig, routes

## Knowledge Gaps
- **82 isolated node(s):** `WeatherForecast`, `$schema`, `commandName`, `dotnetRunMessages`, `launchBrowser` (+77 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **4 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `TGFT.Web` connect `Configuration angular.json` to `Cibles de build Angular`?**
  _High betweenness centrality (0.068) - this node is a cross-community bridge._
- **Why does `devDependencies` connect `Outillage dev frontend` to `Dépendances Angular`?**
  _High betweenness centrality (0.062) - this node is a cross-community bridge._
- **Why does `cli` connect `Outillage dev frontend` to `Configuration angular.json`?**
  _High betweenness centrality (0.058) - this node is a cross-community bridge._
- **Are the 2 inferred relationships involving `Classeur (SQLite par module et par groupe)` (e.g. with `IClasseurStore (port)` and `Pseudonymes par module`) actually correct?**
  _`Classeur (SQLite par module et par groupe)` has 2 INFERRED edges - model-reasoned connections that need verification._
- **What connects `WeatherForecast`, `$schema`, `commandName` to the rest of the system?**
  _87 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Cibles de build Angular` be split into smaller, more focused modules?**
  _Cohesion score 0.08307692307692308 - nodes in this community are weakly interconnected._
- **Should `Classeur, données et exigences` be split into smaller, more focused modules?**
  _Cohesion score 0.12121212121212122 - nodes in this community are weakly interconnected._