using CreativeMinds.CQS.Commands;
using System;

namespace ContentStore.CQS.Commands {

	public class CreateContentCommand : CommandWithStatus {
		public String ContentType { get; set; }
		public String Container { get; set; }
	}
}
