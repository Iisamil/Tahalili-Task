using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using New.Services;
using News.Core.Repos;
using News.Core.Services;
using News.Repository;
using News.Repository.Data;
using NewsApi.Extenstions;

namespace NewsApi
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			#region Configure Services

			
			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<NewsWebsiteDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
				// Type ConnectionString in AppSettingFile in Project APIs
			});

			// Allow Dependancy Injection For IGenericRepository 
			builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));


			builder.Services.AddIdentityServices(builder.Configuration);

			builder.Services.AddScoped(typeof(ITokenService), typeof(TokenService));

			builder.Services.AddAutoMapper(typeof(MappingProfiles)); 


			#endregion


			var app = builder.Build();

			#region Update Database
			
			using var scope = app.Services.CreateScope();

			var services = scope.ServiceProvider;

			var loggerFactory = services.GetRequiredService<ILoggerFactory>(); // make error shown in Kestrel Screen

			try
			{
				var dbContext = services.GetRequiredService<NewsWebsiteDbContext>(); // Ask Explicity CLR to Make Object from StoreContext

				await dbContext.Database.MigrateAsync(); // Apply Migration

			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, " An Error Occured During Applying Migrations !");
			}

			#endregion

			// Configure the HTTP request pipeline.

			#region Configure Middilewares

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			#endregion

			app.Run();
		}
	}
}