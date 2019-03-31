using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Plugin
{
	internal class PluginShowResponse
	{
		public string Type { get; set; }
		public IEnumerable<VerneMQPluginInfo> Table { get; set; }
	}
}
