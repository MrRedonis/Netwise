using System.Net.Http.Json;
using Redonis.Netwise.Models;

namespace Redonis.Netwise.Clients
{
	internal sealed class CatFactClient : ICatFactClient
	{
		private readonly HttpClient _httpClient;

		public CatFactClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<CatFact> GetFactAsync(CancellationToken cancellationToken = default)
		{
			var response = await _httpClient.GetAsync(CatFactEndpoints.Fact, cancellationToken);

			response.EnsureSuccessStatusCode();

			return await response.Content.ReadFromJsonAsync<CatFact>(cancellationToken) 
			       ?? throw new InvalidOperationException();
		}
	}
}
