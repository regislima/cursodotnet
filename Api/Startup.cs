using api.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using api.Domain.Repositories;
using api.Persistence.Repositories;
using api.Services;
using api.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            
            services.AddAutoMapper(typeof(Startup));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                opt =>
                {
                    byte[] encoding = Encoding.UTF8.GetBytes(Configuration["SecurityKey"]);
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Issuer"],
                        ValidAudience = Configuration["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(encoding)
                    };

                    opt.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            return Task.CompletedTask;
                        },

                        OnTokenValidated = context =>
                        {
                            return Task.CompletedTask;
                        }
                    };
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseCors();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
