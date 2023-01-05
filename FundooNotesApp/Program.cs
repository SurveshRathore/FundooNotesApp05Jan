using ManagerLayer.Interface;
using ManagerLayer.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using RepoLayer.Context;
using RepoLayer.Interface;
using RepoLayer.Service;
using System.Text;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddDbContext<FundooDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FundooDB")));


    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Welcome to the Fundoo Notes Application" });
        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            Scheme = "bearer",
            BearerFormat = "JWT",
            Name = "JWT Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Description = "enter JWT Bearer token on textbox below!",

            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };
        c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                 { jwtSecurityScheme, Array.Empty<string>() }
                });
    });
    var tokenKey = builder.Configuration.GetValue<string>("Jwt:Key");
    var key = Encoding.ASCII.GetBytes(tokenKey);
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


    builder.Services.AddTransient<IUserBL, UserBL>();
    builder.Services.AddTransient<IUserRL, UserRL>();
    builder.Services.AddTransient<INoteBL, NoteBL>();
    builder.Services.AddTransient<INotesRL, NotesRL>();
    builder.Services.AddTransient<ILabelBL, LabelBL>();
    builder.Services.AddTransient<ILabelRL, LabelRL>();
    builder.Services.AddTransient<ICollabBL, CollabBL>();
    builder.Services.AddTransient<ICollabRL, CollabRL>();

    //logger code for nlog
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    ///----------- bulider app --------
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //logger code for nlog
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("Home/Error");
        //The default HSTS value is 30 days. you may want to change this for the production scenarios.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    //logger code for nlog
    app.UseStaticFiles();
    

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.MapControllerRoute(
        name: "Admin",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();


}
catch (Exception ex)
{
    logger.Error(ex, "Program stopped due to exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}



