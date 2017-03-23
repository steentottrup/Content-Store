using ContentStore.Domain;
using System;

namespace ContentStore.JsonSettings.Internals {

	public class Container {
		public String Name { get; set; }
		public String[] ContentTypes { get; set; }
		public Index[] Indexes { get; set; }
		public String Database { get; set; }
	}
}
