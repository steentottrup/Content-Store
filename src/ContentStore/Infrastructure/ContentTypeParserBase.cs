using ContentStore.Domain;
using System;
using System.IO;
using System.Linq;

namespace ContentStore.Infrastructure {

	public abstract class ContentTypeParserBase : IContentTypeParser {

		public abstract String Extension { get; }

		public abstract IContentType Parse(Stream stream, IContentTypeStore contentTypeStore);

		protected virtual IContentType Merge(IContentType primary, String[] parentTypes, IContentTypeStore contentTypeStore) {
			// TODO: Stop circular reference, somehow!!!
			if (parentTypes != null && parentTypes.Any()) {
				// Let's get the least important, least important is last in the array!
				IContentType mergedParents = contentTypeStore.Get(parentTypes.Reverse().First());
				// Iterate the parent types, least first, and merge them.
				foreach (String parentType in parentTypes.Reverse().Skip(1)) {
					mergedParents = this.Merge(contentTypeStore.Get(parentType), mergedParents);
				}

				primary = this.Merge(primary, mergedParents);
			}

			return primary;
		}

		protected virtual IContentType Merge(IContentType primary, IContentType secondary) {
			IContentType merged = null;
			if (secondary == null) {
				return primary;
			} 
			else if (primary == null) {
				return secondary;
			}
			else {
				merged = primary;

				// TODO: Per field, overwrite!
			}

			return merged;
		}

		protected virtual ObjectField CreateObjectField(String name, Int32 dataType) {
			// TODO:
			return new ObjectField {
				Name = name,
				Type = dataType
			};
		}

		protected virtual SimpleField CreateSimpleField(String name, Int32 dataType) {
			// TODO:
			return new SimpleField {
				Name = name,
				Type = dataType
			};
		}

		protected virtual ArrayField CreateArrayField(String name, Int32 dataType) {
			// TODO:
			return new ArrayField {
				Name = name,
				Type = dataType
			};
		}
	}
}
