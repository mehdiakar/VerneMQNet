using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Session
{
	internal class VerneMQSessionInfo
	{
		public bool Clean_session { get; set; }
		public string Client_id { get; set; }
		public string Deliver_mode { get; set; }
		public bool Is_offline { get; set; }
		public bool Is_online { get; set; }
		public bool Is_plugin { get; set; }
		public string Mountpoint { get; set; }
		public string Node { get; set; }
		public int Num_sessions { get; set; }
		public int Offline_messages { get; set; }
		public int Online_messages { get; set; }
		public string Peer_host { get; set; }
		public int Peer_port { get; set; }
		public ConnectionProtocol Protocol { get; set; }
		public byte Qos { get; set; }
		public string Queue_pid { get; set; }
		public int? Queue_size { get; set; }
		public double Queue_started_at { get; set; }
		public string Session_pid { get; set; }
		public double Session_started_at { get; set; }
		public string Statename { get; set; }
		public string Topic { get; set; }
		public string User { get; set; }
		public string Waiting_acks { get; set; }

	}
}
