using Domain;
using Infrastructure.Photos;
using Infrastructure.Security;
using Application.Interfaces;
using Application.Profiles;
using Persistence;
using Application.Posts;
using Azure.Storage.Blobs;

var builder = WebApplication.CreateBuilder(args);

// add services

builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseLazyLoadingProxies();
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("http://localhost:4200").AllowCredentials();
    });
});
builder.Services.AddMediatR(typeof(List.Handler).Assembly);
builder.Services.AddAutoMapper(typeof(List.Handler));
builder.Services.AddSwaggerDocumentation();
builder.Services.AddSignalR();
builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));

});
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Create>();
builder.Services.AddMvc();

var identity = builder.Services.AddIdentityCore<AppUser>(opt =>
{
    opt.SignIn.RequireConfirmedEmail = true;
})
.AddDefaultTokenProviders();

var identityBuilder = new IdentityBuilder(identity.UserType, typeof(Role), builder.Services);
identityBuilder.AddEntityFrameworkStores<DataContext>();
identityBuilder.AddRoleValidator<RoleValidator<Role>>();
identityBuilder.AddRoleManager<RoleManager<Role>>();
identityBuilder.AddSignInManager<SignInManager<AppUser>>();

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("IsPostHost", policy =>
    {
        policy.Requirements.Add(new IsHostRequirement());
    });

});
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("IsCommentHost", policy =>
    {
        policy.Requirements.Add(new IsHostRequirementForComment());
    });
});
builder.Services.AddTransient<IAuthorizationHandler, IsHostRequirementHandler>();
builder.Services.AddTransient<IAuthorizationHandler, IsHostRequirementHandlerComment>();

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]!));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
    };

    opt.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chat"))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
builder.Services.AddScoped<IUserAccessor, UserAccessor>();
builder.Services.AddScoped<IImageService, PhotoAccessorS3>();
builder.Services.AddScoped<IProfileReader, ProfileReader>();
builder.Services.AddScoped<IFacebookAccessor, FacebookAccessor>();
builder.Services.AddScoped<IEmailService, EmailServiceAws>();
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.Configure<AwsSettings>(builder.Configuration.GetSection("Aws"));
builder.Services.Configure<FacebookAppSettings>(builder.Configuration.GetSection("Authentication:Facebook"));
builder.Host.UseSerilog(Logging.ConfigureLogger);
builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration["AZURE_STORAGE_CONNECTION_STRING"]));
builder.Services.AddSingleton<IBlobService, BlobService>();
// configure http request pipeline 

//AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<ErrorHandlingMiddleware>();
}
else
{
    app.Use(async(context, next) => {
        context.Response.Headers.Append("Strict-Transport-Security", "max-age=31536000");
        await next.Invoke();
    });
}

// app.UseHttpsRedirection();



app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerDocumentation();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();
app.MapHub<ChatHub>("/chat");
app.MapFallbackToController("Index", "Fallback");


Activity.DefaultIdFormat = ActivityIdFormat.W3C;
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<Role>>();
    await Seed.SeedData(context,userManager,roleManager);
}
catch(Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

await app.RunAsync();
