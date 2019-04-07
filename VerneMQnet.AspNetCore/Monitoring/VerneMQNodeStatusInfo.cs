using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Monitoring
{
	internal class VerneMQNodeStatusInfo
	{
		public int Num_online { get; set; }
		public int Num_offline { get; set; }
		public int Msg_in { get; set; }
		public int Msg_out { get; set; }
		public int Queue_in { get; set; }
		public int Queue_out { get; set; }
		public int Queue_drop { get; set; }
		public int Queue_unhandled { get; set; }
		public int Num_subscriptions { get; set; }
		public int Num_retained { get; set; }
		public IEnumerable<Dictionary<string, bool>> Mystatus { get; set; }
		public IEnumerable<VerneMQListenerInfo> Listeners { get; set; }
		public string Version { get; set; }
	}

	internal class VerneMQListenerInfo
	{
		public string Type { get; set; }
		public string Status { get; set; }
		public string Ip { get; set; }
		public int Port { get; set; }
		public IEnumerable<string> Mountpoint { get; set; }
		public int Max_conns { get; set; }
	}
}
