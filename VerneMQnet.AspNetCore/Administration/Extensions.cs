﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using VerneMQNet.AspNetCore.Administration.Session;

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
			if (sessions == null || !sessions.Any())
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
	}
}