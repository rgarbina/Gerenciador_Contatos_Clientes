#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 7000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Gerenciador_Contatos_Clientes_Front/Gerenciador_Contatos_Clientes_Front.csproj", "Gerenciador_Contatos_Clientes_Front/"]
RUN dotnet restore "./Gerenciador_Contatos_Clientes_Front/Gerenciador_Contatos_Clientes_Front.csproj"
COPY . .
WORKDIR "/src/Gerenciador_Contatos_Clientes_Front"
RUN dotnet build "./Gerenciador_Contatos_Clientes_Front.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Gerenciador_Contatos_Clientes_Front.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gerenciador_Contatos_Clientes_Front.dll"]