using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks.Subscribe
{
	public class OnUnsubscribeM5Payload
	{
		public string Mountpoint { get; set; }
		public string Client_id { get; set; }
		public string Username { get; set; }
		public IEnumerable<string> Topics { get; set; }
		public EmptyProperties Properties { get; set; }
	}
}
