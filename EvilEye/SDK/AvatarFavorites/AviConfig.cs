using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.SDK.AvatarFavorites
{
    internal class AviConfig
	{
		//public List<AviObjects> Avis = new List<AviObjects>();
		public static AviConfig Instance;
		internal void SaveAviConfig()
        {
            string contents = JsonConvert.SerializeObject(this, (Formatting)1);
            File.WriteAllText("EvilEye/Avis.json", contents);
        }
		internal static AviConfig Load_AVIS_FAVE()
		{
			bool flag = !File.Exists("EvilEye/Avis.json");
			AviConfig result;
			if (flag)
			{
				result = new AviConfig();
			}
			else
			{
				AviConfig.Instance = JsonConvert.DeserializeObject<AviConfig>(File.ReadAllText("EvilEye/Avis.json"));
				result = AviConfig.Instance;
			}
			return result;
		}
	}
}
