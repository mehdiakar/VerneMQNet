using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks.Session
{
	public sealed class OkAuthOnRegisterResult: OkResult
	{
		public int MaxMessageSize { get; set; }
		public int MaxInflightMessages { get; set; }
		public int RetryInterval { get; set; }
	}
}
