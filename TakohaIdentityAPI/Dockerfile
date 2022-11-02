#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TakohaIdentityAPI/TakohaIdentityAPI.csproj", "TakohaIdentityAPI/"]
RUN dotnet restore "TakohaIdentityAPI/TakohaIdentityAPI.csproj"
COPY . .
WORKDIR "/src/TakohaIdentityAPI"
RUN dotnet build "TakohaIdentityAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TakohaIdentityAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TakohaIdentityAPI.dll"]