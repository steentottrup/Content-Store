using System;
using System.Collections.Generic;

namespace ContentStore.Domain {

	public interface IContentType {
		String Name { get; }
		String[] ParentTypes { get; }
		IEnumerable<IField> Fields { get; }
	}
}
