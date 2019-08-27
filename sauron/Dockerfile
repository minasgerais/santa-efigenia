FROM microsoft/dotnet:sdk AS sdk-env
WORKDIR /app
COPY . ./
RUN dotnet publish -c Release -o out
FROM microsoft/dotnet:aspnetcore-runtime AS runtime-env
WORKDIR /app
COPY --from=sdk-env /app/src/Sauron.Runner/out .
ENTRYPOINT ["dotnet", "Sauron.Runner.dll"]

