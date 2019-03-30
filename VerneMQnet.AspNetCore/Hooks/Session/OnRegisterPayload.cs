using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks.Session
{
	public class OnRegisterPayload
	{
		public string Peer_addr { get; set; }
		public int Peer_port { get; set; }
		public string Username { get; set; }
		public string Mountpoint { get; set; }
		public string Client_id { get; set; }
	}
}
