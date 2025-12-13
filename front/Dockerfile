FROM node:22.21-alpine AS build

WORKDIR /app

COPY package*.json .

RUN npm ci

RUN npm i -g serve

COPY . .

ARG API_URL
ARG IMAGES_URL

ENV VITE_API_BASE_URL=$API_URL
ENV VITE_IMAGES_URL=$IMAGES_URL

RUN npm run build

EXPOSE 5173

CMD [ "serve", "-s", "dist", "-p", "5173" ]