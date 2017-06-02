using ContentStore.Domain;
using ContentStore.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ContentStore.JsonSettings {

	public class JsonContainerParser : ContainerParserBase {

		public override String Extension {
			get {
				return "json";
			}
		}

		public override IContainer Parse(Stream stream, IReadonlyContainerStore containerStore) {
			TextReader reader = new StreamReader(stream, Encoding.UTF8);
			Internals.Container tmp = JsonConvert.DeserializeObject<Internals.Container>(reader.ReadToEnd());

			IContainer container = new Container {
				Name = tmp.Name,
				Database = tmp.Database,
				ContentTypes = tmp.ContentTypes,
				Indexes = tmp.Indexes != null ? tmp.Indexes.Select(i => new Index { Field = i.Field, Fields = i.Fields, Order = i.Order, Unique = i.Unique }) : new List<Index>()
			};

			return container;
		}
	}
}
