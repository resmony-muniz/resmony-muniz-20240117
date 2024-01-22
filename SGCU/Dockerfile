ARG DOTNET_SDK=mcr.microsoft.com/dotnet/sdk:8.0

FROM $DOTNET_SDK AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM $DOTNET_SDK AS final-env
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SGCU.dll"]