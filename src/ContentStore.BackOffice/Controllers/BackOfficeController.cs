﻿using Microsoft.AspNetCore.Mvc;
using System;

namespace ContentStore.BackOffice.Controllers {

	public class BackOfficeController : Controller {

		public IActionResult Index() {
			return File("~/admin/index.html", "text/html");
		}
	}
}
