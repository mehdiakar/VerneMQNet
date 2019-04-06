using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VerneMQNet.AspNetCore.Monitoring
{
	public class HealthChecker : IHealthChecker
	{
		IMonitoringConfiguration configuration;

		public HealthChecker(IMonitoringConfiguration configuration)
		{
			this.configuration = configuration;
		}

		/// <summary>
		/// This method will return true when VerneMQ is accepting connections and is joined with the cluster (for clustered setups). false will be returned in case any of those two conditions are not met.
		/// For more information <see cref="https://docs.vernemq.com/monitoring/health-check"/>
		/// </summary>
		/// <returns>REturns true if server is healthy.</returns>
		public async Task<bool> IsHealthy()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}/health");
			using (HttpClient client = new HttpClient())
			{
				var response = await client.GetAsync(builder.ToString()).ConfigureAwait(false);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					return true;
				}
				else
					return false;
			}
		}

	}
}
