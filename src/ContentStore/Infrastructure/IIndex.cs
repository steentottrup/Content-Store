using System;

namespace ContentStore.Infrastructure {

	public interface IIndex {
		String Field { get; }
		String[] Fields { get; }
		Boolean? Unique { get; }
		Order Order { get; }
	}
}
