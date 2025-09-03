using System.Diagnostics.CodeAnalysis;

using ApiCore.Application;
using ApiCore.Clients;
using ApiCore.Clients.Contracts;
using ApiCore.Contracts;

namespace ApiCore.DependencyInjection;

[ExcludeFromCodeCoverage]
internal class ServiceRegistry : Lamar.ServiceRegistry {
	public ServiceRegistry() {
		Scan(scanner => {
			scanner.AssemblyContainingType<ApiCoreApplicationHook>();
			scanner.AssemblyContainingType<ApiCoreApplicationContractsHook>();
			scanner.AssemblyContainingType<ApiCoreClientsHook>();
			scanner.AssemblyContainingType<ApiCoreClientsContractsHook>();
			scanner.WithDefaultConventions();
		});
	}
}
