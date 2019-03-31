using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Session
{
	public class DisconnectRequest
	{
		public string ClientId { get; set; }
		public string Mountpoint { get; set; }
		public bool Cleanup { get; set; }
	}
}
