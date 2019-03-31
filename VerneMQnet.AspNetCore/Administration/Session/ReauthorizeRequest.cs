using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Session
{
	public class ReauthorizeRequest
	{
		public string ClientId { get; set; }
		public string Username { get; set; }
		public string Mountpoint { get; set; }
	}
}
