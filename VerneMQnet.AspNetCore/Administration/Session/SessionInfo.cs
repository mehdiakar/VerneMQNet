using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Session
{
	public class SessionInfo
	{
		public bool CleanSession { get; set; }
		public string ClientId { get; set; }
		public QueueDeliverMode DeliverMode { get; set; }
		public bool IsOffline { get; set; }
		public bool IsOnline { get; set; }
		public bool IsPlugin { get; set; }
		public string Mountpoint { get; set; }
		public string Node { get; set; }
		public int NumSessions { get; set; }
		public int OfflineMessages { get; set; }
		public int OnlineMessages { get; set; }
		public string PeerHost { get; set; }
		public int PeerPort { get; set; }
		public ConnectionProtocol Protocol { get; set; }
		public byte Qos { get; set; }
		public string QueuePId { get; set; }
		public int? QueueSize { get; set; }
		public double QueueStartedAt { get; set; }
		public string SessionPId { get; set; }
		public double SessionStartedAt { get; set; }
		public Statename Statename { get; set; }
		public string Topic { get; set; }
		public string Username { get; set; }
		public string WaitingAcks { get; set; }
	}
}
