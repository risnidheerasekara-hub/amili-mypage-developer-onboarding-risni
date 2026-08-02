FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Amili.Myapp.Todo.Service.API/Amili.Myapp.Todo.Service.API.csproj Amili.Myapp.Todo.Service.API/
COPY Amili.Myapp.Todo.Service.Core/Amili.Myapp.Todo.Service.Core.csproj Amili.Myapp.Todo.Service.Core/
COPY Amili.Myapp.Todo.Service.Implementation/Amili.Myapp.Todo.Service.Implementation.csproj Amili.Myapp.Todo.Service.Implementation/

RUN dotnet restore Amili.Myapp.Todo.Service.API/Amili.Myapp.Todo.Service.API.csproj

COPY Amili.Myapp.Todo.Service.API/ Amili.Myapp.Todo.Service.API/
COPY Amili.Myapp.Todo.Service.Core/ Amili.Myapp.Todo.Service.Core/
COPY Amili.Myapp.Todo.Service.Implementation/ Amili.Myapp.Todo.Service.Implementation/

RUN dotnet publish Amili.Myapp.Todo.Service.API/Amili.Myapp.Todo.Service.API.csproj \
    -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Amili.Myapp.Todo.Service.API.dll"]
