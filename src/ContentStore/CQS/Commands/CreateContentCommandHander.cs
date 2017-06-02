using ContentStore.Domain;
using ContentStore.Infrastructure;
using CreativeMinds.CQS.Commands;
using System;

namespace ContentStore.CQS.Commands {

	public class CreateContentCommandHander : ICommandHandler<CreateContentCommand> {
		protected readonly IReadonlyContentTypeStore contentTypes;

		public CreateContentCommandHander(IReadonlyContentTypeStore contentTypes) {
			this.contentTypes = contentTypes;
		}

		public void Execute(CreateContentCommand command) {
			IContentType contentType = this.contentTypes.Get(command.ContentType);





		}
	}
}
