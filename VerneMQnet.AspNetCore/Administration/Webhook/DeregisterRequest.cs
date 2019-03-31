using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Webhook
{
	public class DeregisterRequest
	{
		public string Hook { get; set; }
		public string Endpoint { get; set; }
	}
}
