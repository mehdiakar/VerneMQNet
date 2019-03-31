using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration.Cluster
{
	public class LeaveNodeRequest
	{
		public string Node { get; set; }
		/// <summary>
		/// Terminates all open MQTT connections, and migrates the queues of
		/// the clients that used 'clean_session=false' to other cluster nodes.
		/// </summary>
		public bool KillSessions { get; set; }
		/// <summary>
		/// logs the status of an ongoing migration every <IntervalInSecs>
		/// seconds, defaults to 5 seconds.
		/// </summary>
		public int? SummeryInterval { get; set; }

		/// <summary>
		/// stops the migration process after <TimeoutInSecs> seconds, defaults
		/// to 60 seconds.The command can be reissued in case of a timeout.
		/// </summary>
		public int? Timeout { get; set; }
	}
}
