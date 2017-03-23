using System;
using System.Collections.Generic;

namespace ContentStore.Domain {

	public class ContentType : IContentType {
		public String Name { get; set; }
		public String[] ParentTypes { get; set; }
		public IEnumerable<IField> Fields { get; set; }
	}
}
