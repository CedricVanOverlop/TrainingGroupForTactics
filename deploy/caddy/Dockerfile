# Contexte de build : racine du dépôt (voir docker-compose.yml).

# --- Étape 1 : compilation Angular ---
FROM node:24-alpine AS build
WORKDIR /src

# package*.json d'abord : npm ci reste en cache tant que les dépendances ne changent pas.
COPY frontend/package.json frontend/package-lock.json ./
RUN npm ci

COPY frontend/ ./
RUN npx ng build

# --- Étape 2 : Caddy sert les fichiers statiques et relaie /api ---
FROM caddy:2-alpine
COPY deploy/caddy/Caddyfile /etc/caddy/Caddyfile
COPY --from=build /src/dist/TGFT.Web/browser /srv
