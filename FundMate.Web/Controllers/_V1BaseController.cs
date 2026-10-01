using Microsoft.AspNetCore.Mvc;

namespace FundMate.Web.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public abstract class _V1BaseController : ControllerBase
{
    public _V1BaseController() : base() { }
}
