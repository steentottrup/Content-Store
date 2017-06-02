using ContentStore.Domain;
using System;

namespace ContentStore.Infrastructure {

	public interface IReadonlyContentTypeStore {
		Boolean Exists(String name);
		IContentType Get(String name);
	}
}
