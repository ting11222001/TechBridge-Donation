# Notes

Use PostgreSQL.

<!-- --- -->

## NuGet package
```
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

Right click on "Dependencies" and install `Npgsql.EntityFrameworkCore.PostgreSQL` with version 8.0.11.

Also install 8.0.11 version of `Microsoft.EntityFrameworkCore.Tools`.

## Setup PostgreSQL database locally 

### Step 1:  Run PostgreSQL in Docker

In terminal:
```
docker run --name postgres-techbridge-donation -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=YOUR_LOCAL_DB_PASSWORD -e POSTGRES_DB=dev_techbridge_donation -p 5432:5432 -d postgres
```

What each flag does:
```
--name:         gives the container a name
-e:             sets environment variables (credentials)
-p 5432:5432:   maps container port to your machine
-d:             runs in background
```

### Step 2: Connect DBeaver to it

Open DBeaver

Click New Database Connection (plug icon top left)

Choose PostgreSQL → click Next

Fill in FieldValue: 
```
Host:       localhost
Port:       5432
Database:   dev_techbridge_donation
Username:   admin
Password:   <YOUR_LOCAL_DB_PASSWORD; refer to User Secrets section below>
```

Click Test Connection.

Click Finish. The database should show in the left panel.

<!-- --- -->

## Program.cs
```
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
```

<!-- --- -->
## User Secrets (local dev)

Stores the DB connection string outside the project — never goes to git.
```bash
# Run inside TechBridgeDonation.API/
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:TechBridgeDonationConnectionString" "Host=localhost;Port=5432;Database=dev_techbridge_donation;Username=admin;Password=YOUR_PASSWORD"
dotnet user-secrets list  # verify e.g. it prints this: "ConnectionStrings:TechBridgeDonationConnectionString = Host=localhost;Port=5432;..."
```

Secrets are saved to:
`C:\Users\YOUR_NAME\AppData\Roaming\Microsoft\UserSecrets\`

`appsettings.json` keeps an empty connection string. .NET merges both at runtime.

<!-- --- -->

## Run EF Core migrations

Go to Tools → NuGet Package Manager → Open "Package Manager Console":
```
Add-Migration "Initial Migration"
```

Apply changes e.g. creating or updating database tables:
```
Update-Database
```

The tables are under Schemas > public > Tables:
```
dev_techbridge_donation
  └── Schemas
        └── public
              └── Tables   ← your tables are here
                    ├── Organisations
                    ├── Donations
                    ├── Devices
                    └── __EFMigrationsHistory
```