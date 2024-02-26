using CVDynamicGenerator.WebApi.Business.ApplicationService;
using CVDynamicGenerator.WebApi.Entities.DTO;
using CVDynamicGenerator.WebApi.Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CVDynamicGenerator.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CVGController : ControllerBase
{
    private readonly IGService _GService;

    /// <summary>
    /// Método para inicializar interfaces
    /// </summary>
    public CVGController(IGService GService)
    {
        _GService = GService;
    }

    // Autenticacion
    [HttpPost("/ms/api/v1_0/esGenerator", Name = "Spanish")]
    [HttpPost("/ms/api/v1_0/enGenerator", Name = "English")]
    [ProducesResponseType(typeof(DefaultResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CVGenerator([FromBody] DefaultRequest request)
    {
        try
        {
            string endpoint = ControllerContext.ActionDescriptor.AttributeRouteInfo.Name;

            Console.WriteLine(JsonConvert.SerializeObject(request));

            if (!ModelState.IsValid)
                return BadRequest("Modelo Invalido, por favor verifique los datos enviados en la estructura requerida");

            DefaultResponse response;
            if (endpoint == "English")
            {
                response = await _GService.CVEnGenerator(request);
            }
            else
            {
                response = await _GService.CVEsGenerator(request);
            }

            Console.WriteLine(JsonConvert.SerializeObject(response));

            return Ok(response);
        }
        catch (LoginException ex)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, ex.loginResponse);
        }
    }
}
