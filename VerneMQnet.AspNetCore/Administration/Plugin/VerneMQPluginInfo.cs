using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Plugin
{
	internal class VerneMQPluginInfo
	{
		[JsonProperty("Plugin")]
		public string Plugin { get; set; }

		[JsonProperty("Type")]
		public string Type { get; set; }

		[JsonProperty("Hook(s)")]
		public string Hooks { get; set; }

		[JsonProperty("M:F/A")]
		public string MFA { get; set; }
		
	}
}
