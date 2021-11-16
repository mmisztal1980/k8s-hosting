#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["K8SHosting.NET6.GraphQL/K8SHosting.NET6.GraphQL.csproj", "K8SHosting.NET6.GraphQL/"]
RUN dotnet restore "K8SHosting.NET6.GraphQL/K8SHosting.NET6.GraphQL.csproj"
COPY . .
WORKDIR "/src/K8SHosting.NET6.GraphQL"
RUN dotnet build "K8SHosting.NET6.GraphQL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "K8SHosting.NET6.GraphQL.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "K8SHosting.NET6.GraphQL.dll"]