using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks.Publish
{
	public class OnDeliverM5Payload
	{
		public string Mountpoint { get; set; }
		public string Client_id { get; set; }
		public string Username { get; set; }
		public string Topic { get; set; }
		public string Payload { get; set; }
		public EmptyProperties Properties { get; set; }
	}
}
