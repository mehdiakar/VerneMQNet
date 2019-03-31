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
