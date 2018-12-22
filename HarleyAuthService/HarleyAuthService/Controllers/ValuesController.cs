using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HarleyAuthService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		// GET api/values
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/values
		[HttpGet("auth")]
		[Authorize]
		public ActionResult<IEnumerable<string>> GetAuthorized()
		{
			return new string[] { "value3", "value4" };
		}
	}
}
