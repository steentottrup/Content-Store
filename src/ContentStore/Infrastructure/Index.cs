using System;

namespace ContentStore.Infrastructure {

	public class Index : IIndex {
		public String Field { get; set; }
		public String[] Fields { get; set; }
		public Boolean? Unique { get; set; }
		public Order Order { get; set; }
	}
}
