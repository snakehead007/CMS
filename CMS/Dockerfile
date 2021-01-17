FROM mcr.microsoft.com/dotnet/aspnet:3.1
COPY bin/Release/netcoreapp3.1/ CMS/
WORKDIR /CMS
EXPOSE 44334
EXPOSE 80
ENTRYPOINT ["dotnet", "CMS.dll"]