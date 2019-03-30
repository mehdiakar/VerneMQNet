using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VerneMQNet.AspNetCore.Hooks.Session
{
	[ApiController]
	[Route("vernemq/session")]
	public abstract class SessionController:ControllerBase
	{
		[Route("AuthOnRegister")]
		[HttpPost]
		public async Task<IActionResult> AuthOnRegister([FromBody] AuthOnRegisterPayload request)
		{
			var result = await AuthenticationOnRegister(request);

			if (result != null && result is OkAuthOnRegisterResult authResult)
				return Ok(new
				{
					Result = "ok",
					Modifiers = new
					{
						Max_message_size = authResult.MaxMessageSize,
						Max_inflight_messages = authResult.MaxInflightMessages,
						Retry_interval = authResult.RetryInterval
					}
				});

			return result.GeneratePublicResult();
		}

		[Route("OnClientWakeUp")]
		[HttpPost]
		public async Task<IActionResult> OnClientWakeUp([FromBody] OnClientWakeUpPayload request)
		{
			await ClientWakeUp(request);
			return Ok();
		}

		[Route("OnRegister")]
		[HttpPost]
		public async Task<IActionResult> OnRegister([FromBody] OnRegisterPayload request)
		{
			await Register(request);
			return Ok();
		}

		[Route("OnClientOffline")]
		[HttpPost]
		public async Task<IActionResult> OnClientOffline([FromBody] OnClientOfflinePayload request)
		{
			await ClientOffline(request);
			return Ok();
		}

		[Route("OnClientGone")]
		[HttpPost]
		public async Task<IActionResult> OnClientGone([FromBody] OnClientGonePayload request)
		{
			await ClientGone(request);
			return Ok();
		}

		[Route("OnAuthM5")]
		[HttpPost]
		public async Task<IActionResult> OnAuthM5([FromBody] OnAuthM5Payload request)
		{
			var result = await AuthenticationM5(request);
			if(result != null && result is OkAuthM5Result item)
			{
				return Ok(new
				{
					Result = "ok",
					Modifiers = new
					{
						item.Properties,
						Reason_code = item.ReasonCode
					}
				});
			}

			return result.GeneratePublicResult();
		}

		[Route("AuthOnRegisterM5")]
		[HttpPost]
		public async Task<IActionResult> AuthOnRegisterM5([FromBody] AuthOnRegisterM5Payload request)
		{
			var result = await AuthenticationOnRegisterM5(request);

			if (result != null && result is OkAuthOnRegisterResult authResult)
				return Ok(new
				{
					Result = "ok",
					Modifiers = new
					{
						Max_message_size = authResult.MaxMessageSize,
						Max_inflight_messages = authResult.MaxInflightMessages,
						Retry_interval = authResult.RetryInterval
					}
				});

			return result.GeneratePublicResult();
		}

		[Route("OnRegisterM5")]
		[HttpPost]
		public async Task<IActionResult> OnRegisterM5([FromBody] OnRegisterM5Payload request)
		{
			await OnRegisterM5(request);
			return Ok();
		}

		/// <summary>
		/// This method is called when new client is trying to authentication to server. 
		/// To prevent the client to register return <see cref="ErrorResult"/>
		/// To ignore authentication for the client return <see cref="NextResult"/>
		/// To accept the client as a valid client return complex <see cref="OkAuthOnRegisterResult"/> to control client specific settings or simple <see cref="OkResult"/>.
		/// If you return another HookResult its meaning client is valid and caller ignore your result.
		/// For more information <see cref="https://docs.vernemq.com/plugin-development/sessionlifecycle#auth_on_register-and-auth_on_register_m5"/>
		/// </summary>
		/// <param name="authOnRegisterPayload">new client information</param>
		/// <returns>return value must be one of <see cref="OkResult"/> or <see cref="OkAuthOnRegisterResult"/> or <see cref="NextResult"/> or <see cref="ErrorResult"/> </returns>
		protected virtual async Task<HookResult> AuthenticationOnRegister(AuthOnRegisterPayload authOnRegisterPayload) { return new OkResult(); }

		/// <summary>
		/// This method is called if authenticated client has used clean_session=false or had previous sessions in the cluster and when offline messages migrated and potential duplicate sessions have been disconnected.
		/// For more information <see cref="https://docs.vernemq.com/plugin-development/sessionlifecycle#on_client_wakeup"/>
		/// </summary>
		/// <param name="clientWakeUpPayload">waked up client information</param>
		/// <returns></returns>
		protected virtual async Task ClientWakeUp(OnClientWakeUpPayload clientWakeUpPayload) { return; }

		/// <summary>
		/// This event is called when new client has been authenticated successfully.
		/// For more information <see cref="https://docs.vernemq.com/plugin-development/sessionlifecycle#on_register-and-on_register_m5"/>
		/// </summary>
		/// <param name="registerPayload">on register client information</param>
		/// <returns></returns>
		protected virtual async Task Register(OnRegisterPayload registerPayload) { return; }

		/// <summary>
		/// This event is called if a client using clean_session=false closes the connection or gets disconnected by a duplicate client.
		/// /// For more information <see cref="https://docs.vernemq.com/plugin-development/sessionlifecycle#on_client_offline"/>
		/// </summary>
		/// <param name="clientOfflinePayload">client information</param>
		/// <returns></returns>
		protected virtual async Task ClientOffline(OnClientOfflinePayload clientOfflinePayload) { return; }

		/// <summary>
		/// This event is called if a client using clean_session=true closes the connection or gets disconnected by a duplicate client.
		/// /// For more information <see cref="https://docs.vernemq.com/plugin-development/sessionlifecycle#on_client_gone"/>
		/// </summary>
		/// <param name="clientGonePayload"></param>
		/// <returns></returns>
		protected virtual async Task ClientGone(OnClientGonePayload clientGonePayload) { return; }
		/// <summary>
		///  This method is called when new client is trying to authentication to server using MQTT v5. 
		/// To prevent the client to register return <see cref="ErrorResult"/>
		/// To ignore authentication for the client return <see cref="NextResult"/>
		/// To accept the client as a valid client return complex <see cref="OkAuthOnRegisterResult"/> to control client specific settings or simple <see cref="OkResult"/>.
		/// If you return another HookResult its meaning client is valid and caller ignore your result.
		/// For more information <see cref="https://docs.vernemq.com/plugin-development/sessionlifecycle#auth_on_register-and-auth_on_register_m5"/>
		/// </summary>
		/// <param name="authOnRegisterPayload">new client information</param>
		/// <returns>return value must be one of <see cref="OkResult"/> or <see cref="OkAuthOnRegisterResult"/> or <see cref="NextResult"/> or <see cref="ErrorResult"/> </returns>

		protected virtual async Task<HookResult> AuthenticationOnRegisterM5(AuthOnRegisterM5Payload authOnRegisterPayload) { return new OkResult(); }

		/// <summary>
		/// This method is called when new client is trying to authentication to sever SASL mechanism or when client is trying to re-authentication. 
		/// To prevent the client to register return <see cref="ErrorResult"/>
		/// To ignore authentication for the client return <see cref="NextResult"/>
		/// To accept the client as a valid client return complex <see cref="OkAuthM5Result"/> to control client specific settings or simple <see cref="OkResult"/>.
		/// If you return another HookResult its meaning client is valid and caller ignore your result.
		/// For more information <see cref="https://docs.vernemq.com/plugin-development/sessionlifecycle#on_auth_m5"/>
		/// </summary>
		/// <param name="onAuthM5Payload">Authentication information include authentication data and authentication method</param>
		/// <returns>return value must be one of <see cref="OkResult"/> or <see cref="OkAuthM5Result"/> or <see cref="NextResult"/> or <see cref="ErrorResult"/> </returns>
		protected virtual async Task<HookResult> AuthenticationM5(OnAuthM5Payload onAuthM5Payload) { return new OkResult(); }

		/// <summary>
		/// This event is called when new client has been authenticated successfully using MQTT v5.
		/// For more information <see cref="https://docs.vernemq.com/plugin-development/sessionlifecycle#on_register-and-on_register_m5"/>
		/// </summary>
		/// <param name="registerPayload">on register client information</param>
		/// <returns></returns>
		protected virtual async Task RegisterM5(OnRegisterM5Payload onRegisterPayload) { return; }
	}
}
