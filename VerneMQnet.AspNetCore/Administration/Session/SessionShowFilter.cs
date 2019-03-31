using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Session
{
	public class SessionShowFilter
	{
		/// <summary>
		///Limit the number of results returned from each node in the cluster.
		///Defaults is 100.
		/// </summary>
		public int? Limit { get; set; }
		/// <summary>
		/// Limits the time spent when fetching a single row.
		/// Default is 100 milliseconds.
		/// </summary>
		public int? RowTimeout { get; set; }
		public string Node { get; set; }
		public string Mountpoint { get; set; }
		public string ClientId { get; set; }
		public string QueuePId { get; set; }
		/// <summary>
		/// Set -1 to search Undefined queue sizes
		/// </summary>
		public int? QueueSize { get; set; }
		public string SessionPId { get; set; }
		public bool? IsOffline { get; set; }
		public bool? IsOnline { get; set; }
		public Statename? Statename { get; set; }
		public QueueDeliverMode? DeliverMode { get; set; }
		public int? OfflineMessages { get; set; }
		public int? OnlineMessages { get; set; }
		public int? NumSessions { get; set; }
		public bool? CleanSession { get; set; }
		public bool? IsPlugin { get; set; }
		public double? QueueStartedAt { get; set; }
		public string Username { get; set; }
		public string PeerHost { get; set; }
		public int? PeerPort { get; set; }
		public ConnectionProtocol? Protocol { get; set; }
		public int? WaitingAcks { get; set; }
		public double? SessionStartedAt { get; set; }
		public string Topic { get; set; }
		public byte? Qos { get; set; }
	}
}
