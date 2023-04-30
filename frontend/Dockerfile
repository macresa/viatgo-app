FROM node:18-alpine AS build
WORKDIR /usr/src/app

COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build --prod

FROM nginx:1.24.0-alpine
COPY --from=build /usr/src/app/dist/viatgo_app /usr/share/nginx/html