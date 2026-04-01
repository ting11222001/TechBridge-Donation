# Notes

Use PostgreSQL.

## NuGet package
```
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

Right click on "Dependencies" and install Npgsql.EntityFrameworkCore.PostgreSQL with version 8.0.11

Also install 8.0.11 for Microsoft.EntityFrameworkCore.Tools

## Setup PostgreSQL database locally 

### Step 1:  Run PostgreSQL in Docker

In terminal:
```
docker run --name postgres-techbridge-donation -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=password123 -e POSTGRES_DB=mydb -p 5432:5432 -d postgres
```

What each flag does:
```
--name — gives the container a name
-e — sets environment variables (credentials)
-p 5432:5432 — maps container port to your machine
-d — runs in background
```

### Step 2: Connect DBeaver to it

Open DBeaver

Click New Database Connection (plug icon top left)

Choose PostgreSQL → click Next

Fill in FieldValue: 
```
Host: localhost
Port: 5432
Database: postgres
Username: admin
Password: password123
```

Click Test Connection.

Click Finish. The database should show in the left panel.

## Program.cs
```
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
```