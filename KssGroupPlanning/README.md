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