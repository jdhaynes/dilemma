FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src


COPY *.csproj ./



COPY ["DilemmaSvc.WebApi/DilemmaSvc.WebApi.csproj", "DilemmaSvc.WebApi/"]
COPY . .
COPY .. .
COPY ../../common/dotnet/Common.Application .
COPY ../../common/dotnet/Common.Domain .
COPY ../../common/dotnet/Common.Infrastructure .
RUN dotnet restore "DilemmaSvc.WebApi/DilemmaSvc.WebApi.csproj"
WORKDIR "/src/DilemmaSvc.WebApi"
RUN dotnet build "DilemmaSvc.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DilemmaSvc.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DilemmaSvc.WebApi.dll"]
