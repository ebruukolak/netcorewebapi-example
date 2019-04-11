using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CoreWebApi.Helpers;
using DAL.Abstract;
using DAL.Concrete;
using Manager.Abstract;
using Manager.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace CoreWebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddEntityFrameworkNpgsql().AddDbContext<EFContext>().BuildServiceProvider();

            services.AddAutoMapper();
            
            //DI configuring
            services.AddScoped<IProductManager,ProductManager>();
            services.AddScoped<IProductDAL,ProductDAL>();
            services.AddScoped<ICategoryManager,CategoryManager>();
            services.AddScoped<ICategoryDAL,CategoryDAL>();
            services.AddScoped<ISupplierManager,SupplierManager>();
            services.AddScoped<ISupplierDAL,SupplierDAL>();

            services.AddScoped<IUserManager,UserManager>();
            services.AddScoped<IUserDAL,UserDAL>();   
            services.AddScoped<TokenFilter>();
            
           services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("CoreSwagger", new Info
                {
                    Title = "Swagger on ASP.NET Core",
                    Version = "4.0.1",
                    Description = "Try Swagger on (ASP.NET Core 2.1)"
                });
                 c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
            });
            
            
            var appSettingsSection=Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings =appSettingsSection.Get<AppSettings>();
            var key=Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x=>{
              x.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
              x.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;              
            })
            .AddJwtBearer(x=>{
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated=context=>
                    {
                        var userService=context.HttpContext.RequestServices.GetRequiredService<IUserManager>();
                        var userID =int.Parse(context.Principal.Identity.Name);
                        var user =userService.GetByID(userID);
                        if(user==null)
                            context.Fail("Unauthorized");  
                        
                      return Task.CompletedTask;
                    }
                  
                };
                x.RequireHttpsMetadata=false;
                x.SaveToken=true;
                x.TokenValidationParameters=new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                   ValidateIssuerSigningKey=true,
                   IssuerSigningKey=new SymmetricSecurityKey(key),
                   ValidateIssuer=false,
                   ValidateAudience=false                   
                };
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }       
          app.UseSwagger()
             .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/CoreSwagger/swagger.json", "Swagger Test .Net Core");
                });
        
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
            
        }
    }
}
