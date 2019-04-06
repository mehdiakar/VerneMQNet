using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Configuration
{
	public class VerneMQNonStandardMqttOption
	{
		public int Max_client_id_size { get; set; }
		public int Persistent_client_expiration { get; set; }
		public int Message_size_limit { get; set; }
		public string Node { get; set; }

	}
}
