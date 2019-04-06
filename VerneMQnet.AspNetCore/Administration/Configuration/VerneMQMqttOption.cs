using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Configuration
{
	internal class VerneMQMqttOption
	{
		public int Retry_interval { get; set; }
		public int Max_inflight_messages { get; set; }
		public int Max_online_messages { get; set; }
		public int Max_offline_messages { get; set; }
		public string Node { get; set; }
	}
}
