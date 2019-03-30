using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks.Session
{
	public class OkAuthM5Result: OkResult
	{
		public AuthenticationProperties Properties { get; set; }
		public byte ReasonCode { get; set; }
	}


}
