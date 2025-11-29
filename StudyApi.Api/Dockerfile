FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files for restore
COPY StudyApi.sln .
COPY StudyApi.Api/StudyApi.Api.csproj StudyApi.Api/
COPY StudyApi.Application/StudyApi.Application.csproj StudyApi.Application/
COPY StudyApi.Domain/StudyApi.Domain.csproj StudyApi.Domain/
COPY StudyApi.Infrastructure/StudyApi.Infrastructure.csproj StudyApi.Infrastructure/

RUN dotnet restore StudyApi.Api/StudyApi.Api.csproj

# Copy the rest of the source and publish
COPY . .
WORKDIR /src/StudyApi.Api
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "StudyApi.Api.dll"]

