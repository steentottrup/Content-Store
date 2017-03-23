using ContentStore.Infrastructure;
using System;

namespace ContentStore.JsonSettings.Internals {

	public class Index {
		public String Field { get; set; }
		public String[] Fields { get; set; }
		public Boolean? Unique { get; set; }
		public Order Order { get; set; }
	}
}
