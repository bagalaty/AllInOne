dotnet ef  migrations add CreateDatabase

dotnet ef database update

dotnet ef migrations add LimitStrings
dotnet ef database update


Scaffold-DbContext "server=localhost;port=3306;user=root;password=P@ssw0rd159;database=allinone" MySql.Data.EntityFrameworkCore -OutputDir .\..\Services\MigrationsMySql -Schemas AllinoneContext -f

