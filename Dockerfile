FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS publish
WORKDIR "/src"
COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS run
WORKDIR /app
EXPOSE 80 443
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Todo.Api.dll"]
