using ContentStore.Domain;
using System;
using System.IO;

namespace ContentStore.Infrastructure {

	public abstract class ContainerParserBase : IContainerParser {
		public abstract String Extension { get; }

		public abstract IContainer Parse(Stream stream, IReadonlyContainerStore containerStore);
	}
}
