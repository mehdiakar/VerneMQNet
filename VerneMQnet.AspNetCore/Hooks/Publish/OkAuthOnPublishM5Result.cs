using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks.Publish
{
	public class OkAuthOnPublishM5Result: OkResult
	{
		public string Topic { get; set; }
	}
}
