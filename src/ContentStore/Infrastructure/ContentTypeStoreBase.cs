using ContentStore.Domain;
using System;

namespace ContentStore.Infrastructure {

	public abstract class ContentTypeStoreBase : IContentTypeStore {
		protected readonly ICacheService cache;
		protected readonly IContentTypeParser parser;

		protected ContentTypeStoreBase(ICacheService cacheServicce, IContentTypeParser parser) {
			this.cache = cacheServicce;
			this.parser = parser;
		}

		protected virtual IContentType GetFromCache(String name) {
			return this.cache.Get<IContentType>(name);
		}

		public abstract Boolean Exists(String name);
		public abstract IContentType Get(String name);
	}
}
