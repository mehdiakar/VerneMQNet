using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Configuration
{
	public class NonStandardMqttOptionRequest
	{
		/// <summary>
		/// Set the maximum size for client ids, MQTT v3.1 specifies a limit of 23 characters.
		/// This option default to 23.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/nonstandard#maximum-client-id-size"/>
		/// </summary>
		public int? MaxClientIdSize { get; set; }

		/// <summary>
		/// This option allows persistent clients (those with clean_session set to false) to be removed if they do not reconnect within a certain time frame.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/nonstandard#persistent-client-expiration"/>
		/// </summary>
		public int? PersistentClientExpirationValue { get; set; }
		/// <summary>
		/// This option allows persistent clients (those with clean_session set to false) to be removed if they do not reconnect within a certain time frame.
		/// This option defaults to never.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/nonstandard#persistent-client-expiration"/>
		/// </summary>
		public PersistentClientExpirationValueType? PersistentClientExpirationValueType { get; set; }

		/// <summary>
		/// Limit the maximum publish payload size in bytes that VerneMQ allows. Messages that exceed this size won't be accepted.
		/// Defaults to 0, which means that all valid messages are accepted. MQTT specification imposes a maximum payload size of 268435455 bytes.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/nonstandard#message-size-limit"/>
		/// </summary>
		public int? MessageSizeLimit { get; set; }

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
