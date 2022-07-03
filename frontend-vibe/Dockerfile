#stage 1
FROM node:16-alpine as node
WORKDIR /app
COPY package.json .
COPY package-lock.json .
RUN npm install -g npm@8
RUN npm install
COPY . .
RUN npm run build --prod
#stage 2
FROM nginx:alpine
COPY --from=node /app/dist/frontend-vibe /usr/share/nginx/html
EXPOSE 80
