using GiveAwayInsider_API.Helper;
using GiveAwayInsider_API.Service.IService;
using GiveAwayInsider_API.Service;
using GiveAwayInsider_Business.Repository;
using GiveAwayInsider_Business.Repository.IRepository;
using GiveAwayInsider_DataAccess;
using GiveAwayInsider_DataAccess.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SendGrid.Extensions.DependencyInjection;
using System.Text;
using AspNetCore.Authentication.ApiKey;

namespace GiveAwayInsider_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.User.RequireUniqueEmail = true;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
            opt.TokenLifespan = TimeSpan.FromHours(4));

            builder.Services.AddHttpClient();
            builder.Services.AddSendGrid(opt =>
            {
                opt.ApiKey = "<your sendgrid key>";
            });
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            builder.Services.AddScoped<ISettingsRepository, SettingsRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IGiveawayService, GiveawayService>();

            var apiSettingsSection = builder.Configuration.GetSection("APISettings");
            builder.Services.Configure<APISettings>(apiSettingsSection);

            var apiSettings = apiSettingsSection.Get<APISettings>();
            var key = Encoding.ASCII.GetBytes(apiSettings.SecretKey);

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddApiKeyInHeader<ApiKeyProvider>(ApiKeyDefaults.AuthenticationScheme, options =>
            {
                options.KeyName = "X-API-Key";
                options.SuppressWWWAuthenticateHeader = true;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidAudience = apiSettings.ValidAudience,
                    ValidIssuer = apiSettings.ValidIssuer,
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GiveAwayInsider_Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter Bearer and then token in the field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

            builder.Services.AddCors(o => o.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();

            SeedDatabase();
            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
            app.MapControllers();
            app.UseDeveloperExceptionPage();
            app.UseHsts();

            app.Run();




            void SeedDatabase()
            {
                using var scope = app.Services.CreateScope();
                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                dbInitializer.Initialize();
            }
        }
    }
}