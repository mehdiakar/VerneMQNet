using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Session
{
	internal class SessionShowResponse
	{
		public IEnumerable<VerneMQSessionInfo> Table { get; set; }
		public string Type { get; set; }
	}
}
