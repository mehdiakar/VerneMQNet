using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Monitoring
{
	internal static class Extensions
	{
		public static string CreateUrl(this IMonitoringConfiguration configuration)
		{
			return $"http://{configuration.BaseAddress}:{configuration.Port ?? 8888}/";
		}

	}
}
