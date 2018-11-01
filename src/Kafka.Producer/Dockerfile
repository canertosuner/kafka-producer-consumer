FROM microsoft/dotnet:2.1-sdk as build
WORKDIR /app/message
# Copy csproj and restore as distinct layers
COPY Kafka.Message/*.csproj ./
RUN dotnet restore

WORKDIR /app/producer
# Copy csproj and restore as distinct layers
COPY Kafka.Producer/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./..
WORKDIR /app
RUN dotnet publish -c Release -o ../out

# Build runtime image
FROM microsoft/dotnet:2.1-runtime
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "Kafka.Producer.dll"]