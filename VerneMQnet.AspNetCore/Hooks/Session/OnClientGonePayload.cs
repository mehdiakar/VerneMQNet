﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks.Session
{
	public class OnClientGonePayload
	{
		public string Mountpoint { get; set; }
		public string Client_id { get; set; }
	}
}
