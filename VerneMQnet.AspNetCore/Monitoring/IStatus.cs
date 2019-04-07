using System.Collections.Generic;
using System.Threading.Tasks;

namespace VerneMQNet.AspNetCore.Monitoring
{
	public interface IStatus
	{
		/// <summary>
		/// This method returns status of each node in VerneMQ cluster.
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<NodeStatusInfo>> Get();
	}
}