using Microsoft.AspNetCore.Mvc;
using System;

namespace ContentStore.Api.Controllers {

	[Route("api/tree")]
	public class TreeController : Controller {

		[HttpGet("{tree}/{id}")]
		public string Get(String tree, String id) {
			return "value";
		}

		[HttpPost("{tree}")]
		public void Post(String tree, [FromBody]string value) {
		}

		[HttpPut("{tree}/{id}")]
		public void Put(String tree, String id, [FromBody]string value) {
		}

		[HttpPatch("{tree}/{id}")]
		public void Patch(String tree, String id, [FromBody]string value) {
		}

		[HttpDelete("{tree}/{id}")]
		public void Delete(String container, String id) {
		}
	}
}
