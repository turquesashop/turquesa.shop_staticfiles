using System.Text.Encodings.Web;
using System.Text.Json;

namespace Editor.Configuration
{
	public static class JsonDefaults
	{
		public static readonly JsonSerializerOptions Options = new JsonSerializerOptions
		{
			// 1. Fixes the "Galería" vs "\u00ED" issue
			Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,

			// 2. Makes the log readable (multi-line)
			WriteIndented = true,

			// 3. (Bonus) Ignores case when reading JSON from your .env or AWS
			PropertyNameCaseInsensitive = true
		};
	}
}
