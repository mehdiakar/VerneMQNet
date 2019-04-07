using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Monitoring
{

	public class NodeStatusInfo
	{
		public string Node { get; set; }
		public int OnlineClients { get; set; }
		public int OfflineClients { get; set; }
		public int MessageIn { get; set; }
		public int MessageOut { get; set; }
		public int QueueIn { get; set; }
		public int QueueOut { get; set; }
		public int QueueDrop { get; set; }
		public int QueueUnhandled { get; set; }
		public int SubscriptionsCount { get; set; }
		public int RetainedCount { get; set; }
		public IEnumerable<NodeStatus> NodeStatus { get; set; }
		public IEnumerable<ListenerInfo> Listeners { get; set; }
		public string Version { get; set; }

	}

	public class NodeStatus
	{
		public string Node { get; set; }
		public bool IsHealthy { get; set; }
	}

	public class ListenerInfo
	{
		public string Type { get; set; }
		public string Status { get; set; }
		public string IP { get; set; }
		public int Port { get; set; }
		public IEnumerable<string> Mountpoint { get; set; }
		public int MaxConnectionsCount { get; set; }
	}
}
