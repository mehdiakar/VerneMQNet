using System.Collections.Generic;
using System.Threading.Tasks;
using VerneMQNet.AspNetCore.Administration.Configuration;

namespace VerneMQNet.AspNetCore.Administration.Manager
{
	public interface IConfiguration
	{
		/// <summary>
		/// Returns advanced options for all nodes in cluster.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/advanced_options"/>
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<AdvancedOptions>> GetAdvancedOptionInfo();

		/// <summary>
		/// Returns allow_anonymous status for each node.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/file-auth#authentication"/>
		/// </summary>
		/// <returns>list of allow_anonymous status of nodes</returns>
		Task<IEnumerable<AllowAnonymousStatus>> GetAllowAnonymousStatus();

		/// <summary>
		/// Returns MQTT options of each node.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/options"/>
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<MqttOption>> GetMqttOptions();

		/// <summary>
		/// Returns non standard MQTT options for all server nodes.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/nonstandard"/>
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<NonStandardMqttOption>> GetNonStandardMqttOptions();

		/// <summary>
		/// This method config VerneMQ Advance options for all nodes or particular node.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/advanced_options"/>
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		Task<bool> SetAdvancedOptions(AdvancedOptionsRequest request);

		/// <summary>
		/// This method config allow_anonymous for all nodes or particular node
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/file-auth#authentication"/>
		/// </summary>
		/// <param name="request">configuration information to config allow_anonymous. Set <see cref="AllowAnonymousRequest.AllowAnonymous"/> to true to enable it or false to disable it.</param>
		/// <returns>Return true if everything is OK.</returns>
		Task<bool> SetAllowAnonymous(AllowAnonymousRequest request);

		/// <summary>
		/// This method config MQTT options for all nodes or particular node.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/options"/>
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		Task<bool> SetMqttOptions(MqttOptionRequest request);

		/// <summary>
		/// This method config VerneMQ specific MQTT options for all nodes or particular node.
		/// For more information <see cref="https://docs.vernemq.com/configuring-vernemq/nonstandard"/>
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		Task<bool> SetNonStandardMqttOptions(NonStandardMqttOptionRequest request);
	}
}