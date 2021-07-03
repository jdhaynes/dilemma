FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["IdentitySvc.WebApi/IdentitySvc.WebApi.csproj", "IdentitySvc.WebApi/"]
RUN dotnet restore "IdentitySvc.WebApi/IdentitySvc.WebApi.csproj"
COPY . .
COPY ../../common/dotnet/Common.Application .
COPY ../../common/dotnet/Common.Domain .
COPY ../../common/dotnet/Common.Infrastructure .
WORKDIR "/src/IdentitySvc.WebApi"
RUN dotnet build "IdentitySvc.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentitySvc.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DilemmaApp.IdentitySvc.WebApi.dll"]
