using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Configuration
{
	public class AllowAnonymousRequest
	{
		public bool AllowAnonymous { get; set; }
		public string Node { get; set; }
		public bool ConfigAllNodes { get; set; }
	}
}
