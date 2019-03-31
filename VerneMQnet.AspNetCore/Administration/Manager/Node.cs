using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace VerneMQNet.AspNetCore.Administration.Manager
{
	/// <summary>
	/// Administrate this VerneMQ node.
	/// </summary>
	public class Node
	{
		IAdministrationConfiguration configuration;
		private readonly string startApiPath = "api/v1/node/start";
		private readonly string stopApiPath = "api/v1/node/stop";

		public Node(IAdministrationConfiguration configuration)
		{
			this.configuration = configuration;
		}
		/// <summary>
		/// Starts the server application within node which has referred by configuration address.
		/// This is typically not necessary since the server application is started automatically when starting the service.
		/// </summary>
		/// <returns></returns>
		public async Task<bool> Start()
		{
			return await SendNodeRequest(this.startApiPath).ConfigureAwait(false);
		}

		/// <summary>
		///  Stops the server application within node which has referred by configuration address.
		///  This is typically not necessary since the server application is stopped automatically when the service is stopped.
		/// </summary>
		/// <returns></returns>
		public async Task<bool> Stop()
		{
			return await SendNodeRequest(this.stopApiPath).ConfigureAwait(false);
		}

		private async Task<bool> SendNodeRequest(string eventPath)
		{
			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}{eventPath}");
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Authorization Basic", this.configuration.ApiKey);
				var response = await client.GetAsync(builder.ToString()).ConfigureAwait(false);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
					return true;
				else
					return false;
			}
		}
	}
}
