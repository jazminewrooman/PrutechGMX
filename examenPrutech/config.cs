using System;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace GMX
{
    public static class config
    {
        public static Dictionary<string, string> cfg = null;

        public static Dictionary<string, string> Config
        {
            get
            {
                if (cfg == null)
                {
                    var assembly = typeof(config).GetTypeInfo().Assembly;
					//var stream = assembly.GetManifestResourceStream("GMX.config.settings.json");

					var resources = assembly.GetManifestResourceNames();
					var resourceName = resources.Single(r => r.EndsWith("settings.json", StringComparison.OrdinalIgnoreCase));
					var stream = assembly.GetManifestResourceStream(resourceName);

                    string text;
                    using (var reader = new StreamReader(stream))
                    {
                        text = reader.ReadToEnd();
                    }
                    cfg = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);
                }
                return cfg;
            }
        }
    }
}
