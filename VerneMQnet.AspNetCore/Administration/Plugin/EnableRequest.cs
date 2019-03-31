using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Plugin
{
	public class EnableRequest
	{
		/// <summary>
		/// The name of the plugin application. It is required.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// The path to the plugin application
		/// </summary>
		public string Path { get; set; }
		/// <summary>
		/// Name of the module that implements the plugin, not needed if <see cref="Name"/> is used
		/// </summary>
		public string Module { get; set; }
		/// <summary>
		/// Name of the function that implements the hook, not needed if <see cref="Name"/> is used
		/// </summary>
		public string Function { get; set; }

		/// <summary>
		/// Nr of arguments the function specified in <see cref="Function"/> takes
		/// </summary>
		public string Arity { get; set; }
		/// <summary>
		/// The hook name in case it differs from the function name (<see cref="Function"/>)
		/// </summary>
		public string Hook { get; set; }

	}
}
