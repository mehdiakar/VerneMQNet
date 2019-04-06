using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Monitoring
{
	public interface IMonitoringConfiguration
	{
		string BaseAddress { get; }
		int? Port { get; }
	}
}
