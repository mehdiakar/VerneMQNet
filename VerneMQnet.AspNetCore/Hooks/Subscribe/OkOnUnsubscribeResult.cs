using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks.Subscribe
{
	public sealed class OkOnUnsubscribeResult: OkResult
	{
		public IEnumerable<string> Topics { get; set; }
	}
}
