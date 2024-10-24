# HostingService.Host

#### 介绍
主要功能为服务保活,手动运行此程序或通过服务启动,需要保证持续运行的程序配置在配置文件中,
通过检测进程名称检测对应的程序是否正确运行。支持跳过GAC - 服务模式下启动桌面程序。

#### 软件架构
基于.net5

#### 使用说明
配置示例如下,可以唤起普通程序也可以配置系统服务的启动命令
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
        "ProcessName": "hiprint.exe"
      }
    ]
  }
```


#### 参与贡献

1.  Fork 本仓库
2.  新建 Feat_xxx 分支
3.  提交代码
4.  新建 Pull Request
