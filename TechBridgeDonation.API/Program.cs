using Microsoft.EntityFrameworkCore;
using TechBridgeDonation.API.Data;
using TechBridgeDonation.API.Data.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// database (PostgreSQL) connection
builder.Services.AddDbContext<TechBridgeDonationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TechBridgeDonationConnectionString")));

var app = builder.Build();

// migrate and seed database on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TechBridgeDonationDbContext>();
    await db.Database.MigrateAsync();
    await OrganisationSeeder.SeedAsync(db);

    await DonationSeeder.SeedAsync(db, OrganisationSeeder.DonorOrgId); // A donation from a business donor organisation

    var donationId = Guid.Parse("b1000000-0000-0000-0000-000000000001"); // A donation with the status of DonationStatus.Submitted
    await DeviceSeeder.SeedAsync(db, donationId, OrganisationSeeder.RefurbOrgId);
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
