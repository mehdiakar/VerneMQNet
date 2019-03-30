using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks.Session
{
	public class OnClientWakeUpPayload
	{
		public string Client_id { get; set; }
		public string Mountpoint { get; set; }
	}
}
