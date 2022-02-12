#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Customer.Contact.Api/Customer.Contact.Api.csproj", "src/Customer.Contact.Api/"]
COPY ["src/Customer.Conatct.Infrastructure/Customer.Contact.Infrastructure.csproj", "src/Customer.Conatct.Infrastructure/"]
COPY ["src/Customer.Contact.Core/Customer.Contact.Core.csproj", "src/Customer.Contact.Core/"]
RUN dotnet restore "src/Customer.Contact.Api/Customer.Contact.Api.csproj"
COPY . .
WORKDIR "/src/src/Customer.Contact.Api"
RUN dotnet build "Customer.Contact.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Customer.Contact.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customer.Contact.Api.dll"]