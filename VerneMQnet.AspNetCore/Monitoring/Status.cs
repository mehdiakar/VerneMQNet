using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace VerneMQNet.AspNetCore.Monitoring
{
	public class Status : IStatus
	{
		IMonitoringConfiguration configuration;
		JsonMediaTypeFormatter jsonFormatter;
		public Status(IMonitoringConfiguration configuration)
		{
			this.configuration = configuration;
			jsonFormatter = new JsonMediaTypeFormatter();
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
		}

		/// <summary>
		/// This method returns status of each node in VerneMQ cluster.
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<NodeStatusInfo>> Get()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}status.json");

			using (HttpClient client = new HttpClient())
			{
				var response = await client.GetAsync(builder.ToString()).ConfigureAwait(false);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var result = await response.Content.ReadAsAsync<IEnumerable<Dictionary<string, VerneMQNodeStatusInfo>>>(new List<MediaTypeFormatter> { jsonFormatter });
					return result.ConvertToStatusInfo();
				}
				else
					return new List<NodeStatusInfo>();
			}

		}
	}
}
