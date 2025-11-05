using System.Text;
using flappyBirb_serveur.Data;
using flappyBirb_serveur.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<flappyBirb_serveurContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("flappyBirb_serveurContext") ?? throw new InvalidOperationException("Connection string 'serveur16Context' not found."));
    options.UseLazyLoadingProxies(); // Ceci
});
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<flappyBirb_serveurContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
});

builder.Services.AddAuthentication(options =>
{
    // Indiquer à ASP.NET Core que nous procéderons à l'authentification par le biais d'un JWT
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{

    options.SaveToken = true; // Sauvegarder les tokens côté serveur pour pouvoir valider leur authenticité
    options.RequireHttpsMetadata = false; // Lors du développement on peut laisser à false
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidAudience = "http://localhost:4200", // Audience : Client
        ValidIssuer = "https://localhost:44378", // ⛔ Issuer : Serveur -> HTTPS VÉRIFIEZ le PORT de votre serveur dans launchsettings.json !
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes("LooOOongue Phrase SiNoN Ça ne Marchera PaAaAAAaAas !")) // Clé pour déchiffrer les tokens
    };
});




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");



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
app.UseAuthentication();
