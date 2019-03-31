using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Webhook
{
	public class WebhookInfo
	{
		public string Hook { get; set; }
		public string Endpoint { get; set; }
		public bool Base64payload { get; set; }
	}
}
