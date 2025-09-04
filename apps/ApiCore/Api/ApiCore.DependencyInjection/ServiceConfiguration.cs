using System.Threading.RateLimiting;

using ApiCore.Clients;

using Flurl.Http.Configuration;

using Lamar.Microsoft.DependencyInjection;

using Microsoft.AspNetCore.RateLimiting;

namespace ApiCore.DependencyInjection;

public static class ServiceConfiguration {
	public static void ConfigureBuilder(this WebApplicationBuilder builder) {
		var config = builder.Configuration;
		builder.ConfigureServices(config);
		builder.Host.UseLamar(config => {
			config.IncludeRegistry(new ServiceRegistry());
			config.AddControllers();
		});
	}
	public static void ConfigureApplication(this WebApplication app) {

		if (app.Environment.IsDevelopment()) {
			app.UseSwagger();
			app.UseSwaggerUI();

		} else {
			app.UseHttpsRedirection();
		}

		app.UseRouting();
		app.UseCors("AllowAll");

		app.UseAuthorization();

		//TODO we could also add a global exception handler app.UseGlobalExceptionHandler();

		app.MapControllers();
		app.MapHealthChecks("/api/health");
	}

	public static void ConfigureServices(this WebApplicationBuilder builder, IConfiguration config) {
		var services = builder.Services;
		var configuration = builder.Configuration;

		// Add environment variables to configuration
		var configBuilder = new ConfigurationBuilder()
			.AddConfiguration(configuration)
			.AddEnvironmentVariables();

		// Add auto IOC from Lamar.
		services.AddLamar(new ServiceRegistry());

		services.AddAutoMapper(
				typeof(ApiCoreClientsHook));

		services.AddWeb();

		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
	}

	private static void AddWeb(this IServiceCollection services) {
		services.AddMemoryCache();
		services.AddHealthChecks();
		services.AddRateLimiter(limiterOptions => {
			// available sliding window limitter
			limiterOptions.AddFixedWindowLimiter("fixed", options => {
				options.PermitLimit = 10;
				options.Window = TimeSpan.FromSeconds(10);
				options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
				options.QueueLimit = 5;
			});
		});
		services.AddCors(options => {
			options.AddPolicy("AllowAll", builder => {
				builder
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
					.WithExposedHeaders("Content-Range", "Accept-Ranges");
			});
		});

		services.AddHttpContextAccessor();

		// Configure Flurl client cache with Star Wars API
		services.AddSingleton<IFlurlClientCache>(provider => {
			var cache = new FlurlClientCache();
			cache.Add("https://swapi.info/api/", "https://swapi.info/api/");
			return cache;
		});
	}

}