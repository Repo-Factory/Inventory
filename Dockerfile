# -------------------- DATABASE ---------------------- #

FROM postgres:latest as database_stage
ENV POSTGRES_USER docker_postgres
ENV POSTGRES_PASSWORD docker_postgres
ENV POSTGRES_DB docker_postgres

# -------------------- FRONTEND ---------------------- #

FROM node:latest as frontend_stage
WORKDIR /home
RUN chown -R node:node ./
COPY frontend/package*.json ./
RUN npm install --silent
COPY frontend/ ./
CMD ["npm", "start"]

# -------------------- BACKEND ---------------------- #

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /home

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY backend/src/*.csproj ./
RUN dotnet restore "./tienda.csproj"
COPY backend ./
RUN dotnet build "tienda.csproj" -c Release -o /home/build

FROM build AS publish
RUN dotnet publish "tienda.csproj" -c Release -o /home/publish

FROM build AS backend_stage
WORKDIR /home
COPY --from=publish /home/publish .
ENTRYPOINT ["dotnet", "tienda.dll"]

# -------------------- SERVER ---------------------- #

FROM nginx:latest as nginx_stage
WORKDIR /usr/share/nginx/html
COPY --from=frontend_stage /home/public .
EXPOSE 80
EXPOSE 3000
EXPOSE 5000
CMD [ "nginx", "-g", "daemon off;" ]
