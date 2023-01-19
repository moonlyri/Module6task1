# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY . /src
WORKDIR /src/Catalog/Catalog.Host
RUN dotnet publish -c Release -o /app 


# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Catalog.Host.dll"]

#nginx
FROM nginx:alpine
COPY nginx.conf /etc/nginx/nginx.conf
RUN apk update && apk add bash
ENTRYPOINT ["dotnet", "Catalog.Host.dll"]
