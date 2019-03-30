using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks
{
	public sealed class ErrorResult:HookResult
	{
		public string Error { get; set; }
	}
}
