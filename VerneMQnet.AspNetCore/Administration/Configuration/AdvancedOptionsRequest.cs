using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Configuration
{
	public class AdvancedOptionsRequest
	{
		public QueueDeliverMode? QueueDeliverMode { get; set; }
		public QueueType? QueueType { get; set; }
		public int? MaxMessageRate { get; set; }
		public int? MaxDrainTime { get; set; }
		public int? MaxMessagesPerDrainStep { get; set; }
		public int? OutgoingClusteringBufferSize { get; set; }
		public string Node { get; set; }
		public bool ConfigAllNodes { get; set; }
	}
}
