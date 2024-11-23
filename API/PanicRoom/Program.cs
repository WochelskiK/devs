using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PanicRoom.DAL;
using PanicRoom.Entities;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddAuthentication(o =>
    {
        o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(o =>
    {
        //o.Audience = authOptions.Audience;
        //o.SaveToken = authOptions.SaveToken;
        //o.Challenge = JwtBearerDefaults.AuthenticationScheme;
        o.RequireHttpsMetadata = false;
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = "123", 
            ValidAudience = "123", 
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("8323bcef-df83-4d2f-93d7-9f01c131b02b")),
            ValidateLifetime = true
        }; 
    });
builder.Services.AddAuthorization();
builder.Services
    .AddDbContext<PanicRoomDbContext>(opt => opt.UseNpgsql("Host=localhost;User ID=postgres;Password=mysecretpassword;Database=PanicRoomDatabase;Port=5432"));
builder.Services.AddIdentityCore<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = true;
    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(20);

})
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<PanicRoomDbContext>()
    .AddDefaultTokenProviders();
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
