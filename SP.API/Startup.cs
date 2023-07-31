using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SP.Base.JwtConfig;
using SP.Business;
using SP.Data;
using SP.Schema;
using System.Text;

namespace SP.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static JwtConfig JwtConfig { get; private set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = " SP.API", Version = "v1" });


            //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            //    {
            //        Name = "Bearer",
            //        Type = SecuritySchemeType.Http,
            //        Scheme = "bearer",
            //        BearerFormat = "JWT",
            //        In = ParameterLocation.Header,
            //        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            //    });
            //    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            //    {
            //        {
            //            new OpenApiSecurityScheme
            //            {
            //                Reference = new OpenApiReference
            //                {
            //                    Type = ReferenceType.SecurityScheme,
            //                    Id = "Bearer"
            //                }
            //            },
            //            Array.Empty<string>()

            //        }
            //    });
            //});

     
            //services.AddAuthentication(option =>
            //{
            //    option.DefaultAuthenticateScheme = tokenOptions.ProviderKey;
            //    option.DefaultChallengeScheme = tokenOptions.ProviderKey;

            //})
            //.AddJwtBearer(tokenOptions.ProviderKey, options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateAudience = true,
            //        ValidateIssuer = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = tokenOptions.Issuer,
            //        ValidAudience = tokenOptions.Audience,
            //        IssuerSigningKey = signInKey,
            //        ClockSkew = TimeSpan.Zero,
            //        RequireExpirationTime = true
            //    };
            });
  


            //DB Bağlantısı 

            var dbType = Configuration.GetConnectionString("DbType");
            if (dbType == "Sql")  //DEFAULT OLARAK MSSQL ÇALIŞIYOR
            {
                var dbConfig = Configuration.GetConnectionString("MsSqlConnection");
                services.AddDbContext<SPDbContext>(opts =>
                opts.UseSqlServer(dbConfig));
            }
            else if (dbType == "PostgreSql")
            {
                var dbConfig = Configuration.GetConnectionString("PostgreSqlConnection");
                services.AddDbContext<SPDbContext>(opts =>
                  opts.UseNpgsql(dbConfig));

            }
   

            //INJECT
            services.AddDependencyInjection();

            //MAPPING

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperConfig());
            });
            services.AddSingleton(config.CreateMapper());

            //JWT AUTH
            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = true;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidIssuer = JwtConfig.Issuer,
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtConfig.Secret)),
            //        ValidAudience = JwtConfig.Audience,
            //        ValidateAudience = false,
            //        ValidateLifetime = true,
            //        ClockSkew = TimeSpan.FromMinutes(2)
            //    };
            //});

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", " SP.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
