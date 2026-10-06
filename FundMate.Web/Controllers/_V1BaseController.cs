using FundMate.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FundMate.Web.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public abstract class _V1BaseController : ControllerBase
{
    public _V1BaseController() : base() { }

    /// <summary>
    /// Processes a SimpleResponseDto and returns the appropriate HTTP response.
    /// </summary>
    /// <param name="simpleResponseDto"></param>
    /// <returns>An ActionResult containing the processed SimpleResponseDto.</returns>
    [NonAction]
    public ActionResult<SimpleResponseDto> ProcessSimpleResponseDto(SimpleResponseDto simpleResponseDto)
    {
        if (!simpleResponseDto.Success)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            if (simpleResponseDto.StatusCode != default)
                statusCode = simpleResponseDto.StatusCode;

            return StatusCode((int)statusCode, simpleResponseDto);
        }

        return Ok(simpleResponseDto);
    }
}
