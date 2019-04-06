using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Configuration
{
	public class NonStandardMqttOption
	{
		/// <summary>
		/// The maximum size for client ids, MQTT v3.1 specifies a limit of 23 characters.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/nonstandard#maximum-client-id-size"/>
		/// </summary>
		public int MaxClientIdSize { get; set; }
		/// <summary>
		/// This option shows persistent clients expiration in seconds.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/nonstandard#persistent-client-expiration"/>
		/// </summary>
		public int PersistentClientExpirationInSeconds { get; set; }
		/// <summary>
		/// The maximum publish payload size in bytes that VerneMQ allows. Messages that exceed this size won't be accepted.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/nonstandard#message-size-limit"/>
		/// </summary>
		public int MessageSizeLimit { get; set; }
		public string Node { get; set; }
	}
}
