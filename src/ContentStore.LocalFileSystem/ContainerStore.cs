using ContentStore.Domain;
using ContentStore.Infrastructure;
using System;
using System.IO;
using System.Linq;

namespace ContentStore.LocalFileSystem {

	public class ContainerStore : ContainerStoreBase {
		protected readonly String root;

		public ContainerStore(ICacheService cacheService, IContainerParser parser, String root) : base(cacheService, parser) {
			this.root = root;
		}

		protected virtual String GetTemplateRoot() {
			// TODO: Config
			String containerRoot = "containers";
			return Path.Combine(this.root, containerRoot);
		}

		private String[] GetAllTemplates() {
			return Directory.GetFiles(this.GetTemplateRoot(), $"*.{this.parser.Extension}", SearchOption.AllDirectories);
		}

		public override Boolean Exists(String name) {
			return this.GetAllTemplates().Any(f => Path.GetFileName(f).ToLowerInvariant() == $"{name}.{this.parser.Extension}");
		}

		public override IContainer Get(String name) {
			String path = this.GetAllTemplates().SingleOrDefault(f => Path.GetFileName(f).ToLowerInvariant() == $"{name}.{this.parser.Extension}");
			if (String.IsNullOrWhiteSpace(path)) {
				throw new FileNotFoundException();
			}
			return this.parser.Parse(File.OpenRead(path), this);
		}
	}
}
