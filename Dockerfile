#################################  api build  ################################

# dotnet build
FROM microsoft/dotnet:2.1-sdk-alpine3.7 AS api

# Restore packages
WORKDIR /src
COPY src/Homebank.Api/Homebank.Api.csproj .
RUN dotnet restore Homebank.Api.csproj
# End restore

# Build
COPY src/Homebank.Api/ .
RUN dotnet build Homebank.Api.csproj -c Release -o /app
# End build

# Publish
RUN dotnet publish Homebank.Api.csproj -c Release -o /app
# End publish

#################################  elm build  #################################

# elm build
FROM wunsh/alpine-elm:0.19-alpine3.8 AS web

# Build
WORKDIR /src
COPY src/Homebank.Web/ .
RUN elm make src/Main.elm
# End build

################################# final build #################################
FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine3.7 AS final

WORKDIR /app
EXPOSE 80

COPY --from=api /app .
COPY --from=web /src/index.html ./wwwroot/

ENTRYPOINT ["dotnet", "Homebank.Api.dll"]