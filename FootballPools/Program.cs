using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FootballPools.Data.Context;
using FootballPools.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.OpenApi.Models;
using FootballPools.Data.Identity;
using FootballPools.Data;

var builder = WebApplication.CreateBuilder(args);

// Instantiate database context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(connectionString));

var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})
//    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
//    {
//        options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;

//        //add this to configure Secure

//        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
//        options.Cookie.SameSite = SameSiteMode.None; //TODO is this important?
//        options.Cookie.HttpOnly = true;
//        options.ExpireTimeSpan = TimeSpan.FromDays(1);
//        options.SlidingExpiration = true;

//        options.LoginPath = "/identity/login";
//        options.LogoutPath = "/identity/logout";
//        options.AccessDeniedPath = new PathString("/identity/denied");
//    });

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            RequireExpirationTime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = config["JWT:ValidIssuer"],
            ValidAudience = config["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]))
        };
    });

builder.Services.AddIdentity<User, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<IdentityDbContext>();

builder.Services.AddTransient<IEmailSender, MailKitEmailSender>();
builder.Services.Configure<MailKitEmailSenderOptions>(options =>
{
    options.Host_Address = "smtp.gmail.com";
    options.Host_Port = 587;
    options.Host_SecureSocketOptions = MailKit.Security.SecureSocketOptions.StartTls;
    options.Host_Username = "jegnzc@gmail.com";
    options.Host_Password = "wynsttrbcdpoaixh";
    options.Sender_Email = "jegnzc@gmail.com";
    options.Sender_Name = "jegnzc";
});

//builder.Services.AddHttpContextAccessor();
//builder.Services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
             new string[] {}
     }
 });
}
);
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

var app = builder.Build();
app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error-development");
}
else
{
    app.UseExceptionHandler("/error");
}
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();