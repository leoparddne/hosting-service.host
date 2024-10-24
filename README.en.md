# HostingService. Host

####Introduction
The main function is to keep the service active. When manually running this program or starting it through the service, it is necessary to ensure that the continuously running program is configured in the configuration file,
Check whether the corresponding program is running correctly by detecting the process name.
Support starting desktop programs in skip GAC service mode, which only takes effect when the service is started (if "NeedBypassUAC" is used in non service startup mode: true, the program will not start).

####Software Architecture
Based on. net5

####Instructions for use
The configuration example is as follows, which can invoke a normal program or configure the startup command of system services
```
"HostExe": {
"Exe": [
    {
        "ExePath": "D:\\Program Files\\Notepad--\\Notepad--.exe",
        "Parameter": "",
        "ProcessName": "Notepad--.exe"
    },
    {
        "ExePath": "sc.exe",
        "Parameter": "start nginx",
        "ProcessName": "Test.WebAPI"
    },
    {
        "ExePath": "C:\\Temp\\hiprint\\hiprint.exe",
        "Parameter": "",
        "ProcessName": "hiprint.exe",
        "NeedBypassUAC": true
    }
]
}
```


####Participate and contribute

1. Fork's own warehouse
2. Create a new Feat_xxx branch
3. Submit code
4. Create a new Pull Request
