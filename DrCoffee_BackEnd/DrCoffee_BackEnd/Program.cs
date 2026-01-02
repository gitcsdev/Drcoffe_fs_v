using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using DrCoffee_BackEnd.Data;
using DrCoffee_BackEnd.Models;
using DrCoffee_BackEnd.Repositories;
using DrCoffee_BackEnd.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers + Swagger
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Ensure JSON serialization uses camelCase (default in ASP.NET Core)
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Dr.Coffee Admin API",
        Version = "v1",
        Description = "API for managing Dr.Coffee shop operations"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter: Bearer {your JWT token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ? CORS (Policy ????? ???)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ? EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ? Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// ? JWT
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured");
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer not configured");
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience not configured");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomizationOptionRepository, CustomizationOptionRepository>();

var app = builder.Build();

// ? Seed Data (??? build)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    
    // Seed Identity (Roles & Users)
    await SeedDataAsync(services);
    
    // Seed Menu Data (Categories, Products, Prices, Tags, Flavors)
    await DataSeeder.SeedMenuDataAsync(context);
}

// Enable Swagger in all environments (or just Development if preferred)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dr.Coffee Admin API v1");
    c.RoutePrefix = "swagger"; // Swagger UI will be available at /swagger
    c.DisplayRequestDuration();
    c.EnableDeepLinking();
    c.EnableFilter();
});

app.UseHttpsRedirection();

app.UseCors("AllowAll"); // ??? Auth

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// ---------------- Seed Method ----------------
static async Task SeedDataAsync(IServiceProvider services)
{
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var configuration = services.GetRequiredService<IConfiguration>();
    var logger = services.GetRequiredService<ILogger<Program>>();

    string[] roles = { "Admin", "Manager" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
            logger.LogInformation($"✅ Created role: {role}");
        }
        else
        {
            logger.LogInformation($"ℹ️ Role already exists: {role}");
        }
    }

    var adminEmail = configuration["SeedData:AdminEmail"] ?? "admin@drcoffee.com";
    var adminPassword = configuration["SeedData:AdminPassword"] ?? "Admin@123";

    var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
    if (existingAdmin == null)
    {
        var adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            FirstName = "Admin",
            LastName = "User",
            CreatedAt = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
            logger.LogInformation($"✅ Created admin user: {adminEmail}");
        }
        else
        {
            logger.LogError($"❌ Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }
    else
    {
        // Reset password to ensure it matches configuration
        var token = await userManager.GeneratePasswordResetTokenAsync(existingAdmin);
        var resetResult = await userManager.ResetPasswordAsync(existingAdmin, token, adminPassword);
        if (resetResult.Succeeded)
        {
            logger.LogInformation($"✅ Reset password for existing admin user: {adminEmail}");
        }
        else
        {
            logger.LogWarning($"⚠️ Could not reset admin password: {string.Join(", ", resetResult.Errors.Select(e => e.Description))}");
        }
        
        // Ensure user has Admin role
        if (!await userManager.IsInRoleAsync(existingAdmin, "Admin"))
        {
            await userManager.AddToRoleAsync(existingAdmin, "Admin");
            logger.LogInformation($"✅ Added Admin role to existing user: {adminEmail}");
        }
        else
        {
            logger.LogInformation($"ℹ️ Admin user already exists with Admin role: {adminEmail}");
        }
    }

    var managerEmail = configuration["SeedData:ManagerEmail"] ?? "manager@drcoffee.com";
    var managerPassword = configuration["SeedData:ManagerPassword"] ?? "Manager@123";

    var existingManager = await userManager.FindByEmailAsync(managerEmail);
    if (existingManager == null)
    {
        var managerUser = new ApplicationUser
        {
            UserName = managerEmail,
            Email = managerEmail,
            EmailConfirmed = true,
            FirstName = "Manager",
            LastName = "User",
            CreatedAt = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(managerUser, managerPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(managerUser, "Manager");
            logger.LogInformation($"✅ Created manager user: {managerEmail}");
        }
        else
        {
            logger.LogError($"❌ Failed to create manager user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }
    else
    {
        logger.LogInformation($"ℹ️ Manager user already exists: {managerEmail}");
    }
}
