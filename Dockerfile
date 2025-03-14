FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Скопируйте все файлы проекта (включая .csproj)
COPY . .

# Найдите файл .csproj и восстановите зависимости
# Замените "*.csproj" на точное имя вашего проектного файла, если вы его знаете
RUN dotnet restore "*.csproj"

# Соберите приложение
RUN dotnet build "*.csproj" -c Release -o /app/build

FROM build AS publish
# Опубликуйте приложение
RUN dotnet publish "*.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Замените "sqli-samko.dll" на реальное имя выходного файла DLL вашего проекта
ENTRYPOINT ["dotnet", "sqli-samko.dll"]
