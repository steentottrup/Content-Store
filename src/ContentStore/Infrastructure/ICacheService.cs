using System;

namespace ContentStore.Infrastructure {

	public interface ICacheService {
		// TODO: Not class, content?!
		TEntity Get<TEntity>(String key) where TEntity : class;
		void Put<TEntity>(String key, TEntity data) where TEntity : class;
	}
}
