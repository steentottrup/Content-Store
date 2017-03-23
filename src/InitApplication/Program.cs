using ContentStore.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace InitApplication {

	public class Program {

		public static void Main(string[] args) {

			// ***** START OF CONFIG

			Dictionary<String, String> connections = new Dictionary<String, String>();
			connections.Add("main", "mongodb://localhost:27017/contentstoreinit");
			String solutionConfigurationRoot = @"C:\projects\cmd\github\Content-Store\src\Backoffice\configuration";

			Dictionary<String, IMongoDatabase> databases = new Dictionary<String, IMongoDatabase>();
			foreach (KeyValuePair<String, String> connection in connections) {
				MongoUrl url = new MongoUrl(connection.Value);
				MongoClient client = new MongoClient(url);
				databases.Add(connection.Key, client.GetDatabase(url.DatabaseName));
			}

			Boolean reinit = true;

			IContainerParser parser = new ContentStore.JsonSettings.JsonContainerParser();
			IContainerStore store = new ContentStore.LocalFileSystem.ContainerStore(null, parser, solutionConfigurationRoot);

			// ***** END OF CONFIG

			String[] containerConfigurations = Directory.GetFiles(Path.Combine(solutionConfigurationRoot, "containers"), "*.json", SearchOption.AllDirectories);
			foreach (String path in containerConfigurations) {
				IContainer container = store.Get(Path.GetFileNameWithoutExtension(path));

				IMongoDatabase db = databases[String.IsNullOrWhiteSpace(container.Database) ? "main" : container.Database];

				List<BsonDocument> collections = db.ListCollections().ToList();

				Boolean createCollection = true;
				if (collections.Any(c => c["name"].AsString == container.Name) && reinit == false) {
					createCollection = false;
				}

				if (createCollection) {
					db.CreateCollection(container.Name, new CreateCollectionOptions { AutoIndexId = true });

					if (container.Indexes != null && container.Indexes.Any()) {
						List<CreateIndexModel<BsonDocument>> indexes = new List<CreateIndexModel<BsonDocument>>();

						foreach (IIndex index in container.Indexes) {
							IndexKeysDefinition<BsonDocument> idx = null;
							CreateIndexOptions options = null;
							if (index.Fields != null && index.Fields.Any()) {
								if (index.Order == Order.Ascending) {
									idx = Builders<BsonDocument>.IndexKeys.Ascending(index.Fields.First());
									foreach (String field in index.Fields.Skip(1)) {
										idx = idx.Ascending(field);
									}
								}
								else {
									idx = Builders<BsonDocument>.IndexKeys.Descending(index.Fields.First());
									foreach (String field in index.Fields.Skip(1)) {
										idx = idx.Descending(field);
									}
								}
								if (index.Unique.HasValue && index.Unique.Value == true) {
									options = new CreateIndexOptions { Unique = true };
								}
							}
							else if (!String.IsNullOrWhiteSpace(index.Field)) {
								if (index.Order == Order.Ascending) {
									idx = Builders<BsonDocument>.IndexKeys.Ascending(index.Field);
								}
								else {
									idx = Builders<BsonDocument>.IndexKeys.Descending(index.Field);
								}
								if (index.Unique.HasValue && index.Unique.Value == true) {
									options = new CreateIndexOptions { Unique = true };
								}
							}
							else {
								// TODO:
								throw new Exception("");
							}

							indexes.Add(new CreateIndexModel<BsonDocument>(idx, options));
						}

						IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(container.Name);
						collection.Indexes.CreateMany(indexes);
					}
				}
			}

			String[] treeConfigurations = Directory.GetFiles(Path.Combine(solutionConfigurationRoot, "trees"), "*.json", SearchOption.AllDirectories);
			foreach (String path in containerConfigurations) {

			}


		}
	}
}
