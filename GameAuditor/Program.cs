using GameAuditor.Database;
using GameAuditor.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using GameAuditor.Repositories.Implimentations;
using GameAuditor.Repositories.Interfaces;
using GameAuditor.Services.UserService;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using GameAuditor.AutomapperProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(GameAutomapper));
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEntityRepository<Game>, EntityRepository<Game>>();
builder.Services.AddScoped<IEntityRepository<Post>, EntityRepository<Post>>();
builder.Services.AddScoped<IEntityRepository<PostTag>, EntityRepository<PostTag>>();
builder.Services.AddScoped<IEntityRepository<TagNavigation>, EntityRepository<TagNavigation>>();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"Bearer token\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true, // валидация ключа безопасности
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8 // установка ключа безопасности
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false, // указывает, будет ли валидироваться издатель при валидации токена
            //ValidIssuer = AuthOptions.ISSUER, // строка, представляющая издателя
            ValidateAudience = false // будет ли валидироваться потребитель токена
            //ValidAudience = AuthOptions.AUDIENCE,  // установка потребителя токена
        };
        options.Events = new JwtBearerEvents()
        {
            OnTokenValidated = async ctx =>
            {
                var usermgr = ctx.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
                var signmgr = ctx.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();
                var username = ctx.Principal?.FindFirst(ClaimTypes.Name)?.Value;
                var user = await usermgr.FindByNameAsync(username);
                ctx.Principal = await signmgr.CreateUserPrincipalAsync(user);
            }
        };
    });

// For Identity
builder.Services.AddIdentity<User, IdentityRole>(opts =>
{
    //opts.Password.RequireDigit = true;
    //opts.Password.RequiredLength = 8;
    //opts.Password.RequireUppercase = true;
    //opts.Password.RequireLowercase = true;
    opts.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders();

builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NgOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//SeedData.EnsurePopulated(app);

app.Run();
