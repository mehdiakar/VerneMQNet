using System.Collections.Generic;
using System.Threading.Tasks;
using VerneMQNet.AspNetCore.Administration.Session;

namespace VerneMQNet.AspNetCore.Administration.Manager
{
	/// <summary>
	///  Manage MQTT sessions.
	/// </summary>
	public interface ISession
	{
		/// <summary>
		/// Forcefully disconnects a client from the cluster.
		/// For More information <see cref="https://docs.vernemq.com/live-administration/managing-sessions#managing-sessions"/>
		/// </summary>
		/// <param name="request">session information to disconnect. ClientId is required</param>
		/// <returns>return true if client has been disconnected successfully.</returns>
		Task<bool> Disconnect(DisconnectRequest request);

		/// <summary>
		///  Reauthorizes all current subscriptions of an existing client session.
		/// For More information <see cref="https://docs.vernemq.com/live-administration/managing-sessions#managing-sessions"/>
		/// </summary>
		/// <param name="request">Session information to force reauthorization. ClientId and Username are required.</param>
		/// <returns>Return true if reauthorize configuration has been set successfully. </returns>
		Task<bool> Reauthorize(ReauthorizeRequest request);

		/// <summary>
		/// Show and filter information about MQTT sessions
		/// For More information <see cref="https://docs.vernemq.com/live-administration/managing-sessions#inspecting-sessions"/>
		/// </summary>
		/// <param name="filter">Optional items to filter sessions</param>
		/// <returns>List of client session information</returns>
		Task<IEnumerable<SessionInfo>> Show(SessionShowFilter filter = null);
	}
}