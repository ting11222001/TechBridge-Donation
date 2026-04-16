# Notes

Use PostgreSQL.

<!-- --- -->

## NuGet package
```
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

Right click on "Dependencies" and install `Npgsql.EntityFrameworkCore.PostgreSQL` with version 8.0.11.

Also install 8.0.11 version of `Microsoft.EntityFrameworkCore.Tools`.

<!-- --- -->

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

Apply changes:
```
Update-Database
```

*If ever needed to drop and recreate the database when a model is changed or updating seed data:
```
Drop-Database           deletes the entire database, so be careful!
Update-Database         recreates the schema by running all migrations
```


The new tables are under Schemas > public > Tables:
```
dev_techbridge_donation
  └── Schemas
        └── public
              └── Tables   ← new tables are here
                    ├── Organisations
                    ├── Donations
                    ├── Devices
                    └── __EFMigrationsHistory
```

<!-- --- -->
## To create an empty API controller in Visual Studio:

Right-click the Controllers folder in TechBridgeDonation.API:
- Click Add > New Scaffolded Item
- Select API Controller - Empty
- Click Add

A second dialog opens:
- Select API Controller - Empty from the list
- Type the name (e.g. ValuesController.cs) in the Name field
- Click Add

The result is a new empty controller file with the basic [ApiController] boilerplate, no actions.

Try adding a list of values as a GET endpoint:
```csharp
using Microsoft.AspNetCore.Mvc;
using TechBridgeDonation.API.Models.Domain;

namespace TechBridgeDonation.API.Controllers
{
    // https://localhost:5001/api/organisations
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationsController : ControllerBase
    {
        // GET ALL ORGs
        // GET: https://localhost:portnumber/api/organisations
        [HttpGet]
        public IActionResult GetAll()
        {
            var orgs = new List<Organisation>
            {
                new Organisation
                {
                    Id = Guid.NewGuid(),
                    Name = "Dept. of Education",
                },
                new Organisation
                {
                    Id = Guid.NewGuid(),
                    Name = "AngliCare",
                }
            };

            return Ok(orgs);
        }
    }
}
```

Hit "https" at the top of the file to run the API, then in Swagger click try it out on the GET /api/organisations endpoint. You should see the list of orgs you created in the code.

## DI — Interface vs Implementation


### The Idea: Code Against an Interface, Not a Class

```csharp
// Interface = contract ("what it can do")
public interface IDonationRepository {
    List<Donation> GetAll();
}

// Implementation A = real database
public class SqlDonationRepository : IDonationRepository { ... }

// Implementation B = fake for testing
public class FakeDonationRepository : IDonationRepository { ... }
```


### Register in One Place (Program.cs)

```csharp
builder.Services.AddScoped<IDonationRepository, SqlDonationRepository>();
```

Read it as: **"Whenever someone asks for `IDonationRepository`, give them `SqlDonationRepository`."**

| Part | Meaning |
|---|---|
| `builder.Services` | The DI container (the staff list) |
| `AddScoped` | Register with one instance per HTTP request |
| `IDonationRepository` | What controllers ask for |
| `SqlDonationRepository` | The actual class that gets injected |


### Controllers Just Ask for the Interface

```csharp
public DonationController(IDonationRepository repo) { }
public ReportController(IDonationRepository repo) { }
```

Controllers never change. They only know about `IDonationRepository`.


### Swap Implementation in One Line

```csharp
// Before
builder.Services.AddScoped<IDonationRepository, SqlDonationRepository>();

// After - all controllers now get the fake one
builder.Services.AddScoped<IDonationRepository, FakeDonationRepository>();
```

Real world example: swap PostgreSQL to SQL Server by changing just one line in `Program.cs`.


### The Flow

```
Controller asks for IDonationRepository
        ↓
DI container checks Program.cs registration
        ↓
Creates and injects SqlDonationRepository
```


### Where DI Actually Happens — The Constructor

```csharp
public class DonationController
{
    private readonly IDonationRepository _repo;

    // THIS is DI - the framework "injects" the dependency here
    public DonationController(IDonationRepository repo)
    {
        _repo = repo;
    }
}
```

The controller doesn't create `repo`. The framework gives it (injects it).


### 3 Steps

| Step | What | Example |
|---|---|---|
| 1. Create | Write the class | `SqlDonationRepository` |
| 2. Register | Tell DI container | `AddScoped<I..., Sql...>()` |
| 3. **Inject** | Framework passes it in | Constructor parameter |

- Register = write your name on a staff list
- Inject = manager assigns you to a project automatically


### Analogy

Think of a power socket on a wall.

| | |
|---|---|
| Socket | Interface |
| Power company | Implementation |
| Your devices | Controllers |

Every device just plugs in. If the power company changes, your devices don't care.


### AddDbContext vs AddScoped

```csharp
// Simple registration - no configuration needed
builder.Services.AddScoped<IDonationRepository, SqlDonationRepository>();

// Registration WITH configuration - needs DB connection info
builder.Services.AddDbContext<TechBridgeDonationDbContext>(options =>
    options.UseNpgsql("...connection string..."));
```

`DbContext` needs extra setup so you pass a configuration lambda.
`AddScoped` doesn't need that because the implementation already knows everything.

| Part | Role |
|---|---|
| `builder.Services` | The DI container |
| `AddDbContext` | Register method |
| `TechBridgeDonationDbContext` | Service to inject |
| `options => options.UseNpgsql(...)` | How to configure/create it |

<!-- --- -->

## OrganisationsController

The controller handles HTTP requests at the route `api/organisations`.

```csharp
[Route("api/[controller]")]
[ApiController]
public class OrganisationsController : ControllerBase
```

`[controller]` is replaced by the class name prefix, so `OrganisationsController` becomes `api/organisations`.

### Injecting the DbContext

The controller needs the database context to talk to the database. Use constructor injection to pass it in.

```csharp
private readonly TechBridgeDonationDbContext dbContext;

public OrganisationsController(TechBridgeDonationDbContext dbContext)
{
    this.dbContext = dbContext;
}
```

### Visual Studio Shortcuts

#### `ctor` snippet

Type `ctor` inside the class body then press `Tab` twice. Visual Studio generates a constructor for you.

```csharp
// Type this:
ctor

// VS generates:
public OrganisationsController()
{
}
```

#### `Ctrl + .` to assign a private field

Inside the constructor parameter, place your cursor on `TechBridgeDonationDbContext`, then press `Ctrl + .`. Choose **Create and assign field**. VS generates the private field and the assignment line automatically.

```csharp
// Before Ctrl + .
public OrganisationsController(TechBridgeDonationDbContext dbContext)
{
}

// After Ctrl + .
private readonly TechBridgeDonationDbContext dbContext;

public OrganisationsController(TechBridgeDonationDbContext dbContext)
{
    this.dbContext = dbContext;
}
```

### Why use constructor injection?

ASP.NET Core's dependency injection (DI) container creates the `TechBridgeDonationDbContext` and passes it in automatically. The controller does not need to create it manually. This keeps the code loosely coupled and easy to test.

<!-- --- -->

## Seeding database

```
App starts
    ↓
MigrateAsync()     → runs only pending migrations, skips already-applied ones
    ↓
SeedAsync()        → hits if (db.Donations.Any()) 
                        → table has data? STOP, do nothing
                        → table empty? INSERT seed data
    ↓
App continues
```

<!-- --- -->

## Route constraints in Controllers

`[Route("{id:Guid}")]` tells the router to expect a value at `{id}` in the URL, e.g. `/api/organisations/abc-123-...`.

`:Guid` means it only matches if the value is a valid Guid.

<!-- --- -->


## async/await in ASP.NET Core
One-liner: 
- await does not block the thread, it frees it to handle other requests while the database query runs.

Key points:
- Database calls are slow compared to CPU work, they go over a network
- Without async, the thread sits idle during every database call
- With async, that thread handles other HTTP requests while waiting
- Each layer must be async because the "free the thread" signal must travel all the way up to ASP.NET Core's request handler

Source/context:
User's code or question snippet:
```
public async Task<IActionResult> GetById([FromRoute] Guid id)
{
    var organisationDomain = await organisationRepository.GetByIdAsync(id);
}

public async Task<Organisation?> GetByIdAsync(Guid id)
{
    return await dbContext.Organisations.FirstOrDefaultAsync(x => x.Id == id);
}
```

<!-- --- -->

## Request DTO vs Response DTO

Request DTOs (what the client sends in) don't need an ID because the ID is either generated by the database (on create) or comes from the URL route (on update/delete).

Response DTOs (what the API sends back) include the ID so the client knows which resource was created or modified, and can reference it in future requests.

For example in your TechBridge project:
```
// No ID — client is creating, DB will assign it
public class AddOrganisationRequestDto { ... }

// No ID — ID comes from the route: PUT /organisations/{id}
public class UpdateOrganisationRequestDto { ... }

// Has ID — client needs to know what was saved
public class OrganisationDto { public Guid Id { get; set; } ... }
```

<!-- --- -->

## Implement AutoMapper

Right click on "Dependencies" and install `AutoMapper` with version 16.1.1.

Create a new folder `Mappings`.

### AutoMapperProfiles

Create `Mappings/AutoMapperProfiles.cs`. This file defines how to convert between domain models and DTOs in both directions (`.ReverseMap()`):
```csharp
namespace TechBridgeDonation.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Organisation, OrganisationDto>().ReverseMap();
            CreateMap<AddOrganisationRequestDto, Organisation>().ReverseMap();
            CreateMap<UpdateOrganisationRequestDto, Organisation>().ReverseMap();
        }
    }
}
```

### Register AutoMapper in Program.cs

AutoMapper 16+ removed assembly scanning. You must register each profile explicitly using a config action:
```csharp
// Inject automapper
builder.Services.AddAutoMapper(configuration => configuration.AddProfile<AutoMapperProfiles>());
```

> In older versions (v12 and below), `AddAutoMapper()` with no arguments worked. From v13+, you had to pass an assembly. From v16+, you must pass a config action with explicit profiles.

### Use AutoMapper in the Controller

Before AutoMapper, mapping was manual:
```csharp
var organisationDTO = new OrganisationDto()
{
    Id = organisationDomain.Id,
    Name = organisationDomain.Name,
    Type = organisationDomain.Type,
    ContactEmail = organisationDomain.ContactEmail,
    ContactPhone = organisationDomain.ContactPhone,
    Address = organisationDomain.Address,
    CreatedAt = organisationDomain.CreatedAt,
    UpdatedAt = organisationDomain.UpdatedAt,
    DeletedAt = organisationDomain.DeletedAt
};
```

With AutoMapper, this becomes one line:
```csharp
// Map domain model to DTO (with AutoMapper)
var organisationDto = mapper.Map<OrganisationDto>(organisationDomain);
```

AutoMapper reads the profile and handles the property copying automatically.


<!-- --- -->

## 

In TechBridgeDonationDbContext, add this:

```
// Another way to seed data
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Seed data for Organisations
    var organisations = new List<Organisation>
    {
        new Organisation
        {
            Id = Guid.NewGuid(),
            Name = "Adelaide Electronics Pty Ltd",
            Type = OrganisationType.RefurbPartner,
            ContactEmail = "fix@adlElect.com.au",
            ContactPhone = "0412345678",
            Address = "60 King William St, Adelaide SA 5000",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }
    };

    modelBuilder.Entity<Organisation>().HasData(organisations);
}
```

Then, in Package Manager Console, run:
```
Add-Migration "Name of the migration"
```

Then, run this to apply the changes:
```
Update-Database
```

Open the `Organisations` table and hit `refresh` to confirm if the new row is inserted.


<!-- --- -->

## Domain Model Relationships

Phase 1:

- **One organisation has many users**
- **One organisation can make many donations**
- **One donation has many devices**
- **One device belongs to one donation**
- **One device can be assigned to one refurb partner (organisation)**

<!-- --- -->

## Engineering Highlights

- Controllers: Implemented Async Await in the action methods.
- DTOs: Used DTOs to define what data can be passed between the client and the APIs in Controllers, so client --> DTO --> APIs in controller --> domain model --> database.
- Repository: Implemented to separate the data acess layr from the application, so controller --> repository --> database.