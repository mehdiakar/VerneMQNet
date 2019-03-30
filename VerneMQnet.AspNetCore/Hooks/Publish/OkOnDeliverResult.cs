using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks.Publish
{
	public sealed class OkOnDeliverResult : OkResult
	{
		public string Topic { get; set; }
		public string Payload { get; set; }
	}
}
