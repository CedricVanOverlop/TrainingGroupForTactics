# Graph Report - .  (2026-10-26)

## Corpus Check
- 5 files · ~21,449 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 217 nodes · 250 edges · 23 communities (14 shown, 9 thin omitted)
- Extraction: 96% EXTRACTED · 4% INFERRED · 0% AMBIGUOUS · INFERRED: 10 edges (avg confidence: 0.89)
- Token cost: 118,057 input · 0 output

## Community Hubs (Navigation)
- [[_COMMUNITY_Cibles de build Angular|Cibles de build Angular]]
- [[_COMMUNITY_Dépendances Angular|Dépendances Angular]]
- [[_COMMUNITY_Clean Architecture et marketplace|Clean Architecture et marketplace]]
- [[_COMMUNITY_Accès aux données et protection|Accès aux données et protection]]
- [[_COMMUNITY_Profils de lancement API|Profils de lancement API]]
- [[_COMMUNITY_Isolation, sécurité et tests|Isolation, sécurité et tests]]
- [[_COMMUNITY_Tests d architecture (code)|Tests d architecture (code)]]
- [[_COMMUNITY_Configuration angular.json|Configuration angular.json]]
- [[_COMMUNITY_Projets .NET (csproj)|Projets .NET (csproj)]]
- [[_COMMUNITY_Règles de travail et cahier des charges|Règles de travail et cahier des charges]]
- [[_COMMUNITY_Outillage Angular CLI|Outillage Angular CLI]]
- [[_COMMUNITY_Composants Angular|Composants Angular]]
- [[_COMMUNITY_Sources de référence et noyau|Sources de référence et noyau]]
- [[_COMMUNITY_Pile Docker locale|Pile Docker locale]]
- [[_COMMUNITY_Marqueur assembly Api|Marqueur assembly Api]]
- [[_COMMUNITY_Marqueur assembly Application|Marqueur assembly Application]]
- [[_COMMUNITY_Marqueur assembly Domain|Marqueur assembly Domain]]
- [[_COMMUNITY_Marqueur assembly Infrastructure|Marqueur assembly Infrastructure]]
- [[_COMMUNITY_Template API WeatherForecast|Template API WeatherForecast]]
- [[_COMMUNITY_Schéma utilisateurs et groupes|Schéma utilisateurs et groupes]]
- [[_COMMUNITY_README frontend|README frontend]]
- [[_COMMUNITY_Template racine Angular|Template racine Angular]]
- [[_COMMUNITY_Routage Angular|Routage Angular]]

## God Nodes (most connected - your core abstractions)
1. `TGFT.ArchitectureTests` - 11 edges
2. `LayerDependencyTests` - 9 edges
3. `8 layers of protection` - 9 edges
4. `TGFT.Web` - 7 edges
5. `CLAUDE.md — Projet TGFT instructions` - 7 edges
6. `Cahier des charges V2.1 (évolutions)` - 7 edges
7. `http` - 6 edges
8. `https` - 6 edges
9. `options` - 6 edges
10. `development` - 6 edges

## Surprising Connections (you probably didn't know these)
- `CLAUDE.md — Projet TGFT instructions` --references--> `Settled decisions — do not repropose`  [INFERRED]
  CLAUDE.md → docs/Projet Complet.md
- `CLAUDE.md — Projet TGFT instructions` --references--> `8 layers of protection`  [INFERRED]
  CLAUDE.md → docs/Projet Complet.md
- `Source hierarchy (cahier > Projet Complet > claude.md)` --references--> `CLAUDE.md — Projet TGFT instructions`  [EXTRACTED]
  docs/Projet Complet.md → CLAUDE.md
- `Cahier des charges V2 (TGFT specification)` --references--> `Marketplace (external modules)`  [EXTRACTED]
  docs/Cedric_Van_Overlop_-_Cahier_des_Charges_Projet_integration_V2.pdf → docs/Projet Complet.md
- `Cahier des charges V2 (TGFT specification)` --references--> `TGFT platform`  [EXTRACTED]
  docs/Cedric_Van_Overlop_-_Cahier_des_Charges_Projet_integration_V2.pdf → docs/Projet Complet.md

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Two independent ramparts for tenant isolation** — docs_tgft___plan_du_noyau_groupe_en_cours, docs_tgft___plan_du_noyau_ef_core_global_filters, docs_tgft___plan_du_noyau_postgresql_rls, docs_tgft___plan_du_noyau_deux_roles_postgresql, docs_tgft___plan_du_noyau_tests_noyau [EXTRACTED 1.00]
- **External module call pipeline (paquet → sandbox → verified response)** — docs_projet_complet_deroule_appel, docs_projet_complet_modele_paquet_reponse, docs_projet_complet_salle_des_machines, docs_projet_complet_wasmtime, docs_projet_complet_classeur, docs_projet_complet_reponse_verifiee [EXTRACTED 1.00]
- **V2.1 module access to platform data with member rights and role floor** — docs_cahier_des_charges____volutions_v2_1_evolution_acces_modules_donnees, docs_projet_complet_acces_donnees_plateforme, docs_tgft___plan_du_noyau_principe_pas_de_porte, docs_tgft___plan_du_noyau_seuil_plancher, docs_projet_complet_reponse_verifiee [EXTRACTED 1.00]
- **Pile locale tgft-platform (caddy -> api -> postgres)** — docker_compose_caddy_service, docker_compose_api_service, docker_compose_postgres_service, docker_compose_pgdata_volume [EXTRACTED 1.00]

## Communities (23 total, 9 thin omitted)

### Community 0 - "Cibles de build Angular"
Cohesion: 0.08
Nodes (26): build, serve, test, builder, configurations, defaultConfiguration, options, development (+18 more)

### Community 1 - "Dépendances Angular"
Cohesion: 0.10
Nodes (19): dependencies, @angular/common, @angular/compiler, @angular/core, @angular/forms, @angular/platform-browser, @angular/router, rxjs (+11 more)

### Community 2 - "Clean Architecture et marketplace"
Cohesion: 0.11
Nodes (19): Riot Games legal constraints, Clean Architecture (Domain/Application/Infrastructure/Api), Shared module contracts (manifest, activation, permissions, purge/anonymisation/export), External module lifecycle, IModuleRunner port, Javy (JS → Wasm compiler), Jint fallback plan, AGPL-3.0 licence + DCO (+11 more)

### Community 3 - "Accès aux données et protection"
Cohesion: 0.17
Nodes (18): V2.1 §1: External module access to platform data, V2.1 §2: Export added to module contract (D11), Diagram: zone de confiance / salle des machines call flow, §6.4 bis Module access to platform data, Classeur (SQLite per module per group), D11 native module data contract, 8 layers of protection, Call flow (event, preparation, execution, verification) (+10 more)

### Community 4 - "Profils de lancement API"
Cohesion: 0.13
Nodes (15): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, applicationUrl, commandName (+7 more)

### Community 5 - "Isolation, sécurité et tests"
Cohesion: 0.16
Nodes (15): Progress status (milestones), Structural multi-tenant isolation, Hostile module suite (M10), Tests are the security mechanism, Session cookie (site) + service key (bot), Request isolation chain (group in URL, membership check, groupe en cours), Two PostgreSQL roles (owner vs application), forced RLS, EF Core global query filters (first rampart) (+7 more)

### Community 6 - "Tests d architecture (code)"
Cohesion: 0.27
Nodes (6): Assembly, TGFT.ArchitectureTests, Fact, string, TestResult, LayerDependencyTests

### Community 7 - "Configuration angular.json"
Cohesion: 0.15
Nodes (12): newProjectRoot, projects, TGFT.Web, $schema, style, @schematics/angular:component, prefix, projectType (+4 more)

### Community 8 - "Projets .NET (csproj)"
Cohesion: 0.23
Nodes (11): TGFT.Api, TGFT.Application, TGFT.Domain, TGFT.Infrastructure, net10.0, Microsoft.NET.Test.Sdk (17.14.1), NetArchTest.Rules (1.3.2), xunit (2.9.3) (+3 more)

### Community 9 - "Règles de travail et cahier des charges"
Cohesion: 0.25
Nodes (11): CLAUDE.md — Projet TGFT instructions, Graphify update once before commit, Règle n°1: never code without explicit request, Cahier des charges V2.1 (évolutions), V2.1 §3: Base modules enabled by default, Cahier des charges V2 (PDF), Source hierarchy (cahier > Projet Complet > claude.md), Decision log (+3 more)

### Community 10 - "Outillage Angular CLI"
Cohesion: 0.20
Nodes (10): cli, packageManager, devDependencies, @angular/build, @angular/cli, @angular/compiler-cli, jsdom, prettier (+2 more)

### Community 11 - "Composants Angular"
Cohesion: 0.33
Nodes (4): Component, App, appConfig, routes

### Community 12 - "Sources de référence et noyau"
Cohesion: 0.29
Nodes (8): Cahier des charges V2 (TGFT specification), Défis techniques et points d'intérêt (8 challenges), Hosting budget < 10 €/month, single x86 VPS, Settled decisions — do not repropose, Marketplace (external modules), Paquet → réponse model (Shopify Functions), Noyau minimal (core), TGFT platform

### Community 13 - "Pile Docker locale"
Cohesion: 0.60
Nodes (6): docker-compose.yml (tgft-platform), api service (TGFT.Api), caddy service (127.0.0.1:8081), pgdata volume, POSTGRES_PASSWORD (.env), postgres service (postgres:18-alpine)

## Knowledge Gaps
- **95 isolated node(s):** `WeatherForecast`, `$schema`, `commandName`, `dotnetRunMessages`, `launchBrowser` (+90 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **9 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `TGFT.Web` connect `Configuration angular.json` to `Cibles de build Angular`?**
  _High betweenness centrality (0.058) - this node is a cross-community bridge._
- **Why does `devDependencies` connect `Outillage Angular CLI` to `Dépendances Angular`?**
  _High betweenness centrality (0.053) - this node is a cross-community bridge._
- **Why does `cli` connect `Outillage Angular CLI` to `Configuration angular.json`?**
  _High betweenness centrality (0.050) - this node is a cross-community bridge._
- **What connects `WeatherForecast`, `$schema`, `commandName` to the rest of the system?**
  _98 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Cibles de build Angular` be split into smaller, more focused modules?**
  _Cohesion score 0.08307692307692308 - nodes in this community are weakly interconnected._
- **Should `Dépendances Angular` be split into smaller, more focused modules?**
  _Cohesion score 0.1 - nodes in this community are weakly interconnected._
- **Should `Clean Architecture et marketplace` be split into smaller, more focused modules?**
  _Cohesion score 0.1111111111111111 - nodes in this community are weakly interconnected._