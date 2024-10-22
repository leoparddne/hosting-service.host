set serviceName=HostingService

sc stop   %serviceName% 
sc delete %serviceName% 

pause