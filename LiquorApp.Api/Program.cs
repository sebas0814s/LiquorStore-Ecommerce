using LiquorApp.Api.Data;
using LiquorApp.Api.Models;
using LiquorApp.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Choose database provider based on configuration
var provider = builder.Configuration["DatabaseProvider"]?.ToLowerInvariant() ?? "sqlserver";
var connStr = builder.Configuration.GetConnectionString("DefaultConnection")
             ?? (provider == "sqlite" ? "Data Source=LiquorApp.db" : "Server=(localdb)\\MSSQLLocalDB;Database=LiquorApp;Trusted_Connection=True;TrustServerCertificate=True;");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (provider == "sqlite") options.UseSqlite(connStr);
    else options.UseSqlServer(connStr);
});

// Password hashing service
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// Static files for simple frontend
builder.Services.AddDirectoryBrowser();

var app = builder.Build();

// Apply migrations / ensure created on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try { db.Database.Migrate(); }
    catch { db.Database.EnsureCreated(); }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

// Minimal API for user registration
app.MapPost("/api/users/register", async (
    [FromBody] RegisterUserRequest request,
    AppDbContext db,
    IPasswordHasher hasher,
    CancellationToken ct) =>
{
    // Basic model validation
    var errors = Validation.ValidateRegisterRequest(request);
    if (errors.Count > 0)
    {
        return Results.ValidationProblem(errors);
    }

    var emailNorm = request.Email.Trim().ToLowerInvariant();

    var emailExists = await db.Users.AnyAsync(u => u.Email == emailNorm, ct);
    if (emailExists)
    {
        return Results.Conflict(new { message = "Email already in use" });
    }

    // Hash password
    var (hash, salt) = hasher.HashPassword(request.Password);

    var user = new User
    {
        FirstName = request.FirstName.Trim(),
        LastName = request.LastName.Trim(),
        Email = emailNorm,
        PasswordHash = hash,
        PasswordSalt = salt,
        CreatedAtUtc = DateTime.UtcNow
    };

    db.Users.Add(user);
    await db.SaveChangesAsync(ct);

    var response = new RegisterUserResponse
    {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email,
        CreatedAtUtc = user.CreatedAtUtc
    };

    return Results.Created($"/api/users/{user.Id}", response);
})
.WithName("RegisterUser")
.WithOpenApi();

app.Run();

// Expose Program for WebApplicationFactory in tests
public partial class Program { }
