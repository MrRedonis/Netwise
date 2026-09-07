using Redonis.Netwise.Clients;

namespace Redonis.Netwise
{
	public sealed class CatFactService
	{
		private readonly ICatFactClient _client;
		private readonly FileWriter _fileWriter;

		public CatFactService(
			ICatFactClient client, 
			FileWriter fileWriter)
		{
			_client = client;
			_fileWriter = fileWriter;
		}

		public async Task SaveRandomFactAsync(CancellationToken cancellationToken = default)
		{
			var catFact = await _client.GetFactAsync(cancellationToken);
			await _fileWriter.WriteAsync(catFact.Fact, cancellationToken);
		}
	}
}
