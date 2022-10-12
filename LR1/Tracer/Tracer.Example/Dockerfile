FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Tracer.Example/Tracer.Example.csproj", "Tracer.Example/"]
RUN dotnet restore "Tracer.Example/Tracer.Example.csproj"
COPY . .
WORKDIR "/src/Tracer.Example"
RUN dotnet build "Tracer.Example.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tracer.Example.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tracer.Example.dll"]
