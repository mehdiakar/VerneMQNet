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

		protected virtual async Task<HookResult> AuthorizeOnPublish(AuthOnPublishPayload authOnRegisterPayload) { return new OkResult(); }
		protected virtual async Task Publish(OnPublishPayload authOnRegisterPayload) { return; }
		protected virtual async Task OfflineMessage(OnOfflineMessagePayload authOnRegisterPayload) { return; }
		protected virtual async Task<HookResult> Deliver(OnDeliverPayload authOnRegisterPayload) { return new OkResult(); }
		protected virtual async Task<HookResult> AuthorizeOnPublishM5(AuthOnPublishM5Payload authOnRegisterPayload) { return new OkResult(); }
		protected virtual async Task PublishM5(OnPublishM5Payload authOnRegisterPayload) { return; }
		protected virtual async Task<HookResult> DeliverM5(OnDeliverM5Payload authOnRegisterPayload) { return new OkResult(); }
	}
}
