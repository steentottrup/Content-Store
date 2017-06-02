using ContentStore.Domain;
using System;

namespace ContentStore.Infrastructure {

	public abstract class ContainerStoreBase : IReadonlyContainerStore {
		protected readonly ICacheService cache;
		protected readonly IContainerParser parser;

		protected ContainerStoreBase(ICacheService cacheServicce, IContainerParser parser) {
			this.cache = cacheServicce;
			this.parser = parser;
		}

		protected virtual IContentType GetFromCache(String name) {
			return this.cache.Get<IContentType>(name);
		}

		public abstract Boolean Exists(String name);
		public abstract IContainer Get(String name);
	}
}
