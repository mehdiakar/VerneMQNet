using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks.Subscribe
{
	public class OkOnUnsubscribeM5Result: OkResult
	{
		public IEnumerable<string> Topics { get; set; }
	}
}
