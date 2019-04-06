using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Administration
{
	internal class TableBaseResponse<T> where T :class
	{
		public string Type { get; set; }
		public IEnumerable<T> Table { get; set; }
	}
}
