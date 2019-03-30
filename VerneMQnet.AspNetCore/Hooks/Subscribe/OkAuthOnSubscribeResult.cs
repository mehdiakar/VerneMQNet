using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks.Subscribe
{
	public sealed class OkAuthOnSubscribeResult: OkResult
	{
		public IEnumerable<TopicPayload> Topics { get; set; }
	}
}
