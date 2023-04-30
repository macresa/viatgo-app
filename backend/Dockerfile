FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /app

COPY . . 
COPY ["./Application/Application.csproj", "app/Application/"]
COPY ["./Infrastructure/Infrastructure.csproj", "app/Infrastructure/"]
COPY ["./Api/Api.csproj", "app/Api/"]
 
RUN dotnet restore "app/Api/Api.csproj"

COPY . ./
RUN dotnet publish -c Release -o output

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/output /app

RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

ENTRYPOINT [ "dotnet", "Api.dll" , "--environment=Development" ]