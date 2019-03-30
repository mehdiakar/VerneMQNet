using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks
{
	public class TopicPayload
	{
		public string Topic { get; set; }
		public byte Qos { get; set; }
	}
}
