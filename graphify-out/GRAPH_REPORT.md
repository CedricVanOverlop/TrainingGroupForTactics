# Graph Report - .  (2026-10-26)

## Corpus Check
- 1 files · ~21,778 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 309 nodes · 364 edges · 26 communities (17 shown, 9 thin omitted)
- Extraction: 96% EXTRACTED · 4% INFERRED · 0% AMBIGUOUS · INFERRED: 14 edges (avg confidence: 0.8)
- Token cost: 88,012 input · 0 output

## Community Hubs (Navigation)
- [[_COMMUNITY_Sources de référence et user stories|Sources de référence et user stories]]
- [[_COMMUNITY_Contrats des modules et cadre légal|Contrats des modules et cadre légal]]
- [[_COMMUNITY_Cibles de build Angular|Cibles de build Angular]]
- [[_COMMUNITY_Configuration Angular CLI|Configuration Angular CLI]]
- [[_COMMUNITY_Stack et couches de protection|Stack et couches de protection]]
- [[_COMMUNITY_Règles de travail et noyau|Règles de travail et noyau]]
- [[_COMMUNITY_Cahier des charges V2.1 et auth|Cahier des charges V2.1 et auth]]
- [[_COMMUNITY_Dépendances Angular|Dépendances Angular]]
- [[_COMMUNITY_Classeurs, RGPD et purge|Classeurs, RGPD et purge]]
- [[_COMMUNITY_Profils de lancement API|Profils de lancement API]]
- [[_COMMUNITY_Tests d architecture (code)|Tests d architecture (code)]]
- [[_COMMUNITY_Projets .NET (csproj)|Projets .NET (csproj)]]
- [[_COMMUNITY_Données de jeu et contraintes|Données de jeu et contraintes]]
- [[_COMMUNITY_Isolation multi-tenant|Isolation multi-tenant]]
- [[_COMMUNITY_Composants Angular|Composants Angular]]
- [[_COMMUNITY_Intégration continue (CI)|Intégration continue (CI)]]
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
3. `Decisions tranchees` - 9 edges
4. `TGFT Platform` - 8 edges
5. `TGFT.Web` - 7 edges
6. `Modules externes` - 7 edges
7. `Salle des machines` - 7 edges
8. `http` - 6 edges
9. `https` - 6 edges
10. `options` - 6 edges

## Surprising Connections (you probably didn't know these)
- `Cahier des charges V2 (TGFT specification)` --references--> `TGFT Platform`  [EXTRACTED]
  docs/Cedric_Van_Overlop_-_Cahier_des_Charges_Projet_integration_V2.pdf → docs/Projet Complet.md
- `Diagram: zone de confiance / salle des machines call flow` --references--> `Salle des machines`  [EXTRACTED]
  docs/Cedric_Van_Overlop_-_Cahier_des_Charges_Projet_integration_V2.pdf → docs/Projet Complet.md
- `CLAUDE.md — Projet TGFT instructions` --references--> `Cahier des charges V2`  [EXTRACTED]
  CLAUDE.md → docs/Projet Complet.md
- `CLAUDE.md — Projet TGFT instructions` --references--> `Decisions tranchees`  [INFERRED]
  CLAUDE.md → docs/Projet Complet.md
- `V2.1 §2: Export added to module contract (D11)` --semantically_similar_to--> `Personal data lifecycle in modules (purge/anonymise/export table)`  [INFERRED] [semantically similar]
  docs/Cahier des charges — évolutions/V2.1.md → docs/TGFT — Plan du noyau.md

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Marketplace call flow paquet to reponse** — docs_projet_complet_zone_de_confiance, docs_projet_complet_paquet, docs_projet_complet_salle_des_machines, docs_projet_complet_reponse, docs_projet_complet_classeur [EXTRACTED 1.00]
- **Eight independent protection layers** — docs_projet_complet_couche_1_bac_a_sable_webassembly, docs_projet_complet_couche_2_salle_des_machines_separee, docs_projet_complet_couche_3_reponse_verifiee, docs_projet_complet_couche_4_classeur_par_module_et_par_groupe, docs_projet_complet_couche_5_pseudonymes, docs_projet_complet_couche_6_interface_declarative, docs_projet_complet_couche_7_niveaux_de_confiance, docs_projet_complet_couche_8_suite_de_modules_hostiles [EXTRACTED 1.00]
- **Two-rampart multi-tenant isolation** — docs_projet_complet_ef_core_et_dapper, docs_projet_complet_row_level_security, docs_projet_complet_groupe_en_cours, docs_projet_complet_chaine_d_une_requete, docs_projet_complet_isolation_multi_tenant_structurelle [EXTRACTED 1.00]
- **Two independent ramparts for tenant isolation** — docs_tgft___plan_du_noyau_groupe_en_cours, docs_tgft___plan_du_noyau_ef_core_global_filters, docs_tgft___plan_du_noyau_postgresql_rls, docs_tgft___plan_du_noyau_deux_roles_postgresql, docs_tgft___plan_du_noyau_tests_noyau [EXTRACTED 1.00]
- **V2.1 module access to platform data with member rights and role floor** — docs_cahier_des_charges____volutions_v2_1_evolution_acces_modules_donnees, docs_projet_complet_acces_donnees_plateforme, docs_tgft___plan_du_noyau_principe_pas_de_porte, docs_tgft___plan_du_noyau_seuil_plancher, docs_projet_complet_reponse_verifiee [EXTRACTED 1.00]
- **Pile locale tgft-platform (caddy -> api -> postgres)** — docker_compose_caddy_service, docker_compose_api_service, docker_compose_postgres_service, docker_compose_pgdata_volume [EXTRACTED 1.00]

## Communities (26 total, 9 thin omitted)

### Community 0 - "Sources de référence et user stories"
Cohesion: 0.08
Nodes (29): Cahier des charges V2 (TGFT specification), Défis techniques et points d'intérêt (8 challenges), Acces des modules aux donnees de la plateforme, Adhesion, Bot Discord epiques J K L, Cahier des charges V2, Cahier des charges V2.1, CLAUDE.md (+21 more)

### Community 1 - "Contrats des modules et cadre légal"
Cohesion: 0.09
Nodes (27): Cadre Riot Games, Compos, Contrats partages des modules, Couche 8 Suite de modules hostiles, DCO, Deux modules natifs engages, Epique D Systeme de modules, Epique E Team Builder (+19 more)

### Community 2 - "Cibles de build Angular"
Cohesion: 0.08
Nodes (26): build, serve, test, builder, configurations, defaultConfiguration, options, development (+18 more)

### Community 3 - "Configuration Angular CLI"
Cohesion: 0.09
Nodes (22): cli, packageManager, newProjectRoot, projects, TGFT.Web, $schema, style, @schematics/angular:component (+14 more)

### Community 4 - "Stack et couches de protection"
Cohesion: 0.11
Nodes (21): Diagram: zone de confiance / salle des machines call flow, Angular, Caddy, Clean Architecture, Couche 1 Bac a sable WebAssembly, Couche 2 Salle des machines separee, Couche 6 Interface declarative, Docker Compose (+13 more)

### Community 5 - "Règles de travail et noyau"
Cohesion: 0.10
Nodes (20): CLAUDE.md — Projet TGFT instructions, Graphify update once before commit, Règle n°1: never code without explicit request, Arret d'urgence, ASP.NET Core, CI bloquante, Compilation attestee depuis le depot public, Cookie de session et cle de service (+12 more)

### Community 6 - "Cahier des charges V2.1 et auth"
Cohesion: 0.12
Nodes (20): Cahier des charges V2.1 (évolutions), V2.1 §1: External module access to platform data, V2.1 §2: Export added to module contract (D11), V2.1 §3: Base modules enabled by default, Session cookie (site) + service key (bot), Request isolation chain (group in URL, membership check, groupe en cours), Two PostgreSQL roles (owner vs application), forced RLS, Personal data lifecycle in modules (purge/anonymise/export table) (+12 more)

### Community 7 - "Dépendances Angular"
Cohesion: 0.10
Nodes (19): dependencies, @angular/common, @angular/compiler, @angular/core, @angular/forms, @angular/platform-browser, @angular/router, rxjs (+11 more)

### Community 8 - "Classeurs, RGPD et purge"
Cohesion: 0.14
Nodes (18): Appels serialises par module et groupe, Atlassian Forge SQL, B8 Supprimer le groupe, Classeur, Contrat D11 anonymisation purge export, Couche 3 Reponse verifiee, Couche 4 Classeur par module et par groupe, Couche 5 Pseudonymes (+10 more)

### Community 9 - "Profils de lancement API"
Cohesion: 0.13
Nodes (15): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, applicationUrl, commandName (+7 more)

### Community 10 - "Tests d architecture (code)"
Cohesion: 0.27
Nodes (6): Assembly, TGFT.ArchitectureTests, Fact, string, TestResult, LayerDependencyTests

### Community 11 - "Projets .NET (csproj)"
Cohesion: 0.23
Nodes (11): TGFT.Api, TGFT.Application, TGFT.Domain, TGFT.Infrastructure, net10.0, Microsoft.NET.Test.Sdk (17.14.1), NetArchTest.Rules (1.3.2), xunit (2.9.3) (+3 more)

### Community 12 - "Données de jeu et contraintes"
Cohesion: 0.20
Nodes (11): Budget hebergement moins de 10 euros, Cache memoire et disque, Data Dragon, Encodage versionne du plateau dans l'URL, ImageSharp, Instance unique multi-tenant, Pas de conteneur ou base par module, Q1 Format d'encodage du plateau (+3 more)

### Community 13 - "Isolation multi-tenant"
Cohesion: 0.24
Nodes (10): Chaine d'une requete, EF Core et Dapper, EF Core Migrations, Groupe, Groupe en cours, Isolation multi-tenant structurelle, Pas de groupe dans un en-tete HTTP, PostgreSQL (+2 more)

### Community 14 - "Composants Angular"
Cohesion: 0.33
Nodes (4): Component, App, appConfig, routes

### Community 15 - "Intégration continue (CI)"
Cohesion: 0.38
Nodes (7): CI workflow (ci.yml), CI backend job, CI frontend job, frontend npm package (package-lock.json), Tests are the security mechanism (never bypass CI jobs), TGFT.slnx solution, Vulnerable packages check

### Community 16 - "Pile Docker locale"
Cohesion: 0.60
Nodes (6): docker-compose.yml (tgft-platform), api service (TGFT.Api), caddy service (127.0.0.1:8081), pgdata volume, POSTGRES_PASSWORD (.env), postgres service (postgres:18-alpine)

## Knowledge Gaps
- **108 isolated node(s):** `WeatherForecast`, `$schema`, `commandName`, `dotnetRunMessages`, `launchBrowser` (+103 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **9 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Modules natifs` connect `Contrats des modules et cadre légal` to `Cahier des charges V2.1 et auth`?**
  _High betweenness centrality (0.063) - this node is a cross-community bridge._
- **Why does `Cahier des charges V2.1 (évolutions)` connect `Cahier des charges V2.1 et auth` to `Sources de référence et user stories`?**
  _High betweenness centrality (0.053) - this node is a cross-community bridge._
- **Why does `Decisions tranchees` connect `Règles de travail et noyau` to `Sources de référence et user stories`, `Stack et couches de protection`, `Classeurs, RGPD et purge`, `Données de jeu et contraintes`, `Isolation multi-tenant`?**
  _High betweenness centrality (0.051) - this node is a cross-community bridge._
- **What connects `WeatherForecast`, `$schema`, `commandName` to the rest of the system?**
  _117 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Sources de référence et user stories` be split into smaller, more focused modules?**
  _Cohesion score 0.0812807881773399 - nodes in this community are weakly interconnected._
- **Should `Contrats des modules et cadre légal` be split into smaller, more focused modules?**
  _Cohesion score 0.08831908831908832 - nodes in this community are weakly interconnected._
- **Should `Cibles de build Angular` be split into smaller, more focused modules?**
  _Cohesion score 0.08307692307692308 - nodes in this community are weakly interconnected._