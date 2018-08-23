# FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
# WORKDIR /app
# EXPOSE 50114
# EXPOSE 44356

# FROM microsoft/dotnet:2.1-sdk AS build
# WORKDIR /src
# COPY ["Pioneer.Blog/Pioneer.Blog.csproj", "Pioneer.Blog/"]
# COPY ["Pioneer.Blog.Model/Pioneer.Blog.Model.csproj", "Pioneer.Blog.Model/"]
# COPY ["Pioneer.Blog.Service/Pioneer.Blog.Service.csproj", "Pioneer.Blog.Service/"]
# COPY ["Pioneer.Blog.Repository/Pioneer.Blog.Repository.csproj", "Pioneer.Blog.Repository/"]
# COPY ["Pioneer.Blog.Entity/Pioneer.Blog.Entity.csproj", "Pioneer.Blog.Entity/"]
# RUN dotnet restore "Pioneer.Blog/Pioneer.Blog.csproj"
# COPY . .
# WORKDIR "/src/Pioneer.Blog"
# RUN dotnet build "Pioneer.Blog.csproj" -c Release -o /app

# FROM build AS publish
# RUN dotnet publish "Pioneer.Blog.csproj" -c Release -o /app

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app .
# ENTRYPOINT ["dotnet", "Pioneer.Blog.dll"]

FROM microsoft/dotnet:2.1-aspnetcore-runtime

WORKDIR /app
COPY bin/Debug/netcoreapp2.1/publish .

ENTRYPOINT ["dotnet", "Pioneer.Blog.dll"]