using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Configuration
{
	public class MqttOption
	{
		/// <summary>
		/// The time in seconds after a QoS=1 or QoS=2 message has been sent that VerneMQ will wait before retrying when no response is received.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/options#retry-interval"/>
		/// </summary>
		public int RetryInterval { get; set; }

		/// <summary>
		/// The maximum number of QoS 1 or 2 messages that can be in the process of being transmitted simultaneously.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/options#inflight-messages"/>
		/// </summary>
		public int MaxInflightMessages { get; set; }

		/// <summary>
		/// The maximum number of messages to hold in the queue above those messages that are currently in flight.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/options#load-shedding"/>
		/// </summary>
		public int MaxOnlineMessages { get; set; }

		/// <summary>
		/// This option specifies the maximum number of QoS 1 and 2 messages to hold in the offline queue.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/options#load-shedding"/>
		/// </summary>
		public int MaxOfflineMessages { get; set; }
		public string Node { get; set; }
	}
}
