using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BackendGestionProyectosLiquidaciones.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BackendGestionProyectosLiquidaciones.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BackendGestionProyectosLiquidaciones
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddCors();
            services.AddDbContext<TpSeminarioContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TpSeminarioContext")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            // JWT Authentication
            var appSettings = Configuration.GetSection("AppSettings").Get<Settings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(auth =>
            {
                auth.Events = new JwtBearerEvents
                {
                    OnTokenValidated = ctx =>
                    {
                        var usuarioService = ctx.HttpContext.RequestServices.GetRequiredService<IUsuarioService>();
                        var userId = int.Parse(ctx.Principal.Identity.Name);
                        var user = usuarioService.FindUsuarioById(userId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            ctx.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };

                auth.RequireHttpsMetadata = false;
                auth.SaveToken = true;
                auth.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // services.AddAuthorization();

            // Inyeccion de dependencias
            services.AddSingleton<IUsuarioService, UsuarioService>();
            services.AddSingleton<IClienteService, ClienteService>();
            services.AddSingleton<IEmpleadoService, EmpleadoService>();
            services.AddSingleton<ILocalidadService, LocalidadService>();
            services.AddSingleton<IProvinciaService, ProvinciaService>();
            services.AddSingleton<IProyectoService, ProyectoService>();
            services.AddSingleton<IPerfilService, PerfilService>();

        }

        // -------------------------------------------------------------------------------------------------
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            // Con esto genero las url de los endpoints
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
