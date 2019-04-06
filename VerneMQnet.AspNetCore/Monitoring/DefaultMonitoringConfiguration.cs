using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Monitoring
{
	public class DefaultMonitoringConfiguration: IMonitoringConfiguration
	{
		private string baseAddress;
		private int? port;
		public DefaultMonitoringConfiguration(string baseAddress, int? port)
		{
			this.baseAddress = baseAddress;
			this.port = port;
		}

		public string BaseAddress => baseAddress;

		public int? Port => port;
	}
}
