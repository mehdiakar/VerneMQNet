using System.Collections.Generic;
using System.Threading.Tasks;
using VerneMQNet.AspNetCore.Administration.Webhook;

namespace VerneMQNet.AspNetCore.Administration.Manager
{
	/// <summary>
	/// Manage VerneMQ Webhooks.
	/// </summary>
	public interface IWebhook
	{
		/// <summary>
		///  Deregisters a webhook endpoint.
		///  For more information  <see cref="https://docs.vernemq.com/plugin-development/webhookplugins#configuring-webhooks"/>
		/// </summary>
		/// <param name="request">hook and endpoint information</param>
		/// <returns></returns>
		Task<bool> Deregister(DeregisterRequest request);
		/// <summary>
		///  Registers a webhook endpoint with a hook.
		///  For more information  <see cref="https://docs.vernemq.com/plugin-development/webhookplugins#configuring-webhooks"/>
		/// </summary>
		/// <param name="request">hook and endpoint information</param>
		/// <returns></returns>
		Task<bool> Register(RegisterRequest request);
		/// <summary>
		/// Shows the information of the registered webhooks.
		/// For more information <see cref="https://docs.vernemq.com/plugin-development/webhookplugins#configuring-webhooks"/>
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<WebhookInfo>> Show();
	}
}