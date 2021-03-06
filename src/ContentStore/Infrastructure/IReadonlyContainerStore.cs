﻿using ContentStore.Domain;
using System;

namespace ContentStore.Infrastructure {

	public interface IReadonlyContainerStore {
		Boolean Exists(String name);
		IContainer Get(String name);
	}
}
