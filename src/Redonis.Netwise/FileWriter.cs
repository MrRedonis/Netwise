namespace Redonis.Netwise
{
	public sealed class FileWriter
	{
		private readonly string _filePath;

		public FileWriter(string filePath)
		{
			_filePath = filePath;
		}

		public Task WriteAsync(string content, CancellationToken cancellationToken = default)
		{
			return File.AppendAllTextAsync(
				_filePath,
				content + Environment.NewLine,
				cancellationToken);
		}
	}
}
