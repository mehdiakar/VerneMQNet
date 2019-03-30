using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks
{
	internal static class HookResultExtensions
	{
		public static IActionResult GeneratePublicResult(this HookResult result)
		{
			if (result == null || result is OkResult)
			{
				return new OkObjectResult(new { Result = "ok" });
			}

			if (result is NextResult)
				return new OkObjectResult(new { Result = "next" });

			if (result is ErrorResult item)
				return new OkObjectResult(new { Result = new { item.Error } });

			return new OkObjectResult(new { Result = "ok" });

		}

		public static IActionResult GenerateOnDeliverResult(this HookResult result)
		{
			if (result == null || result is OkResult)
			{
				return new OkObjectResult(new { Result = "ok" });
			}

			if (result is NextResult)
				return new OkObjectResult(new { Result = "next" });

			return new OkObjectResult(new { Result = "ok" });
		}
	}
}
