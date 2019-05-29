using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.DAL.Data;
using DatingWebsiteApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace App.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            }
            );

            //var mappingConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new AutoMapperProfile());
            //});
            //IMapper mapper = mappingConfig.CreateMapper();
            //services.AddSingleton(mapper);

            services.AddMvc(options =>
            {
                //options.Filters.Add(typeof(LogActionFilter));
                //options.Filters.Add(typeof(LogErrorFilter));
            }).AddJsonOptions(options =>
            {
                var resolver = options.SerializerSettings.ContractResolver;
                if (resolver != null)
                    ((DefaultContractResolver)resolver).NamingStrategy = null;
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddCors();

            services.AddSession(opts =>
            {
                opts.Cookie.IsEssential = true;
            });

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            //services.AddScoped<IEmailService, EmailService>();
            //services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<IAccountService, AccountService>();
            //services.AddScoped<IAdminService, AdminService>();
            //services.AddScoped<ISessionHelper, SessionHelper>();
            //services.AddScoped<IOrderService, OrderService>();
            //services.AddScoped<ICartService, CartService>();
            //services.AddScoped<IOrderProductService, OrderProductService>();
            //services.AddScoped<IFileService, FileService>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "WebAppTest", Description = "Swagger API" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
