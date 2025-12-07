FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["SecretSantaBackend.csproj", "./"] 
RUN dotnet restore "SecretSantaBackend.csproj"

COPY . .
RUN dotnet publish "SecretSantaBackend.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:$PORT

ENTRYPOINT ["dotnet", "SecretSantaBackend.dll"]