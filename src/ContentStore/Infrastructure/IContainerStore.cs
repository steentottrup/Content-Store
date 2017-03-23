using ContentStore.Domain;
using System;

namespace ContentStore.Infrastructure {

	public interface IContainerStore {
		Boolean Exists(String name);
		IContainer Get(String name);
	}
}
