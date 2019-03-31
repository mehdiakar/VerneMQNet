using System.Collections.Generic;
using System.Threading.Tasks;
using VerneMQNet.AspNetCore.Administration.Cluster;

namespace VerneMQNet.AspNetCore.Administration.Manager
{
	/// <summary>
	/// Administrate cluster membership for particular VerneMQ node.
	/// </summary>
	public interface ICluster
	{
		/// <summary>
		/// The discovery node will be used to find out about the nodes in the cluster.
		/// For more information <see cref="https://docs.vernemq.com/vernemq-clustering/introduction#joining-a-cluster"/>
		/// </summary>
		/// <param name="request">Node information to join</param>
		/// <returns></returns>
		Task<bool> Join(JoinNodeRequest request);
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
		Task<bool> Leave(LeaveNodeRequest request);
		/// <summary>
		///  Return cluster information
		///  For more information <see cref="https://docs.vernemq.com/vernemq-clustering/introduction#getting-cluster-status-information"/>
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<NodeInfo>> Show();
	}
}