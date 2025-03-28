#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PresentationLayer/PresentationLayer.csproj", "PresentationLayer/"]
RUN dotnet restore "PresentationLayer/PresentationLayer.csproj"
COPY . .
WORKDIR "/src/PresentationLayer"
RUN dotnet build "PresentationLayer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PresentationLayer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PresentationLayer.dll"]