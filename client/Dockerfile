FROM node:20-alpine as build

WORKDIR /app

COPY package.json package-lock.json ./
RUN npm install --frozen-lockfile --verbose

COPY . .
RUN npm run build --verbose

FROM nginx:stable-alpine
COPY --from=build /app/.next /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;", "-g", "error_log /var/log/nginx/error.log info;", "-g", "access_log /var/log/nginx/access.log;"]