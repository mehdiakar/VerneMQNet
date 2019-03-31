using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VerneMQNet.AspNetCore.Hooks.Publish
{
	[ApiController]
	[Route("vernemq/publish")]
	public abstract class PublishController:ControllerBase
	{
		[Route("AuthOnPublish")]
		[HttpPost]
		public async Task<IActionResult> AuthOnPublish([FromBody] AuthOnPublishPayload request)
		{
			var result = await AuthorizeOnPublish(request);

			if (result != null && result is OkAuthOnPublishResult item)
				return Ok(new
				{
					Result = "ok",
					Modifiers = new
					{
						item.Topic,
						item.Qos,
						item.Payload,
						item.Retain
					}
				});

			return result.GeneratePublicResult();
		}

		[Route("OnPublish")]
		[HttpPost]
		public async Task<IActionResult> OnPublish([FromBody] OnPublishPayload request)
		{
			await Publish(request);
			return Ok();
		}

		[Route("OnOfflineMessage")]
		[HttpPost]
		public async Task<IActionResult> OnOfflineMessage([FromBody] OnOfflineMessagePayload request)
		{
			await OfflineMessage(request);
			return Ok();
		}

		[Route("OnDeliver")]
		[HttpPost]
		public async Task<IActionResult> OnDeliver([FromBody] OnDeliverPayload request)
		{
			var result = await Deliver(request);
			if (result != null && result is OkOnDeliverResult item)
				return Ok(new
				{
					Result = "ok",
					Modifiers = new
					{
						item.Topic,
						item.Payload,
					}
				});

			return result.GenerateOnDeliverResult();
		}

		[Route("AuthOnPublishM5")]
		[HttpPost]
		public async Task<IActionResult> AuthOnPublishM5([FromBody] AuthOnPublishM5Payload request)
		{
			var result = await AuthorizeOnPublishM5(request);

			if (result != null && result is OkAuthOnPublishM5Result item)
				return Ok(new
				{
					Result = "ok",
					Modifiers = new
					{
						item.Topic
					}
				});

			return result.GeneratePublicResult();
		}

		[Route("OnPublishM5")]
		[HttpPost]
		public async Task<IActionResult> OnPublishM5([FromBody] OnPublishM5Payload request)
		{
			await PublishM5(request);
			return Ok();
		}

		[Route("OnDeliverM5")]
		[HttpPost]
		public async Task<IActionResult> OnDeliverM5([FromBody] OnDeliverM5Payload request)
		{
			var result = await DeliverM5(request);
			return result.GenerateOnDeliverResult();
		}

		/// <summary>
		/// This method is called when client is trying to publish a message to specific topic. You can authorize client and accept or reject this request.
		/// To accept the client to publish the message return complex <see cref="OkAuthOnPublishResult"/> to rewrite the topic and the payload or simple <see cref="OkResult"/> 
		/// To ignore authorization return <see cref="NextResult"/>
		/// To reject the client return <see cref="ErrorResult"/>
		/// For more information <see cref="https://docs.vernemq.com/plugin-development/publishflow#auth_on_publish-and-auth_on_publish_m5"/>
		/// </summary>
		/// <param name="authOnRegisterPayload">publish message information</param>
		/// <returns>return value must be one of <see cref="OkResult"/> or <see cref="OkAuthOnPublishResult"/> or <see cref="NextResult"/> or <see cref="ErrorResult"/> </returns>
		protected virtual async Task<HookResult> AuthorizeOnPublish(AuthOnPublishPayload authOnRegisterPayload) { return new OkResult(); }

		/// <summary>
		/// This event is called when a new message has published successfully. 
		/// For more information <see cref="https://docs.vernemq.com/plugin-development/publishflow#on_publish-and-on_publish_m5"/>
		/// </summary>
		/// <param name="authOnRegisterPayload">published message information</param>
		/// <returns></returns>
		protected virtual async Task Publish(OnPublishPayload authOnRegisterPayload) { return; }

		/// <summary>
		/// This method get notified a new a queued message for a client that is currently offline.
		/// </summary>
		/// <param name="authOnRegisterPayload">queued message information</param>
		/// <returns></returns>
		protected virtual async Task OfflineMessage(OnOfflineMessagePayload authOnRegisterPayload) { return; }

		/// <summary>
		/// This method is called when a message devilered to a subscriber. You can rewrite topic and payload in result.
		/// To rewrite topic and payload return <see cref="OkOnDeliverResult"/>
		/// To ignore delivered message return <see cref="NextResult"/>
		/// For more information <see cref="https://docs.vernemq.com/plugin-development/publishflow#on_deliver-and-on_deliver_m5"/>
		/// </summary>
		/// <param name="authOnRegisterPayload">delivered message information</param>
		/// <returns>Return value musb be one of <see cref="OkResult"/> or <see cref="OkOnDeliverResult"/> or <see cref="NextResult"/></returns>
		protected virtual async Task<HookResult> Deliver(OnDeliverPayload authOnRegisterPayload) { return new OkResult(); }

		/// <summary>
		/// This method is called when client is trying to publish a message to specific topic in MQTT v5. You can authorize client and accept or reject this request.
		/// To accept the client to publish the message return complex <see cref="OkAuthOnPublishM5Result"/> to rewrite the topic and the payload or simple <see cref="OkResult"/> 
		/// To ignore authorization return <see cref="NextResult"/>
		/// To reject the client return <see cref="ErrorResult"/>
		/// For more information <see cref="https://docs.vernemq.com/plugin-development/publishflow#auth_on_publish-and-auth_on_publish_m5"/>
		/// </summary>
		/// <param name="authOnRegisterPayload">publish message information</param>
		/// <returns>return value must be one of <see cref="OkResult"/> or <see cref="OkAuthOnPublishM5Result"/> or <see cref="NextResult"/> or <see cref="ErrorResult"/> </returns>

		protected virtual async Task<HookResult> AuthorizeOnPublishM5(AuthOnPublishM5Payload authOnRegisterPayload) { return new OkResult(); }
		/// <summary>
		/// This event is called when a new message has published successfully using MQTT v5. 
		/// For more information <see cref="https://docs.vernemq.com/plugin-development/publishflow#on_publish-and-on_publish_m5"/>
		/// </summary>
		/// <param name="authOnRegisterPayload">published message information</param>
		/// <returns></returns>
		protected virtual async Task PublishM5(OnPublishM5Payload authOnRegisterPayload) { return; }

		/// <summary>
		/// This method is called when a message devilered to a subscriber using MQTT v5. You can rewrite topic and payload in result.
		/// To rewrite topic and payload return <see cref="OkOnDeliverResult"/>
		/// To ignore delivered message return <see cref="NextResult"/>
		/// For more information <see cref="https://docs.vernemq.com/plugin-development/publishflow#on_deliver-and-on_deliver_m5"/>
		/// </summary>
		/// <param name="authOnRegisterPayload">delivered message information</param>
		/// <returns>Return value musb be one of <see cref="OkResult"/> or <see cref="OkOnDeliverResult"/> or <see cref="NextResult"/></returns>
		protected virtual async Task<HookResult> DeliverM5(OnDeliverM5Payload authOnRegisterPayload) { return new OkResult(); }
	}
}
