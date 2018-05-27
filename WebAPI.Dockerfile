FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80


FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY WebAPI.sln ./
COPY WebAPI/WebAPI.csproj WebAPI/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/WebAPI
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebAPI.dll"]

# docker build -t webapi -f WebAPI.Dockerfile .
# docker run -d -p 3001:80 --name webapi webapi


# docker tag webapi  acmfimages.azurecr.io/webapi 
# docker push acmfimages.azurecr.io/webapi 
