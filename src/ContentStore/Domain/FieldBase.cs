using System;
using System.Collections.Generic;

namespace ContentStore.Domain {

	public abstract class FieldBase {
		public virtual String Name { get; set; }
		public virtual Int32 Type { get; set; }
		public virtual IEnumerable<IRequirement> Requirements { get; set; }
	}
}
