using ContentStore.Domain;
using System;
using System.IO;

namespace ContentStore.Infrastructure {

	public interface IContentTypeParser {
		IContentType Parse(Stream stream, IContentTypeStore contentTypeStore);
		String Extension { get; }
	}
}
