using Redonis.Netwise.Models;

namespace Redonis.Netwise.Clients
{
	public interface ICatFactClient
	{
		public Task<CatFact> GetFactAsync(CancellationToken cancellationToken = default);
	}
}
