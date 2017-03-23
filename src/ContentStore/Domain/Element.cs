using MongoDB.Bson;
using System;

namespace ContentStore.Domain {

	public class Element : IElement {
		public ObjectId Id { get; set; }
		public ObjectId ParentId { get; set; }
		public String Name { get; set; }
		public String Tree { get; set; }
		public String Template { get; set; }
		//public IProperty[] Properties { get; set; }
	}
}
