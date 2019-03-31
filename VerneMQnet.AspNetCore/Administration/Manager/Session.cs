using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using VerneMQNet.AspNetCore.Administration.Session;

namespace VerneMQNet.AspNetCore.Administration.Manager
{
	/// <summary>
	///  Manage MQTT sessions.
	/// </summary>
	public class Session
	{
		IAdministrationConfiguration configuration;
		JsonMediaTypeFormatter jsonFormatter;
		private readonly string showApiPath = "api/v1/session/show";
		private readonly string disconnectApiPath = "api/v1/session/disconnect";
		private readonly string reauthorizeApiPath = "api/v1/session/reauthorize";
		private const string responseParameters = "--node&--mountpoint&--client_id&--queue_pid&--queue_size&--session_pid&--is_offline&--is_online&--statename&--deliver_mode&--offline_messages&--online_messages&--num_sessions&--clean_session&--is_plugin&--queue_started_at&--user&--peer_host&--peer_port&--protocol&--waiting_acks&--session_started_at&--topic&--qos";

		private HttpClientHandler clientHandler;

		public Session(IAdministrationConfiguration configuration)
		{
			this.configuration = configuration;
			jsonFormatter = new JsonMediaTypeFormatter();
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			this.clientHandler = new HttpClientHandler
			{
				Credentials = new NetworkCredential(this.configuration.ApiKey, "")
			};
		}

		/// <summary>
		/// Show and filter information about MQTT sessions
		/// For More information <see cref="https://docs.vernemq.com/live-administration/managing-sessions#inspecting-sessions"/>
		/// </summary>
		/// <param name="filter">Optional items to filter sessions</param>
		/// <returns>List of client session information</returns>
		public async Task<IEnumerable<SessionInfo>> Show(SessionShowFilter filter = null)
		{
			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}{showApiPath}?{responseParameters}");
			if(filter != null) { 
			if (filter.CleanSession != null)
				builder.Append($"&--clean_session={filter.CleanSession}");


			if (!string.IsNullOrEmpty(filter.ClientId))
				builder.Append($"&--client_id={filter.ClientId}");

			if (filter.DeliverMode.HasValue)
				builder.Append($"&--deliver_mode={filter.DeliverMode.Value.ToString().ToLower()}");

			if (filter.IsOffline.HasValue)
				builder.Append($"&--is_offline={filter.IsOffline.Value}");


			if (filter.IsOnline.HasValue)
				builder.Append($"&--is_online={filter.IsOnline.Value}");

			if (filter.IsPlugin.HasValue)
				builder.Append($"&--is_plugin={filter.IsPlugin.Value}");

			if (filter.Limit.HasValue)
				builder.Append($"&--limit={filter.Limit.Value}");

			if (!string.IsNullOrWhiteSpace(filter.Mountpoint))
				builder.Append($"&--mountpoint={filter.Mountpoint}");

			if (!string.IsNullOrWhiteSpace(filter.Node))
				builder.Append($"&--node={filter.Node}");

			if (filter.NumSessions.HasValue)
				builder.Append($"&--num_sessions={filter.NumSessions.Value}");

			if (filter.OfflineMessages.HasValue)
				builder.Append($"&--offline_messages={filter.OfflineMessages.Value}");

			if (filter.OnlineMessages.HasValue)
				builder.Append($"&--online_messages={filter.OnlineMessages.Value}");

			if (!string.IsNullOrWhiteSpace(filter.PeerHost))
				builder.Append($"&--peer_host={filter.PeerHost}");

			if (filter.PeerPort.HasValue)
				builder.Append($"&--peer_port={filter.PeerPort.Value}");

			if (filter.Protocol.HasValue)
				builder.Append($"&--num_sessions={(int)filter.Protocol.Value}");

			if (filter.Qos.HasValue)
				builder.Append($"&--qos={filter.Qos}");

			if (!string.IsNullOrWhiteSpace(filter.QueuePId))
				builder.Append($"&--queue_pid={filter.QueuePId}");

			if (filter.QueueSize.HasValue)
				builder.Append($"&--queue_size={filter.QueueSize}");

			if (filter.QueueStartedAt.HasValue)
				builder.Append($"&--queue_started_at={filter.QueueStartedAt}");

			if (filter.RowTimeout.HasValue)
				builder.Append($"&--rowtimeout={filter.RowTimeout}");

			if (!string.IsNullOrWhiteSpace(filter.SessionPId))
				builder.Append($"&--session_pid={filter.SessionPId}");

			if (filter.SessionStartedAt.HasValue)
				builder.Append($"&--session_started_at={filter.SessionStartedAt}");

			if (filter.Statename.HasValue)
				builder.Append($"&--statename={filter.Statename.Value.ToString().ToLower()}");

			if (!string.IsNullOrWhiteSpace(filter.Topic))
				builder.Append($"&--topic={filter.Topic}");

			if (!string.IsNullOrWhiteSpace(filter.Username))
				builder.Append($"&--user={filter.Username}");

			if (filter.WaitingAcks.HasValue)
				builder.Append($"&--waiting_acks={filter.WaitingAcks.Value}");

			}

			using (HttpClient client = new HttpClient(clientHandler))
			{
				var response = await client.GetAsync(builder.ToString()).ConfigureAwait(false);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var result = await response.Content.ReadAsAsync<SessionShowResponse>(new List<MediaTypeFormatter> { jsonFormatter });
					return result.Table.ConvertToSessionInfo();
				}
				else
					return new List<SessionInfo>();
			}
		}

		/// <summary>
		/// Forcefully disconnects a client from the cluster.
		/// For More information <see cref="https://docs.vernemq.com/live-administration/managing-sessions#managing-sessions"/>
		/// </summary>
		/// <param name="request">session information to disconnect. ClientId is required</param>
		/// <returns>return true if client has been disconnected successfully.</returns>
		public async Task<bool> Disconnect(DisconnectRequest request)
		{
			if (request == null || string.IsNullOrWhiteSpace(request.ClientId))
				throw new ArgumentNullException(nameof(request.ClientId), "ClientId value is required");

			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}{disconnectApiPath}?client-id={request.ClientId}");

			if(request.Cleanup)
				builder.Append("&--cleanup");

			if(!string.IsNullOrWhiteSpace(request.Mountpoint))
				builder.Append($"--mountpoint={request.Mountpoint}");

			using (HttpClient client = new HttpClient(this.clientHandler))
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
		///  Reauthorizes all current subscriptions of an existing client session.
		/// For More information <see cref="https://docs.vernemq.com/live-administration/managing-sessions#managing-sessions"/>
		/// </summary>
		/// <param name="request">Session information to force reauthorization. ClientId and Username are required.</param>
		/// <returns>Return true if reauthorize configuration has been set successfully. </returns>
		public async Task<bool> Reauthorize(ReauthorizeRequest request)
		{
			if (request == null || string.IsNullOrWhiteSpace(request.ClientId) || string.IsNullOrWhiteSpace(request.Username))
				throw new ArgumentNullException("ClientId and Username are required");

			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}{reauthorizeApiPath}?client-id={request.ClientId}&username={request.Username}");

			if (!string.IsNullOrWhiteSpace(request.Mountpoint))
				builder.Append($"--mountpoint={request.Mountpoint}");

			using (HttpClient client = new HttpClient(this.clientHandler))
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
