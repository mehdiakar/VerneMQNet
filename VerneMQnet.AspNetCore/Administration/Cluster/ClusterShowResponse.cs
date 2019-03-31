using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Cluster
{
	public class ClusterShowResponse
	{
		public string Type { get; set; }
		public IEnumerable<NodeInfo> Table { get; set; }
	}
}
