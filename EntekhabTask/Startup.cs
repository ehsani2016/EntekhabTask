using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace api.EntekhabTask
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


            services.AddDbContext<Data.DatabaseContext>(option =>
            {
                option.UseSqlServer(Configuration.GetSection(key: "ConnectionStrings").GetSection(key: "MyConnectionString").Value);
            });

            services.AddTransient<Data.IUnitOfWork, Data.UnitOfWork>(sp =>
            {
                Data.Tools.Options options =
                    new Data.Tools.Options
                        (Configuration.GetSection(key: "ConnectionStrings").GetSection(key: "MyConnectionString").Value)
                    {
                        Provider =
                            (Data.Tools.Enums.Provider)
                            System.Convert.ToInt32(Configuration.GetSection(key: "DatabaseProvider").Value),
                    };

                return new Data.UnitOfWork(options: options);
            });

          
            //***** For Encode Unicode and utf8
            services.AddSingleton<System.Text.Encodings.Web.HtmlEncoder>(
                System.Text.Encodings.Web.HtmlEncoder.Create(allowedRanges:
                    new[] { System.Text.Unicode.UnicodeRanges.BasicLatin, System.Text.Unicode.UnicodeRanges.Arabic }));
            //***** For Encode Unicode and utf8




            services.AddControllers();

            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation
                swagger.SwaggerDoc("v1.00", new OpenApiInfo
                {
                    Version = "v1.00",
                    Title = "API Document - ENTEKHAB.CO",
                    Description = "THIS APP ENTEKHAB TASK",
                    Contact = new OpenApiContact()
                    {
                        Url = new Uri("https://entekhabelectronic.ir/"),
                        Email = "info@entekhabelectronic.ir",
                        Name = "https://entekhabelectronic.ir/",
                    }
                });
            });


            services.AddTransient<DataDapper.IUnitOfWork, DataDapper.UnitOfWork>(sp=>
            {
                DataDapper.Tools.Options options =
                   new DataDapper.Tools.Options(Configuration.GetSection(key: "ConnectionStrings").GetSection(key: "MyConnectionString").Value);

                return new DataDapper.UnitOfWork(options: options);
            });

            services.AddTransient<OvertimePolicies.IOvertimeCalculator, OvertimePolicies.OvertimeCalculator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
              
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1.00/swagger.json", "api.EntekhabTask v1.00"));

            app.UseHttpsRedirection();

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
