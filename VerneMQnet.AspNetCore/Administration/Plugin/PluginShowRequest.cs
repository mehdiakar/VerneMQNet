using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Plugin
{
	public class PluginShowRequest
	{
		/// <summary>
		/// If true, only shows the hooks for the specified plugin
		/// </summary>
		public bool Plugin { get; set; }
		/// <summary>
		/// If true, only shows the plugins that provide callbacks for the specified hook
		/// </summary>
		public bool Hook { get; set; }
		/// <summary>
		///  If true, also show internal plugins
		/// </summary>
		public bool Internal { get; set; }
	}
}
