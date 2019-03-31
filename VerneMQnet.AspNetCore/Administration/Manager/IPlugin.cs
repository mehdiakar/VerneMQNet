using System.Collections.Generic;
using System.Threading.Tasks;
using VerneMQNet.AspNetCore.Administration.Plugin;

namespace VerneMQNet.AspNetCore.Administration.Manager
{
	/// <summary>
	/// Mangage plugins.
	/// </summary>
	public interface IPlugin
	{
		/// <summary>
		/// Disables either an application plugin
		/// For more information <seealso cref="https://docs.vernemq.com/configuring-vernemq/plugins#disable-a-plugin"/>
		/// </summary>
		/// <param name="request">plugin information</param>
		/// <returns></returns>
		Task<bool> Disable(DisableRequest request);
		/// <summary>
		/// Enables either an Application plugin or a module plugin. The application
		/// plugins are bundled as Erlang OTP apps.If the application code is not yet
		/// loaded you have to specify the --path=<PathToPlugin>. If the plugin is
		/// implemented in a single Erlang module make sure that the module is loaded.
		/// For more information <seealso cref="https://docs.vernemq.com/configuring-vernemq/plugins#enable-a-plugin"/>
		/// </summary>
		/// <param name="request">plugin information</param>
		/// <returns></returns>
		Task<bool> Enable(EnableRequest request);
		/// <summary>
		/// Shows the currently running plugins.
		/// For more information <seealso cref="https://docs.vernemq.com/configuring-vernemq/plugins"/>
		/// </summary>
		/// <param name="request">request options <see cref="PluginShowRequest"/></param>
		/// <returns></returns>
		Task<IEnumerable<PluginInfo>> Show(PluginShowRequest request = null);
	}
}