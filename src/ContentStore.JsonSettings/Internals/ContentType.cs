using ContentStore.Domain;
using System;

namespace ContentStore.JsonSettings.Internals {

	public class ContentType {
		public String Name { get; set; }
		public String[] ParentTypes { get; set; }
		public Field[] Fields { get; set; }
	}
}
