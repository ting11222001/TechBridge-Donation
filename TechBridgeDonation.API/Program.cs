using Microsoft.EntityFrameworkCore;
using TechBridgeDonation.API.Data;
using TechBridgeDonation.API.Data.Seeders;
using TechBridgeDonation.API.Mappings;
using TechBridgeDonation.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// database (PostgreSQL) connection
builder.Services.AddDbContext<TechBridgeDonationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TechBridgeDonationConnectionString")));

// Inject repositories
builder.Services.AddScoped<IOrganisationRepository, SQLOrganisationRepository>();
builder.Services.AddScoped<IDeviceRepository, SQLDeviceRepository>();

// Inject automapper
builder.Services.AddAutoMapper(configuration => configuration.AddProfile<AutoMapperProfiles>());

var app = builder.Build();

// migrate and seed database on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TechBridgeDonationDbContext>();
    await db.Database.MigrateAsync();

    await DeviceConditionSeeder.SeedAsync(db);
    await DeviceStatusSeeder.SeedAsync(db);
    await OrganisationTypeSeeder.SeedAsync(db);
    await DonationStatusSeeder.SeedAsync(db);

    await OrganisationSeeder.SeedAsync(db);
    await DonationSeeder.SeedAsync(db, OrganisationSeeder.DonorOrgId); // A donation from a business donor organisation
    await DeviceSeeder.SeedAsync(db, DonationSeeder.SubmittedDonationId, OrganisationSeeder.RefurbOrgId); // A donation with the status of DonationStatus.Submitted
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
