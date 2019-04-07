using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace VerneMQNet.AspNetCore.Monitoring
{
	internal static class Extensions
	{
		public static string CreateUrl(this IMonitoringConfiguration configuration)
		{
			return $"http://{configuration.BaseAddress}:{configuration.Port ?? 8888}/";
		}

		public static IEnumerable<NodeStatusInfo> ConvertToStatusInfo(this IEnumerable<Dictionary<string, VerneMQNodeStatusInfo>> statusInfo)
		{
			return statusInfo.Select(x => new NodeStatusInfo
			{
				Listeners = x.First().Value.Listeners.Select(y => new ListenerInfo
				{
					IP = y.Ip,
					MaxConnectionsCount = y.Max_conns,
					Mountpoint = y.Mountpoint,
					Port = y.Port,
					Status = y.Status,
					Type = y.Type
				}).ToList(),
				MessageIn = x.First().Value.Msg_in,
				MessageOut = x.First().Value.Msg_out,
				Node = x.First().Key,
				NodeStatus = x.First().Value.Mystatus.Select(k => new NodeStatus
				{
					Node = k.First().Key,
					IsHealthy = k.First().Value
				}).ToList(),
				OfflineClients = x.First().Value.Num_offline,
				OnlineClients = x.First().Value.Num_online,
				QueueDrop = x.First().Value.Queue_drop,
				QueueIn = x.First().Value.Queue_in,
				QueueOut = x.First().Value.Queue_out,
				QueueUnhandled = x.First().Value.Queue_unhandled,
				RetainedCount = x.First().Value.Num_retained,
				SubscriptionsCount = x.First().Value.Num_subscriptions,
				Version = x.First().Value.Version
			}).ToList();
		}

	}
}
