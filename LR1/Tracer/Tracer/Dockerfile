FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Tracer/Tracer.csproj", "Tracer/"]
RUN dotnet restore "Tracer/Tracer.csproj"
COPY . .
WORKDIR "/src/Tracer"
RUN dotnet build "Tracer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tracer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tracer.dll"]
