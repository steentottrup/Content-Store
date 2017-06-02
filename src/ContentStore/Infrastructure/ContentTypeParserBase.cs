using ContentStore.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ContentStore.Infrastructure {

	public abstract class ContentTypeParserBase : IContentTypeParser {

		public abstract String Extension { get; }

		public abstract IContentType Parse(Stream stream, IReadonlyContentTypeStore contentTypeStore);

		protected virtual IContentType Merge(IContentType primary, String[] parentTypes, IReadonlyContentTypeStore contentTypeStore) {
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
			ContentType merged = null;
			// Is the secondary empty?
			if (secondary == null) {
				// Just return the primary then!
				return primary;
			}
			// Is the primary empty?
			else if (primary == null) {
				// Just return the secondary then!
				return secondary;
			}
			else {
				// Both have an instance, so, merge time!
				merged = new ContentType {
					Name = primary.Name,
					ParentTypes = primary.ParentTypes
				};

				List<IField> mergedFields = new List<IField>(primary.Fields);

				foreach (IField field in secondary.Fields) {
					// Does this field already existing in the primary template?
					if (primary.Fields.Any(f => f.Name == field.Name) == false) {
						// Nope, let's add it then!
						mergedFields.Add(field);
					}
				}

				merged.Fields = mergedFields;
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
