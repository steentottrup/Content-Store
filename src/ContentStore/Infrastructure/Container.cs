using System;
using System.Collections.Generic;

namespace ContentStore.Infrastructure {

	public class Container : IContainer {
		public String Name { get; set; }
		public String[] ContentTypes { get; set; }
		public IEnumerable<IIndex> Indexes { get; set; }
		public String Database { get; set; }
	}
}
