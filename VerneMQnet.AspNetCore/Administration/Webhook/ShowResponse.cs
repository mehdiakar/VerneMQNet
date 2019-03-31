using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Webhook
{
	internal class ShowResponse
	{
		public string Type { get; set; }
		public IEnumerable<WebhookInfo> Table { get; set; }
	}
}
