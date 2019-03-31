using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Plugin
{
	public class PluginInfo
	{
		public string Plugin { get; set; }
		public string Type { get; set; }
		public IEnumerable<string> Hooks { get; set; }
		public IEnumerable<MFA> MFAs { get; set; }
	}
}
