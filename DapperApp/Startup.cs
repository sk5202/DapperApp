using DapperApp.Domain.Context;
using DapperApp.Domain.Migrations;
using FluentMigrator.Runner;
using System.Reflection;

namespace DapperApp
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<DapperContext>();
			services.AddSingleton<Database>();

			services.AddLogging(c => c.AddFluentMigratorConsole())
				.AddFluentMigratorCore()
				.ConfigureRunner(c => c.AddSqlServer2012()
					.WithGlobalConnectionString(Configuration.GetConnectionString("SqlConnection"))
					.ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());


            // Add services to the container.
            services.AddControllersWithViews();
        }

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
            app.UseStaticFiles();
            app.UseRouting();

			app.UseAuthorization();
		
		
            app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
			});
		}
	}
}
