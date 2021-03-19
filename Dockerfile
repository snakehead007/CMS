FROM mcr.microsoft.com/dotnet/aspnet:3.1
COPY bin/Release/netcoreapp3.1/publish/ CMS/
WORKDIR /CMS
ENTRYPOINT ["dotnet", "NetCore.Docker.dll"]