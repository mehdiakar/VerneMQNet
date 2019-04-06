using System.Threading.Tasks;

namespace VerneMQNet.AspNetCore.Monitoring
{
	public interface IHealthChecker
	{
		/// <summary>
		/// This method will return true when VerneMQ is accepting connections and is joined with the cluster (for clustered setups). false will be returned in case any of those two conditions are not met.
		/// For more information <see cref="https://docs.vernemq.com/monitoring/health-check"/>
		/// </summary>
		/// <returns>REturns true if server is healthy.</returns>
		Task<bool> IsHealthy();
	}
}