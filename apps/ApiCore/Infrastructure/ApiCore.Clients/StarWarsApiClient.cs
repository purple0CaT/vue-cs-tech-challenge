using ApiCore.Clients.Contracts;
using ApiCore.Clients.Models;
using ApiCore.Common.Extensions;
using ApiCore.Models;

using AutoMapper;

using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;

using Microsoft.AspNetCore.Http;

namespace ApiCore.Clients;

public class StarWarsApiClient
(IFlurlClientCache flurlClientCache, IMapper mapper) : IStarWarsApiClient {
	private readonly IFlurlClientCache _flurlClientCache = flurlClientCache.NotNull(nameof(flurlClientCache));
	private readonly IMapper _mapper = mapper.NotNull(nameof(mapper));
	private readonly string _peopleSegment = "people";
	private readonly string baseUrl = "https://swapi.info/api";

	public async Task<Character> GetOne(int id) {
		var url = $"{baseUrl}/{_peopleSegment}/{id}";
		var client = _flurlClientCache.GetOrAdd(url, url);
		var flurlResponse = await client.Request(url).GetAsync();
		if (flurlResponse.StatusCode != StatusCodes.Status200OK) {
			throw new Exception("Character not found");
		}

		var response = await flurlResponse
			.GetJsonAsync<StarWarsApiPeopleResponse>();
		return _mapper.Map<Character>(response);
	}

	public async Task<IList<Character>> GetAll() {
		var url = $"{baseUrl}/{_peopleSegment}";
		var client = _flurlClientCache.GetOrAdd(url, url);
		var flurlResponse = await client.Request().GetAsync();

		if (flurlResponse.StatusCode != StatusCodes.Status200OK) {
			throw new Exception("Characters not found");
		}
		var response = await flurlResponse.GetJsonAsync<List<StarWarsApiPeopleResponse>>();
		return _mapper.Map<IList<Character>>(response);
	}

}
