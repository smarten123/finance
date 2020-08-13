FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY src/Finance.Web/Finance.Web.csproj                               src/Finance.Web/Finance.Web.csproj
COPY src/Finance.Application/Finance.Application.csproj               src/Finance.Application/Finance.Application.csproj
COPY src/Finance.Infrastructure/Finance.Infrastructure.csproj         src/Finance.Infrastructure/Finance.Infrastructure.csproj
RUN dotnet restore src/Finance.Web/Finance.Web.csproj

COPY . .
RUN dotnet publish src/Finance.Web/Finance.Web.csproj --no-restore -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /src/out .

ENV ASPNETCORE_URLS "http://+:5000"
EXPOSE 5000

RUN groupadd --system finance && useradd --no-log-init --system --gid finance finance
USER finance

ENTRYPOINT ["dotnet", "Finance.Web.dll"]
