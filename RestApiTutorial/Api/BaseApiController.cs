using Microsoft.AspNetCore.Mvc;

namespace RestApiTutorial.Api;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseApiController : Controller
{
}
