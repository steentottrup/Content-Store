using CreativeMinds.CQS.Commands;
using MongoDB.Bson;
using System;

namespace ContentStore.CQS.Commands {

	public class DeleteContentCommand : CommandWithStatus {
		public ObjectId Id { get; set; }
	}
}
