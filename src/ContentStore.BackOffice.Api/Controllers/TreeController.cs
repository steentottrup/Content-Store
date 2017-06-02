using Microsoft.AspNetCore.Mvc;
using System;

namespace ContentStore.BackOffice.Api.Controllers {

	[Route("/api/backoffice/tree")]
	public class TreeController : Controller {

		[HttpGet]
		public JsonResult GetAll() {
			return Json(new Dtos.Tree[] { new Dtos.Tree { Id = "site" }, new Dtos.Tree { Id = "file" }, new Dtos.Tree { Id = "user" } });
		}

		[HttpGet("{tree}")]
		public JsonResult GetRoot(String tree) {
			return Json(new Dtos.TreeNode[] { new Dtos.TreeNode { Id = "geesggesg", Name = "First" }, new Dtos.TreeNode { Id = "hehegse", Name = "Second" }, new Dtos.TreeNode { Id = "hehreefweghe", Name = "Third" } });
		}

		[HttpGet("{tree}/{id}")]
		public void GetNodes(String tree, String id) {

		}
	}
}
