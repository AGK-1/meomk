FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем весь проект
COPY . .

# Находим файл проекта и используем его для восстановления
RUN find . -name "*.csproj" | xargs -I {} dotnet restore {}

# Находим и собираем проект
RUN find . -name "*.csproj" | xargs -I {} dotnet build {} -c Release -o /app/build

FROM build AS publish
# Находим и публикуем проект
RUN find . -name "*.csproj" | xargs -I {} dotnet publish {} -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Определяем DLL на основе сборки
ENTRYPOINT ["sh", "-c", "dotnet $(find . -name *.dll | grep -v 'runtimeconfig\|refs' | head -1)"]
