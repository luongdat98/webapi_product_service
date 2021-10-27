using APIWeb.Businesses;
using APIWeb.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebAPI2.Businesses;
using WebAPI2.Helpers;
using WebAPI2.Models;
using WebAPI2.Repositories;

namespace WebAPI2
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

            services.AddControllers();

            // Secret key
            var jwtSection = Configuration.GetSection("JWTSettings");
            services.Configure<JWTSettings>(jwtSection);

            //To validate the token which has been sent by clients
            var appSettings = jwtSection.Get<JWTSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            services.AddTransient<ProductBusiness>();
            services.AddTransient<UserBusiness>();
            services.AddTransient<CategoryBusiness>();
            services.AddTransient<SupplierBusiness>();

            services.AddScoped<IUserRepository, UserBusiness>();
            services.AddScoped<IProductRepository, ProductBusiness>();
            services.AddScoped<ICategoryRepository, CategoryBusiness>();
            services.AddScoped<ISupplierRepository, SupplierBusiness>();

            // Add connect db
            services.AddDbContext<EcommerceContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("EcommerceDb"));
            });

            //Enable Cors
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod()
                 .AllowAnyHeader());
            });

            //// Json Serializer
            //services.AddControllersWithViews()
            //    .AddNewtonsoftJson(options =>
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
            //    .Json.ReferenceLoopHandling.Ignore)
            //    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
            //    = new DefaultContractResolver());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI2", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI2 v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
