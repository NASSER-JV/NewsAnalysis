FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV TZ=America/Sao_Paulo
ENV LANG pt_BR.UTF-8
ENV LANGUAGE ${LANG}
ENV LC_ALL ${LANG}

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NewsAnalysis.API/NewsAnalysis.API.csproj", "NewsAnalysis.API/"]
RUN dotnet restore "NewsAnalysis.API/NewsAnalysis.API.csproj"
COPY . .
WORKDIR "/src/NewsAnalysis.API"
RUN dotnet build "NewsAnalysis.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewsAnalysis.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsAnalysis.API.dll"]
