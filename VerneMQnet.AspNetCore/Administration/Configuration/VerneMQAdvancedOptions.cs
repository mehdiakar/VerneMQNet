using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Configuration
{
	internal class VerneMQAdvancedOptions
	{
		public string Queue_deliver_mode { get; set; }
		public string Queue_type { get; set; }
		public int Max_message_rate { get; set; }
		public int Max_drain_time { get; set; }
		public int Max_msgs_per_drain_step { get; set; }
		public int Outgoing_clustering_buffer_size { get; set; }
		public string Node { get; set; }
	}
}
