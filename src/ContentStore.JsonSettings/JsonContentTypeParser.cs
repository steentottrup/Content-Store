using ContentStore.Domain;
using ContentStore.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ContentStore.JsonSettings {

	public class JsonContentTypeParser : ContentTypeParserBase {

		public override String Extension {
			get {
				return "json";
			}
		}

		public override IContentType Parse(Stream stream, IReadonlyContentTypeStore templateStore) {
			TextReader reader = new StreamReader(stream, Encoding.UTF8);
			Internals.ContentType tmp = JsonConvert.DeserializeObject<Internals.ContentType>(reader.ReadToEnd());

			List<IField> fields = new List<IField>();
			foreach (Internals.Field field in tmp.Fields) {
				DataType dataType = DataType.Array;
				if (Enum.TryParse<DataType>(field.DataType, true, out dataType)) {
					IField newField = null;
					if (dataType.IsSimpleDataType()) {
						newField = this.CreateSimpleField(field.Name, (Int32)dataType);
					}
					else if (dataType == DataType.Array) {
						newField = this.CreateArrayField(field.Name, (Int32)dataType);
					}
					else if (dataType == DataType.Object) {
						newField = this.CreateObjectField(field.Name, (Int32)dataType);
					}
					fields.Add(newField);
				}
				else {
					throw new Exception($"Unknown DataType {field.DataType}");
				}
			}

			IContentType template = new ContentType {
				Name = tmp.Name,
				ParentTypes = tmp.ParentTypes,
				Fields = fields
			};

			return this.Merge(template, tmp.ParentTypes, templateStore);
		}
	}
}
