using Microsoft.AspNetCore.Mvc;
using System;

namespace ContentStore.Api.Controllers {

	[Route("api/content")]
	public class ContentController : Controller {

		[HttpGet("{container}/{id}")]
		public string Get(String container, String id) {
			return "value";
		}

		[HttpPost]
		public void Post(String container, [FromBody]string value) {
		}

		[HttpPut("{id}")]
		public void Put(String container, String id, [FromBody]string value) {
		}

		[HttpDelete("{id}")]
		public void Delete(String container, String id) {
		}
	}
}
