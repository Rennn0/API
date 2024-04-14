using System.IO;

namespace Application.Templates
{
	public static class Reader
	{
		public static string Read(string fileName)
		{
			return File.ReadAllText($"{Directory.GetCurrentDirectory()}/Templates/{fileName}");
		}
	}
}