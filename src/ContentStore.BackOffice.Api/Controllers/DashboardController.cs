using Microsoft.AspNetCore.Mvc;
using System;

namespace ContentStore.BackOffice.Api.Controllers {

	[Route("api/backoffice/dashboard")]
	public class DashboardController : Controller {

		[HttpGet()]
		public JsonResult Get() {
			return this.Json(new { Trees = new String[] { } });
		}
	}
}
