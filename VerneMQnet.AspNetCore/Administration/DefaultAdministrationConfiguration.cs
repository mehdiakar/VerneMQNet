using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration
{
	public class DefaultAdministrationConfiguration : IAdministrationConfiguration
	{
		private string apiKey;
		private string baseAddress;
		private int? port;
		public DefaultAdministrationConfiguration(string apiKey, string baseAddress, int? port)
		{
			this.apiKey = apiKey;
			this.baseAddress = baseAddress;
			this.port = port;
		}
		public string ApiKey => apiKey;

		public string BaseAddress => baseAddress;

		public int? Port => port;
	}
}
