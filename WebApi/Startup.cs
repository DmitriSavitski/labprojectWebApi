using BLL.Interfaces;
using BLL.Manages;
using DAL.Context;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		public IConfiguration Configuration { get; }

		public Program Program
		{
			get => default;
			set
			{
			}
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddScoped<IRepositoryManager<Departments>, RepositoryManager<Departments>>();
			services.AddScoped<IRepositoryManager<Employees>, RepositoryManager<Employees>>();
			services.AddScoped<IRepositoryManager<FIO>, RepositoryManager<FIO>>();
			services.AddScoped<IRepositoryManager<Leaders>, RepositoryManager<Leaders>>();
			services.AddScoped<IRepositoryManager<Nationality>, RepositoryManager<Nationality>>();
			services.AddScoped<IRepositoryManager<Position>, RepositoryManager<Position>>();
			services.AddScoped<IRepositoryManager<Protocol>, RepositoryManager<Protocol>>();
			services.AddScoped<IRepositoryManager<Role>, RepositoryManager<Role>>();
			services.AddScoped<IRepositoryManager<Status>, RepositoryManager<Status>>();
			services.AddScoped<IRepositoryManager<Users>, RepositoryManager<Users>>();

			services.AddScoped<IDepartmentsManager, DepartmentsManager>();
			services.AddScoped<IEmployeesManages, EmployeesManager>();
			services.AddScoped<IFIOManager, FIOManager>();
			services.AddScoped<ILeadersManager, LeadersManager>();
			services.AddScoped<INationalityManager, NationalityManager>();
			services.AddScoped<IPositionManager, PositionManager>();
			services.AddScoped<IProtocolManager, ProtocolManager>();
			services.AddScoped<IRoleManager, RoleManager>();
			services.AddScoped<IStatusManager, StatusManager>();
			services.AddScoped<IUsersManager, UsersManager>();

			services.AddSwaggerGen();

			services.AddDbContext<ApplicationContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
					options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
				});

			services.AddControllers();
		}
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseDeveloperExceptionPage();
			if (env.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();
			app.UseAuthentication();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
				   name: "default",
				   pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
