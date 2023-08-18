using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NewsApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class BaseApiController : ControllerBase
	{
	}
}
