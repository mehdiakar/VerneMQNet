using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks.Session
{
	public class OnAuthM5Payload
	{
		public string Username { get; set; }
		public string Mountpoint { get; set; }
		public string Client_id { get; set; }
		public AuthenticationProperties Properties { get; set; }
	}

}
