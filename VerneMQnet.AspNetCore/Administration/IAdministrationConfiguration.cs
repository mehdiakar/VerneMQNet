using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration
{
	public interface IAdministrationConfiguration
	{
		string ApiKey { get;}
		string BaseAddress { get; }
		int? Port { get; }
	}
}
