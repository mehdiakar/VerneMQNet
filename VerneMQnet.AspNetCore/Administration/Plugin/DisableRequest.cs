using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Plugin
{
	public class DisableRequest
	{
		/// <summary>
		/// The name of the plugin application. It is required.
		/// </summary>
		public string Name { get; set; }
	}
}
