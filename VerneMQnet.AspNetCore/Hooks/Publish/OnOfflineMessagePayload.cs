﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks.Publish
{
	public class OnOfflineMessagePayload
	{
		public string Mountpoint { get; set; }
		public string Client_id { get; set; }
		public string Qos { get; set; }
		public string Topic { get; set; }
		public string Payload { get; set; }
		public bool Retain { get; set; }
	}
}
