using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using VerneMQNet.AspNetCore.Administration.Plugin;

namespace VerneMQNet.AspNetCore.Administration.Manager
{
	/// <summary>
	/// Mangage plugins.
	/// </summary>
	public class Plugin : IPlugin
	{
		IAdministrationConfiguration configuration;
		JsonMediaTypeFormatter jsonFormatter;
		private HttpClientHandler clientHandler;
		private readonly string showApiPath = "api/v1/plugin/show";
		private readonly string enableApiPath = "api/v1/plugin/enable";
		private readonly string disableApiPath = "api/v1/plugin/disable";
		public Plugin(IAdministrationConfiguration configuration)
		{
			this.configuration = configuration;
			this.jsonFormatter = new JsonMediaTypeFormatter();
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			this.clientHandler = new HttpClientHandler
			{
				Credentials = new NetworkCredential(this.configuration.ApiKey, "")
			};
		}

		/// <summary>
		/// Shows the currently running plugins.
		/// For more information <seealso cref="https://docs.vernemq.com/configuring-vernemq/plugins"/>
		/// </summary>
		/// <param name="request">request options <see cref="PluginShowRequest"/></param>
		/// <returns></returns>
		public async Task<IEnumerable<PluginInfo>> Show(PluginShowRequest request = null)
		{
			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}{showApiPath}?");

			if (request != null)
			{
				if (request.Plugin)
					builder.Append("&--plugin");
				if (request.Hook)
					builder.Append("&--hook");
				if (request.Internal)
					builder.Append("&--internal");
			}

			using (HttpClient client = new HttpClient(clientHandler))
			{
				var response = await client.GetAsync(builder.ToString()).ConfigureAwait(false);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var result = await response.Content.ReadAsAsync<TableBaseResponse<VerneMQPluginInfo>>(new List<MediaTypeFormatter> { jsonFormatter });
					return result.Table.ConvertToPluginInfo();
				}
				else
					return new List<PluginInfo>();
			}
		}

		/// <summary>
		/// Enables either an Application plugin or a module plugin. The application
		/// plugins are bundled as Erlang OTP apps.If the application code is not yet
		/// loaded you have to specify the --path=<PathToPlugin>. If the plugin is
		/// implemented in a single Erlang module make sure that the module is loaded.
		/// For more information <seealso cref="https://docs.vernemq.com/configuring-vernemq/plugins#enable-a-plugin"/>
		/// </summary>
		/// <param name="request">plugin information</param>
		/// <returns></returns>
		public async Task<bool> Enable(EnableRequest request)
		{
			if (request == null || string.IsNullOrWhiteSpace(request.Name))
				throw new ArgumentNullException("Name", " Plugin name is required");

			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}{enableApiPath }?--name={request.Name}");


			if (!string.IsNullOrWhiteSpace(request.Path))
				builder.Append($"&--path={request.Path}");

			if (!string.IsNullOrWhiteSpace(request.Module))
				builder.Append($"&--module={request.Module}");

			if (!string.IsNullOrWhiteSpace(request.Function))
				builder.Append($"&--function={request.Function}");

			if (!string.IsNullOrWhiteSpace(request.Arity))
				builder.Append($"&--arity={request.Arity}");

			if (!string.IsNullOrWhiteSpace(request.Hook))
				builder.Append($"&--hook={request.Hook}");

			using (HttpClient client = new HttpClient(clientHandler))
			{
				var response = await client.GetAsync(builder.ToString()).ConfigureAwait(false);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					return true;
				}
				else
					return false;
			}
		}

		/// <summary>
		/// Disables either an application plugin
		/// For more information <seealso cref="https://docs.vernemq.com/configuring-vernemq/plugins#disable-a-plugin"/>
		/// </summary>
		/// <param name="request">plugin information</param>
		/// <returns></returns>
		public async Task<bool> Disable(DisableRequest request)
		{
			if (request == null || string.IsNullOrWhiteSpace(request.Name))
				throw new ArgumentNullException("Name", " Plugin name is required");

			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}{disableApiPath}?--name={request.Name}");

			using (HttpClient client = new HttpClient(clientHandler))
			{
				var response = await client.GetAsync(builder.ToString()).ConfigureAwait(false);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					return true;
				}
				else
					return false;
			}
		}

	}
}
