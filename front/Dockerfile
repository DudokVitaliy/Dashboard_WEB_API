# Stage 1: Build React (Vite)
FROM node:22.21-alpine AS build

WORKDIR /app

# 🔹 Оголошуємо build arguments
ARG VITE_API_BASE_URL
ARG VITE_IMAGES_URL

# 🔹 Робимо їх доступними для Vite
ENV VITE_API_BASE_URL=$VITE_API_BASE_URL
ENV VITE_IMAGES_URL=$VITE_IMAGES_URL

COPY package*.json ./
RUN npm ci

COPY . .
RUN npm run build

# Stage 2: Serve static files with Caddy
FROM caddy:2-alpine

COPY --from=build /app/dist /usr/share/caddy

EXPOSE 80
EXPOSE 443
