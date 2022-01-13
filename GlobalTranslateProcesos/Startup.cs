using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GlobalTranslateProcesos.Helpers;
using GlobalTranslateProcesos.Servicios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace GlobalTranslateProcesos
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        private IHostingEnvironment CurrentEnvironment { get; set; }

        public Startup(IConfiguration config, IHostingEnvironment env)
        {
            Configuration = config;
            CurrentEnvironment = env;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string llave = Configuration["Llave"];
            string endPoint = Configuration["EndPoint"];

            string llaveTraducctor = Configuration["LlaveTraduccion"];
            string hostTraductor = Configuration["HostTraduccion"];
            string pathTraductor = Configuration["PathTraduccion"];
            string pathDeteccion = Configuration["PathDeteccion"];

            services.AddMvc();

            string baseDir = CurrentEnvironment.IsDevelopment() ? AppContext.BaseDirectory : "D:\\home\\site\\wwwroot";
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info() {Title = "Global Translate SV Procesos", Version = "v1"});
                c.OperationFilter<FileUploadOperation>(); //Registramos una operacion custom
                var filePath = Path.Combine(baseDir, "GlobalTranslateProcesos.xml");
                c.IncludeXmlComments(filePath);
            });

            services.AddTransient<ExtractorService>(s => new ExtractorService(llave, endPoint));
            services.AddTransient<TraductorService>(s => new TraductorService(hostTraductor, pathTraductor, pathDeteccion, llaveTraducctor));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Global Translate SV Procesos V1");
                c.RoutePrefix = string.Empty;
                
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
