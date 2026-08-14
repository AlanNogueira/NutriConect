FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["NutriConect.Web/NutriConect.csproj", "NutriConect.Web/"]
COPY ["NutriConect.Business/NutriConect.Business.csproj", "NutriConect.Business/"]
COPY ["NutriConect.Data/NutriConect.Data.csproj", "NutriConect.Data/"]
RUN dotnet restore "NutriConect.Web/NutriConect.csproj"

COPY . .
WORKDIR "/src/NutriConect.Web"
RUN dotnet publish "NutriConect.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "NutriConect.dll"]
