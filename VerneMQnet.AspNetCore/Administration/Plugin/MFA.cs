using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Plugin
{
	public class MFA
	{
		public string Module { get; set; }
		public string Function { get; set; }
		public int Arity { get; set; }
	}
}
