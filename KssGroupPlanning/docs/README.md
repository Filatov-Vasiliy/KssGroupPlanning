Чтобы накатить миграции нужно вбить в консоль
```

dotnet ef database update
```

Чтобы создать и накатить миграцию 
```
dotnet ef migrations add initial -s KssGroupPlanning -p KssGroupPlanning
cd KssGroupPlanning
dotnet ef database update

```

dotnet add package Serilog.Sinks.File
dotnet add package Serilog
dotnet add package Serilog.AspNetCore
dotnet add package Quartz.Extensions.DependencyInjection
dotnet add package Quartz
dotnet add package Quartz.Extensions.Hosting