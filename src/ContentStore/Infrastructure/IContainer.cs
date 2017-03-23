using System;
using System.Collections.Generic;

namespace ContentStore.Infrastructure {

	public interface IContainer {
		String Name { get; }
		String[] ContentTypes { get; }
		IEnumerable<IIndex> Indexes { get; }
		String Database { get; }
	}
}
