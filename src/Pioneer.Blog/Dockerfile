# 1) Build Project
FROM microsoft/aspnetcore-build AS builder
WORKDIR /source

# caches restore result by copying csproj file separately
COPY *.csproj .
RUN dotnet restore

# copies the rest of your code
COPY . .
RUN dotnet publish --output /app/ --configuration Release

 # 2) Build runtime image
 FROM microsoft/aspnetcore
 WORKDIR /app
 COPY --from=builder /app .
 ENTRYPOINT ["dotnet", "Pioneer.Blog.dll"]

 # Build image
 # docker build -t pioneercode/pioneer.blog .
 #
 # Start container
 # docker run --rm -it -p 8080:80 pioneercode/pioneer-blog

