namespace Redonis.Netwise.Configuration
{
	public sealed class CatFactOptions
	{
		public const string SectionName = "CatFact";

		public required Uri BaseAddress { get; init; }
		public required string FilePath { get; init; }
	}
}
