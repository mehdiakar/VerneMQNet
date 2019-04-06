using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Configuration
{
	public class MqttOptionRequest
	{
		/// <summary>
		/// Set the time in seconds after a QoS=1 or QoS=2 message has been sent that VerneMQ will wait before retrying when no response is received.
		/// This option default to 20 seconds.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/options#retry-interval"/>
		/// </summary>
		public int? RetryInterval { get; set; }
		/// <summary>
		/// This option defines the maximum number of QoS 1 or 2 messages that can be in the process of being transmitted simultaneously.
		/// Defaults to 20 messages, use 0 for no limit.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/options#inflight-messages"/>
		/// </summary>
		public int? MaxInflightMessages { get; set; }

		/// <summary>
		/// The maximum number of messages to hold in the queue above those messages that are currently in flight. Defaults to 1000. Set to -1 for no limit. This option protects a client session from overload by dropping messages (of any QoS).
		/// Defaults to 1000 messages, use -1 for no limit. 
		/// Note that 0 will totally block message delivery from any queue!
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/options#load-shedding"/>
		/// </summary>
		public int? MaxOnlineMessages { get; set; }

		/// <summary>
		/// This option specifies the maximum number of QoS 1 and 2 messages to hold in the offline queue.
		/// Defaults to 1000 messages, use -1 for no limit, use 0 if no messages should be stored.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/options#load-shedding"/>
		/// </summary>
		public int? MaxOfflineMessages { get; set; }
		/// <summary>
		/// To config particular node use this option. 
		/// </summary>
		public string Node { get; set; }
		/// <summary>
		/// If this option has been true, these options will config to all nodes axisted in cluster.
		/// </summary>
		public bool ConfigAllNodes { get; set; }
	}
}
