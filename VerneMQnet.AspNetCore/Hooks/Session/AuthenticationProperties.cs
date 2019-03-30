using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks.Session
{
	public class AuthenticationProperties
	{
		public string P_authentication_data { get; set; }
		public string P_authentication_method { get; set; }
	}
}
