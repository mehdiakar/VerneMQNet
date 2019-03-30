using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks.Publish
{
	public sealed class OkAuthOnPublishResult: OkResult
	{
		public string Topic { get; set; }
		public byte Qos { get; set; }
		public string Payload { get; set; }
		public bool Retain { get; set; }
	}
}
