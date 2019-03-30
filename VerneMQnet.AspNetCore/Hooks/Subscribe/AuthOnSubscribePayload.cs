using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks.Subscribe
{
	public class AuthOnSubscribePayload
	{
		public string Mountpoint { get; set; }
		public string Client_id { get; set; }
		public string Username { get; set; }
		public IEnumerable<TopicPayload> Topics { get; set; }
	}
}
