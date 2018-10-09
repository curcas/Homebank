#################################  api build  ################################

# dotnet build
FROM microsoft/dotnet:2.1-sdk-alpine3.7 AS api

# Restore packages
WORKDIR /src
COPY src/Homebank.Api/Homebank.Api.csproj .
RUN dotnet restore Homebank.Api.csproj
# End restore packages

# Build
COPY src/Homebank.Api/ .
RUN dotnet build Homebank.Api.csproj -c Release -o /app
# End build

# Publish
RUN dotnet publish Homebank.Api.csproj -c Release -o /app
# End publish

#################################  elm build  #################################

# elm build
FROM node:10.11.0 AS web

# Build tools
RUN npm install -g yarn@1.10.1
# End build tools

# Build
WORKDIR /src
COPY src/Homebank.Web/ .
RUN yarn install && yarn build
# End build

################################# final build #################################
FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine3.7 AS final
LABEL maintainer Curdin Caspar <curdin.caspar@gmail.com>

WORKDIR /app
EXPOSE 80

COPY --from=api /app .
COPY --from=web /src/dist ./wwwroot/

ENTRYPOINT ["dotnet", "Homebank.Api.dll"]