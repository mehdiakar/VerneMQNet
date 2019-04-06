using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using VerneMQNet.AspNetCore.Administration.Session;
using VerneMQNet.AspNetCore.Administration.Plugin;

namespace VerneMQNet.AspNetCore.Administration
{
	internal static class Extensions
	{
		public static string CreateUrl(this IAdministrationConfiguration configuration)
		{
			return $"http://{configuration.BaseAddress}:{configuration.Port ?? 8888}/";
		}

		public static IEnumerable<SessionInfo> ConvertToSessionInfo(this IEnumerable<VerneMQSessionInfo> sessions)
		{
			if (!sessions?.Any() ?? true)
				return new List<SessionInfo>();

			return sessions.Select(x => new SessionInfo
			{
				CleanSession = x.Clean_session,
				ClientId = x.Client_id,
				DeliverMode = QueueDeliverMode.Fanout.ToString().ToLower() == x.Deliver_mode.ToLower() ? QueueDeliverMode.Fanout : QueueDeliverMode.Balance,
				IsOffline = x.Is_offline,
				IsOnline = x.Is_online,
				IsPlugin = x.Is_plugin,
				Mountpoint = x.Mountpoint,
				Node = x.Node,
				NumSessions = x.Num_sessions,
				OfflineMessages = x.Offline_messages,
				OnlineMessages = x.Online_messages,
				PeerHost = x.Peer_host,
				PeerPort = x.Peer_port,
				Protocol = x.Protocol,
				Qos = x.Qos,
				QueuePId = x.Queue_pid,
				QueueSize = x.Queue_size,
				QueueStartedAt = x.Queue_started_at,
				SessionPId = x.Session_pid,
				SessionStartedAt = x.Session_started_at,
				Statename = Statename.Offline.ToString().ToLower() == x.Statename.ToLower() ? Statename.Offline : Statename.Online,
				Topic = x.Topic,
				Username = x.User,
				WaitingAcks = x.Waiting_acks
			}).ToList();
		}
		public static IEnumerable<Plugin.PluginInfo> ConvertToPluginInfo(this IEnumerable<Plugin.VerneMQPluginInfo> plugins)
		{
			if(!plugins?.Any() ?? true)
				return new List<PluginInfo>();

			return plugins.Select(x => new PluginInfo
			{
				Plugin = x.Plugin,
				Type = x.Type,
				Hooks = x.Hooks.ToPluginHooks(),
				MFAs = x.MFA.ToPluginMFA()
			});

		}

		public static IEnumerable<Configuration.AllowAnonymousStatus> ConvertToAllowAnonymousStatus(this IEnumerable<Configuration.VerneMQAllowAnonymousStatus> statuses)
		{
			if (!statuses?.Any() == null)
				return new List<Configuration.AllowAnonymousStatus>();

			return statuses.Select(x => new Configuration.AllowAnonymousStatus
			{
				AllowAnonymous = x.Allow_anonymous.ToLower() == "off" ? false : true,
				Node = x.Node
			}).ToList();

		}

		public static IEnumerable<Configuration.MqttOption> ConvertToMqttOption(this IEnumerable<Configuration.VerneMQMqttOption> options)
		{
			if (!options?.Any() == null)
				return new List<Configuration.MqttOption>();

			return options.Select(x => new Configuration.MqttOption
			{
				RetryInterval = x.Retry_interval,
				MaxInflightMessages = x.Max_inflight_messages,
				MaxOfflineMessages = x.Max_offline_messages,
				MaxOnlineMessages = x.Max_online_messages,
				Node = x.Node
			}).ToList();

		}
		public static IEnumerable<Configuration.NonStandardMqttOption> ConvertToNonStandardMqttOption(this IEnumerable<Configuration.VerneMQNonStandardMqttOption> options)
		{
			if (!options?.Any() == null)
				return new List<Configuration.NonStandardMqttOption>();

			return options.Select(x => new Configuration.NonStandardMqttOption
			{
				MaxClientIdSize = x.Max_client_id_size,
				MessageSizeLimit = x.Message_size_limit,
				PersistentClientExpirationInSeconds = x.Persistent_client_expiration,
				Node = x.Node
			}).ToList();

		}

		public static IEnumerable<Configuration.AdvancedOptions> ConvertToAdvancedOptions(this IEnumerable<Configuration.VerneMQAdvancedOptions> options)
		{
			if (!options?.Any() == null)
				return new List<Configuration.AdvancedOptions>();

			return options.Select(x => new Configuration.AdvancedOptions
			{
				MaxDrainTime = x.Max_drain_time,
				MaxMessageRate = x.Max_message_rate,
				MaxMessagesPerDrainStep = x.Max_msgs_per_drain_step,
				OutgoingClusteringBufferSize = x.Outgoing_clustering_buffer_size,
				QueueDeliverMode = QueueDeliverMode.Fanout.ToString().ToLower() == x.Queue_deliver_mode.ToLower() ? QueueDeliverMode.Fanout : QueueDeliverMode.Balance,
				QueueType = QueueType.Fifo.ToString().ToLower() == x.Queue_type.ToLower() ? QueueType.Fifo : QueueType.Lifo,
				Node = x.Node
			}).ToList();

		}
		


		public static IEnumerable<string> ToPluginHooks(this string hooks)
		{
			return hooks.Trim('\n').Split('\n').ToList();
		}

		public static IEnumerable<MFA> ToPluginMFA(this string mfa)
		{
			return mfa.Trim('\n').Split('\n').Select(x => {
				var item = x.Split(':', '/');
				return new MFA
				{
					Module = item[0],
					Function = item[1],
					Arity = Convert.ToInt32(item[2])
				};
				}).ToList();
		}
	}
}
