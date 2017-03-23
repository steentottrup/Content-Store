using System;
using System.Collections.Generic;

namespace ContentStore.Domain {

	public interface IField {
		String Name { get; }
		Int32 Type { get; }
		IEnumerable<IRequirement> Requirements { get; }
	}
}
