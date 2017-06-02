using ContentStore.CQS.Commands;
using ContentStore.Infrastructure;
using CreativeMinds.CQS.Validators;
using System;
using System.Linq;

namespace ContentStore.CQS.Validators {

	public class CreateContentContainerTypeMatchValidator : IValidator<CreateContentCommand> {
		protected readonly IReadonlyContainerStore containerStore;

		public CreateContentContainerTypeMatchValidator(IReadonlyContainerStore containerStore) {
			this.containerStore = containerStore;
		}

		public ValidationResult Validate(CreateContentCommand msg) {
			ValidationResult result = new ValidationResult();
			IContainer container = this.containerStore.Get(msg.Container);
			if (!container.ContentTypes.Any(c => c == msg.Container)) {
				result.AddError($"The '{msg.Container}' container does not allow content with the type '{msg.ContentType}'", -1);
			}

			return result;
		}
	}
}
