using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VerneMQNet.AspNetCore.Hooks.Subscribe
{
	[ApiController]
	[Route("vernemq/subscribe")]
	public abstract class SubscribeController : ControllerBase
	{
		[Route("AuthOnSubscribe")]
		[HttpPost]
		public async Task<IActionResult> AuthOnSubscribe([FromBody] AuthOnSubscribePayload request)
		{
			var result = await AuthorizeOnSubscribe(request);
			if (result != null && result is OkAuthOnSubscribeResult authResult)
			{
				return Ok(new
				{
					Result = "ok",
					authResult.Topics
				});
			}

			return result.GeneratePublicResult();
		}

		[Route("OnSubscribe")]
		[HttpPost]
		public async Task<IActionResult> OnSubscribe([FromBody] OnSubscribePayload request)
		{
			await Subscribe(request);
			return Ok();
		}

		[Route("OnUnsubscribe")]
		[HttpPost]
		public async Task<IActionResult> OnUnsubscribe([FromBody] OnUnsubscribePayload request)
		{
			var result = await Unsubscribe(request);
			if (result != null && result is OkOnUnsubscribeResult authResult)
				return Ok(new
				{
					Result = "ok",
					authResult.Topics
				});

			return result.GeneratePublicResult();
		}

		[Route("AuthOnSubscribeM5")]
		[HttpPost]
		public async Task<IActionResult> AuthOnSubscribeM5([FromBody] AuthOnSubscribeM5Payload request)
		{
			var result = await AuthorizeOnSubscribeM5(request);
			if (result != null && result is OkAuthOnSubscribeM5Result authResult)
			{
				return Ok(new
				{
					Result = "ok",
					Modifiers = new { authResult.Topics }
				});
			}

			return result.GeneratePublicResult();
		}

		[Route("OnSubscribeM5")]
		[HttpPost]
		public async Task<IActionResult> OnSubscribeM5([FromBody] OnSubscribeM5Payload request)
		{
			await SubscribeM5(request);
			return Ok();
		}

		[Route("OnUnsubscribeM5")]
		[HttpPost]
		public async Task<IActionResult> OnUnsubscribeM5([FromBody] OnUnsubscribeM5Payload request)
		{
			var result = await UnsubscribeM5(request);
			if (result != null && result is OkOnUnsubscribeM5Result authResult)
			{
				return Ok(new
				{
					Result = "ok",
					Modifiers = new { authResult.Topics }
				});
			}

			return result.GeneratePublicResult();
		}

		/// <summary>
		/// This method is called when client is trying to subscribe specific topics. You can accept or reject client to subscribe a topic or ignore process the request.
		/// To accept the client to subscribe the topic return complex <see cref="OkAuthOnSubscribeResult"/> to rewrite the subscribe topic and qos or simple <see cref="OkResult"/> 
		/// To ignore authorization return <see cref="NextResult"/>
		/// To reject the client return <see cref="ErrorResult"/>
		/// For more information <see cref="https://docs.vernemq.com/plugin-development/subscribeflow#auth_on_subscribe-and-auth_on_subscribe_m5"/>
		/// </summary>
		/// <param name="authOnRegisterPayload">subscribe topic information</param>
		/// <returns>return value must be one of <see cref="OkResult"/> or <see cref="OkAuthOnSubscribeResult"/> or <see cref="NextResult"/> or <see cref="ErrorResult"/> </returns>
		protected virtual async Task<HookResult> AuthorizeOnSubscribe(AuthOnSubscribePayload authOnRegisterPayload) { return new OkResult(); }

		/// <summary>
		/// This event is called when authorized client subscribe a specific topic.
		/// For more information <see cref="https://docs.vernemq.com/plugin-development/subscribeflow#on_subscribe-and-on_subscribe_m5"/>
		/// </summary>
		/// <param name="authOnRegisterPayload">subsciption information</param>
		/// <returns></returns>
		protected virtual async Task Subscribe(OnSubscribePayload onSubscribePayload) { return; }

		/// <summary>
		/// This method is called when client is trying to unsubscribe specific topics. You can accept or reject the client to unsubscription.
		/// To accept client request return complex <see cref="OkOnUnsubscribeResult"/> to rewrite topics or simple <see cref="OkResult"/>.
		/// To ingore client request return <see cref="NextResult"/>
		/// To reject client request return <see cref="ErrorResult"/>
		/// For more information <see cref="https://docs.vernemq.com/plugin-development/subscribeflow#on_unsubscribe-and-on_unsubscribe_m5"/>
		/// </summary>
		/// <param name="authOnRegisterPayload"></param>
		/// <returns>return value must be one of <see cref="OkResult"/> or <see cref="OkOnUnsubscribeResult"/> or <see cref="NextResult"/> or <see cref="ErrorResult"/> </returns>
		protected virtual async Task<HookResult> Unsubscribe(OnUnsubscribePayload onUnsubscribePayload) { return new OkResult(); }

		protected virtual async Task<HookResult> AuthorizeOnSubscribeM5(AuthOnSubscribeM5Payload authOnRegisterPayload) { return new OkResult(); }
		protected virtual async Task SubscribeM5(OnSubscribeM5Payload authOnRegisterPayload) { return; }
		protected virtual async Task<HookResult> UnsubscribeM5(OnUnsubscribeM5Payload onUnsubscribeM5Payload) { return new OkResult(); }
	}
}
