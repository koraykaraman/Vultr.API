# Vultr.API - Simple .NET Vultr API Client
### [Official Site][1]

.NET library to manage Vultr API currently supports;

* /v1/account/...
* /v1/app/...
* /v1/auth/...
* /v1/backup/...
* /v1/server/...

### Features

* Assemblies for .NET 4.5.2
* Easy installation using [NuGet](https://www.nuget.org/packages/Vultr/) for most .NET flavors

###Example
```vb
Dim Client As VultrClient = New VultrClient("YOUR-API-KEY-FROM-Vultr.com")
Dim Account As AccountResult = Client.Account.GetInfo()
Dim Applications As ApplicationResult = Client.Application.GetApplications()
Dim Backups As BackupResult = Client.Backup.GetBackups()
Dim Servers As ServerResult = Client.Server.GetServers()
```

```csharp
VultrClient Client = new VultrClient("YOUR-API-KEY-FROM-Vultr.com");
AccountResult Account = Client.Account.GetInfo();
ApplicationResult Applications = Client.Application.GetApplications();
BackupResult Backups = Client.Backup.GetBackups();
ServerResult Servers = Client.Server.GetServers();
```

 
  [1]: https://koraykaraman.com/project/1764/Vultr.API/