set serviceName=HostingService
set serviceFilePath=%~dp0\HostingService.exe
set serviceDescription=HostingService

sc create %serviceName%  BinPath=%serviceFilePath%
sc config %serviceName%    start=auto  
sc description %serviceName%  %serviceDescription%
sc start  %serviceName%
pause