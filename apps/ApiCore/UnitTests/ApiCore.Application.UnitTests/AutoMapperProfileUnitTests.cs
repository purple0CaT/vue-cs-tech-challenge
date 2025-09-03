using System.Diagnostics.CodeAnalysis;
using ApiCore.Clients.Contracts;
using AutoMapper;

namespace ApiCore.Application.UnitTests;

[ExcludeFromCodeCoverage]
public class AutoMapperProfileUnitTests {
	[Fact]
	public void LoadMappingsByAssemblies_ConfigurationIsValid() {
		var configuration = new MapperConfiguration(cfg => cfg.AddMaps(
			typeof(ApiCoreClientsContractsHook)));

		configuration.AssertConfigurationIsValid();
	}
}
