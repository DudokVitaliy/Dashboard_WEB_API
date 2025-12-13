# Stage 1: Build React
FROM node:22.21-alpine AS build

WORKDIR /app

COPY package*.json ./
RUN npm ci

COPY . .
RUN npm run build

# Stage 2: Serve static files with Caddy
FROM caddy:2-alpine

# Копіюємо зібраний фронтенд
COPY --from=build /app/dist /usr/share/caddy

# Caddy стартує автоматично
EXPOSE 80
EXPOSE 443