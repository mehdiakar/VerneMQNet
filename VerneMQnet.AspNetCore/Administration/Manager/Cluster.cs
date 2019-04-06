using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using VerneMQNet.AspNetCore.Administration.Cluster;

namespace VerneMQNet.AspNetCore.Administration.Manager
{
	/// <summary>
	/// Administrate cluster membership for particular VerneMQ node.
	/// </summary>
	public class Cluster : ICluster
	{
		IAdministrationConfiguration configuration;
		JsonMediaTypeFormatter jsonFormatter;
		private HttpClientHandler clientHandler;
		private readonly string showApiPath = "api/v1/cluster/show";
		private readonly string joinApiPath = "api/v1/cluster/join";
		private readonly string leaveApiPath = "api/v1/cluster/leave";
		public Cluster(IAdministrationConfiguration configuration)
		{
			this.configuration = configuration;
			this.jsonFormatter = new JsonMediaTypeFormatter();
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			this.clientHandler = new HttpClientHandler
			{
				Credentials = new NetworkCredential(this.configuration.ApiKey, "")
			};
		}
		/// <summary>
		///  Return cluster information
		///  For more information <see cref="https://docs.vernemq.com/vernemq-clustering/introduction#getting-cluster-status-information"/>
		/// </summary>
		/// <returns></returns>

		public async Task<IEnumerable<NodeInfo>> Show()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}{showApiPath}");
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Authorization Basic", this.configuration.ApiKey);
				var response = await client.GetAsync(builder.ToString()).ConfigureAwait(false);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var result = await response.Content.ReadAsAsync<TableBaseResponse<NodeInfo>>(new List<MediaTypeFormatter> { jsonFormatter });
					return result.Table;
				}
				else
					return new List<NodeInfo>();
			}
		}
		/// <summary>
		/// The discovery node will be used to find out about the nodes in the cluster.
		/// For more information <see cref="https://docs.vernemq.com/vernemq-clustering/introduction#joining-a-cluster"/>
		/// </summary>
		/// <param name="request">Node information to join</param>
		/// <returns></returns>
		public async Task<bool> Join(JoinNodeRequest request)
		{
			if (request == null || string.IsNullOrWhiteSpace(request.DiscoveryNode))
				throw new ArgumentNullException("DiscoveryNode", "DiscoveryNode value is required");
			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}{joinApiPath}?discovery-node={request.DiscoveryNode}");

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

		/// <summary>
		/// Graceful cluster-leave and shutdown of a cluster node.
		/// 
		///If <see cref="JoinNodeRequest.DiscoveryNode"/> is already offline its cluster membership gets removed, 
		///and the queues of the subscribers that have been connected at shutdown
		///will be recreated on other cluster nodes.This might involve
		///the disconnecting of clients that have already reconnected.
		///
		///If <see cref="JoinNodeRequest.DiscoveryNode"/> is still online all its MQTT listeners(including websockets)
		///are stopped and wont therefore accept new connections.Established
		///connections aren't cancelled at this point. Use KillSession to get
		///into the second phase of the graceful shutdown.
		///For more information <see cref="https://docs.vernemq.com/vernemq-clustering/introduction#leaving-a-cluster"/>
		/// </summary>
		/// <param name="request">node information to leave</param>
		/// <returns></returns>
		public async Task<bool> Leave(LeaveNodeRequest request)
		{

			if (request == null || string.IsNullOrWhiteSpace(request.Node))
				throw new ArgumentNullException("Node", "Node value is required");

			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}{leaveApiPath}?node={request.Node}");

			if(request.KillSessions)
				builder.Append("&--kill_sessions");

			if (request.SummeryInterval.HasValue)
				builder.Append($"&--summary-interval={request.SummeryInterval.Value}");

			if (request.Timeout.HasValue)
				builder.Append($"&--timeout={request.Timeout.Value}");

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
