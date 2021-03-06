﻿using ContentStore.Domain;
using System;
using System.IO;

namespace ContentStore.Infrastructure {

	public interface IContainerParser {
		IContainer Parse(Stream stream, IReadonlyContainerStore containerStore);
		String Extension { get; }
	}
}
