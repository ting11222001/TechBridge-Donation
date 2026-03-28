# Notes

## NuGet package
```
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

## Program.cs
```
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
```