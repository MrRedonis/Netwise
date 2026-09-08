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
			EnsureDirectoryExist();

			return File.AppendAllTextAsync(
				_filePath,
				content + Environment.NewLine,
				cancellationToken);
		}

		private void EnsureDirectoryExist()
		{
			var directory = Path.GetDirectoryName(_filePath);

			if (!string.IsNullOrWhiteSpace(directory))
			{
				Directory.CreateDirectory(directory);
			}
		}
	}
}
