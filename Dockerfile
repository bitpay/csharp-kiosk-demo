FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG TARGETARCH
WORKDIR /source

RUN dotnet --version

COPY . ./

RUN dotnet restore -a $TARGETARCH

WORKDIR /source/CsharpKioskDemoDotnet
RUN dotnet publish -c Release -o /app

COPY CsharpKioskDemoDotnet/bitPayDesign.yaml /app/bitPayDesign.yaml

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./
USER $APP_UID

EXPOSE 80

ENTRYPOINT ["dotnet", "CsharpKioskDemoDotnet.dll"]