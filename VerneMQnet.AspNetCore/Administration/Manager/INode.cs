using System.Threading.Tasks;

namespace VerneMQNet.AspNetCore.Administration.Manager
{
	/// <summary>
	/// Administrate this VerneMQ node.
	/// </summary>
	public interface INode
	{
		/// <summary>
		/// Starts the server application within node which has referred by configuration address.
		/// This is typically not necessary since the server application is started automatically when starting the service.
		/// </summary>
		/// <returns></returns>
		Task<bool> Start();

		/// <summary>
		///  Stops the server application within node which has referred by configuration address.
		///  This is typically not necessary since the server application is stopped automatically when the service is stopped.
		/// </summary>
		/// <returns></returns>
		Task<bool> Stop();
	}
}