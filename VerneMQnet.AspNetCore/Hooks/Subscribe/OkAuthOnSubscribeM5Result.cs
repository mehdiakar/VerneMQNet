using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks.Subscribe
{
	public class OkAuthOnSubscribeM5Result: OkResult
	{
		public IEnumerable<TopicPayload> Topics { get; set; }
	}


}
