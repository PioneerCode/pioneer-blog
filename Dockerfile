# 1) Build Project
FROM microsoft/dotnet:2.1-sdk AS builder
WORKDIR /source

# caches restore result by copying csproj file separately
COPY ["src/Pioneer.Blog/Pioneer.Blog.csproj", "Pioneer.Blog/"]
COPY ["src/Pioneer.Blog.Model/Pioneer.Blog.Model.csproj", "Pioneer.Blog.Model/"]
COPY ["src/Pioneer.Blog.Service/Pioneer.Blog.Service.csproj", "Pioneer.Blog.Service/"]
COPY ["src/Pioneer.Blog.Repository/Pioneer.Blog.Repository.csproj", "Pioneer.Blog.Repository/"]
COPY ["src/Pioneer.Blog.Entity/Pioneer.Blog.Entity.csproj", "Pioneer.Blog.Entity/"]

WORKDIR /source/Pioneer.Blog
RUN dotnet restore

# copies the rest of your code
COPY . .
RUN dotnet publish --output /app/ --configuration Release

 # 2) Build runtime image
 FROM microsoft/dotnet:2.1-aspnetcore-runtime
 WORKDIR /source
 COPY --from=builder /app .
 ENTRYPOINT ["dotnet", "Pioneer.Blog.dll"]

 # Build image
 # docker build -t pioneercode/pioneer-blog .
 #
 # Start container
 # docker run --rm -it -p 8080:80 pioneercode/pioneer.blog

