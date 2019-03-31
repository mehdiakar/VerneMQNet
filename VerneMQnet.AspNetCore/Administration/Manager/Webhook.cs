using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using VerneMQNet.AspNetCore.Administration.Webhook;

namespace VerneMQNet.AspNetCore.Administration.Manager
{
	/// <summary>
	/// Manage VerneMQ Webhooks.
	/// </summary>
	public class Webhook : IWebhook
	{
		IAdministrationConfiguration configuration;
		JsonMediaTypeFormatter jsonFormatter;
		private HttpClientHandler clientHandler;
		private readonly string showApiPath = "api/v1/webhooks/show";
		private readonly string registerApiPath = "api/v1/webhooks/register";
		private readonly string deregisterApiPath = "api/v1/webhooks/deregister";

		public Webhook(IAdministrationConfiguration configuration)
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
		/// Shows the information of the registered webhooks.
		/// For more information <see cref="https://docs.vernemq.com/plugin-development/webhookplugins#configuring-webhooks"/>
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<WebhookInfo>> Show()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}{showApiPath}");

			using (HttpClient client = new HttpClient(clientHandler))
			{
				var response = await client.GetAsync(builder.ToString()).ConfigureAwait(false);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var result = await response.Content.ReadAsAsync<ShowResponse>(new List<MediaTypeFormatter> { jsonFormatter });
					return result.Table;
				}
				else
					return new List<WebhookInfo>();
			}
		}

		/// <summary>
		///  Registers a webhook endpoint with a hook.
		///  For more information  <see cref="https://docs.vernemq.com/plugin-development/webhookplugins#configuring-webhooks"/>
		/// </summary>
		/// <param name="request">hook and endpoint information</param>
		/// <returns></returns>
		public async Task<bool> Register(RegisterRequest request)
		{
			if (string.IsNullOrEmpty(request?.Hook))
				throw new ArgumentNullException("Hook", "Hook value is required");
			if (string.IsNullOrEmpty(request?.Endpoint))
				throw new ArgumentNullException("Endpoint", "Endpoint value is required");

			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}{registerApiPath }?hook={request.Hook}& endpoint={request.Endpoint}");

			if(request.Base64payload.HasValue)
				builder.Append($"&--base64payload={request.Base64payload.Value.ToString().ToLower()}");

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
		///  Deregisters a webhook endpoint.
		///  For more information  <see cref="https://docs.vernemq.com/plugin-development/webhookplugins#configuring-webhooks"/>
		/// </summary>
		/// <param name="request">hook and endpoint information</param>
		/// <returns></returns>
		public async Task<bool> Deregister(DeregisterRequest request)
		{
			if (string.IsNullOrEmpty(request?.Hook))
				throw new ArgumentNullException("Hook", "Hook value is required");
			if (string.IsNullOrEmpty(request?.Endpoint))
				throw new ArgumentNullException("Endpoint", "Endpoint value is required");

			StringBuilder builder = new StringBuilder();
			builder.Append($"{this.configuration.CreateUrl()}{deregisterApiPath}?hook={request.Hook}& endpoint={request.Endpoint}");

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
