using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using VerneMQNet.AspNetCore.Administration.Configuration;

namespace VerneMQNet.AspNetCore.Administration.Manager
{
	public class Configuration : IConfiguration
	{
		IAdministrationConfiguration configuration;
		JsonMediaTypeFormatter jsonFormatter;
		private HttpClientHandler clientHandler;
		private readonly string allowAnonymousApiPath = "api/v1/show/allow_anonymous";
		private readonly string setBasePath = "api/v1/set?";
		private readonly string mqttOptionsApiPath = "api/v1/show/retry_interval/max_inflight_messages/max_online_messages/max_offline_messages";
		private readonly string nonStandardMqttOptionsApiPath = "api/v1/show/max_client_id_size/persistent_client_expiration/message_size_limit";
		private readonly string advancedOptionsInfoApiPath = "api/v1/show/queue_deliver_mode/queue_type/max_message_rate/max_drain_time/max_msgs_per_drain_step/outgoing_clustering_buffer_size"; 
		public Configuration(IAdministrationConfiguration configuration)
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
		/// Returns allow_anonymous status for each node.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/file-auth#authentication"/>
		/// </summary>
		/// <returns>list of allow_anonymous status of nodes</returns>
		public async Task<IEnumerable<AllowAnonymousStatus>> GetAllowAnonymousStatus()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}{allowAnonymousApiPath}");

			using (HttpClient client = new HttpClient(clientHandler))
			{
				var response = await client.GetAsync(builder.ToString()).ConfigureAwait(false);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var result = await response.Content.ReadAsAsync<TableBaseResponse<VerneMQAllowAnonymousStatus>>(new List<MediaTypeFormatter> { jsonFormatter });
					return result.Table.ConvertToAllowAnonymousStatus();
				}
				else
					return new List<AllowAnonymousStatus>();
			}
		}

		/// <summary>
		/// This method config allow_anonymous for all nodes or particular node
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/file-auth#authentication"/>
		/// </summary>
		/// <param name="request">configuration information to config allow_anonymous. Set <see cref="AllowAnonymousRequest.AllowAnonymous"/> to true to enable it or false to disable it.</param>
		/// <returns>Return true if everything is OK.</returns>
		public async Task<bool> SetAllowAnonymous(AllowAnonymousRequest request)
		{
			StringBuilder builder = new StringBuilder();
			var anonymousConfig = request.AllowAnonymous ? "on" : "off";
			builder.Append($"{this.configuration.CreateUrl()}{setBasePath}allow_anonymous={anonymousConfig}");

			if(request.ConfigAllNodes)
			{
				builder.Append("&--all");
			}
			else if (!string.IsNullOrWhiteSpace(request.Node))
			{
				builder.Append($"&--node={request.Node}");
			}

			using (HttpClient client = new HttpClient(clientHandler))
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
		/// <summary>
		/// Returns MQTT options of each node.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/options"/>
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<MqttOption>> GetMqttOptions()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}{mqttOptionsApiPath}");

			using (HttpClient client = new HttpClient(clientHandler))
			{
				var response = await client.GetAsync(builder.ToString()).ConfigureAwait(false);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var result = await response.Content.ReadAsAsync<TableBaseResponse<VerneMQMqttOption>>(new List<MediaTypeFormatter> { jsonFormatter });
					return result.Table.ConvertToMqttOption();
				}
				else
					return new List<MqttOption>();
			}
		}

		/// <summary>
		/// This method config MQTT options for all nodes or particular node.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/options"/>
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public async Task<bool> SetMqttOptions(MqttOptionRequest request)
		{
			if (!request.MaxInflightMessages.HasValue && !request.MaxOfflineMessages.HasValue && !request.MaxOnlineMessages.HasValue && !request.RetryInterval.HasValue)
				return true;

			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}{setBasePath}");

			if (request.MaxInflightMessages.HasValue)
				builder.Append($"&max_inflight_messages={request.MaxInflightMessages.Value}");

			if (request.MaxOfflineMessages.HasValue)
				builder.Append($"&max_offline_messages={request.MaxOfflineMessages.Value}");

			if (request.MaxOnlineMessages.HasValue)
				builder.Append($"&max_online_messages={request.MaxOnlineMessages.Value}");

			if (request.RetryInterval.HasValue)
				builder.Append($"&retry_interval={request.RetryInterval.Value}");


			if (request.ConfigAllNodes)
			{
				builder.Append("&--all");
			}
			else if (!string.IsNullOrWhiteSpace(request.Node))
			{
				builder.Append($"&--node={request.Node}");
			}

			using (HttpClient client = new HttpClient(clientHandler))
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


		/// <summary>
		/// Returns non standard MQTT options for all server nodes.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/nonstandard"/>
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<NonStandardMqttOption>> GetNonStandardMqttOptions()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}{nonStandardMqttOptionsApiPath}");

			using (HttpClient client = new HttpClient(clientHandler))
			{
				var response = await client.GetAsync(builder.ToString()).ConfigureAwait(false);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var result = await response.Content.ReadAsAsync<TableBaseResponse<VerneMQNonStandardMqttOption>>(new List<MediaTypeFormatter> { jsonFormatter });
					return result.Table.ConvertToNonStandardMqttOption();
				}
				else
					return new List<NonStandardMqttOption>();
			}
		}

		/// <summary>
		/// This method config VerneMQ specific MQTT options for all nodes or particular node.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/nonstandard"/>
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public async Task<bool> SetNonStandardMqttOptions(NonStandardMqttOptionRequest request)
		{
			if (!request.MaxClientIdSize.HasValue && !request.MessageSizeLimit.HasValue && !request.PersistentClientExpirationValue.HasValue && (!request.PersistentClientExpirationValueType.HasValue || (request.PersistentClientExpirationValueType.HasValue && request.PersistentClientExpirationValueType.Value != PersistentClientExpirationValueType.Never)))
				return true;

			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}{setBasePath}");

			if (request.MaxClientIdSize.HasValue)
				builder.Append($"&max_client_id_size={request.MaxClientIdSize.Value}");

			if (request.PersistentClientExpirationValueType.HasValue)
			{
				
				if(request.PersistentClientExpirationValueType.Value == PersistentClientExpirationValueType.Never)
					builder.Append("&persistent_client_expiration=never");
				else
				{
					builder.Append($"&persistent_client_expiration={request.PersistentClientExpirationValue.Value}{GetPersistentClientExpirationValueTypeAsString(request.PersistentClientExpirationValueType.Value)}");
				}
			}

			if (request.MessageSizeLimit.HasValue)
				builder.Append($"&message_size_limit={request.MessageSizeLimit.Value}");


			if (request.ConfigAllNodes)
			{
				builder.Append("&--all");
			}
			else if (!string.IsNullOrWhiteSpace(request.Node))
			{
				builder.Append($"&--node={request.Node}");
			}

			using (HttpClient client = new HttpClient(clientHandler))
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

		/// <summary>
		/// Returns advanced options for all nodes in cluster.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/advanced_options"/>
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<AdvancedOptions>> GetAdvancedOptionInfo()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}{advancedOptionsInfoApiPath}");

			using (HttpClient client = new HttpClient(clientHandler))
			{
				var response = await client.GetAsync(builder.ToString()).ConfigureAwait(false);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var result = await response.Content.ReadAsAsync<TableBaseResponse<VerneMQAdvancedOptions>>(new List<MediaTypeFormatter> { jsonFormatter });
					return result.Table.ConvertToAdvancedOptions();
				}
				else
					return new List<AdvancedOptions>();
			}
		}

		/// <summary>
		/// This method config VerneMQ Advance options for all nodes or particular node.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/advanced_options"/>
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public async Task<bool> SetAdvancedOptions(AdvancedOptionsRequest request)
		{
			if (!request.MaxDrainTime.HasValue && !request.MaxMessageRate.HasValue && !request.MaxMessagesPerDrainStep.HasValue && !request.OutgoingClusteringBufferSize.HasValue && !request.QueueDeliverMode.HasValue && !request.QueueType.HasValue)
				return true;

			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}{setBasePath}");

			if (request.MaxDrainTime.HasValue)
				builder.Append($"&max_drain_time={request.MaxDrainTime.Value}");

			if (request.MaxMessageRate.HasValue)
				builder.Append($"&max_message_rate={request.MaxMessageRate.Value}");

			if (request.MaxMessagesPerDrainStep.HasValue)
				builder.Append($"&max_msgs_per_drain_step={request.MaxMessagesPerDrainStep.Value}");

			if (request.OutgoingClusteringBufferSize.HasValue)
				builder.Append($"&outgoing_clustering_buffer_size={request.OutgoingClusteringBufferSize.Value}");

			if (request.QueueDeliverMode.HasValue)
				builder.Append($"&queue_deliver_mode={request.QueueDeliverMode.Value.ToString().ToLower()}");

			if (request.QueueType.HasValue)
				builder.Append($"&queue_type={request.QueueType.Value.ToString().ToLower()}");


			if (request.ConfigAllNodes)
			{
				builder.Append("&--all");
			}
			else if (!string.IsNullOrWhiteSpace(request.Node))
			{
				builder.Append($"&--node={request.Node}");
			}

			using (HttpClient client = new HttpClient(clientHandler))
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

		private string GetPersistentClientExpirationValueTypeAsString(PersistentClientExpirationValueType value)
		{
			switch (value)
			{
				case PersistentClientExpirationValueType.Never:
					return "never";
				case PersistentClientExpirationValueType.Hour:
					return "h";
				case PersistentClientExpirationValueType.Day:
					return "d";
				case PersistentClientExpirationValueType.Week:
					return "w";
				case PersistentClientExpirationValueType.Month:
					return "m";
				case PersistentClientExpirationValueType.Year:
					return "y";
				default:
					return "never";
			}
		}
	}
}
