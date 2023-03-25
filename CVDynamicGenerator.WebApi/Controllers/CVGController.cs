using CVDynamicGenerator.WebApi.Business.ApplicationService;
using CVDynamicGenerator.WebApi.Entities.DTO;
using CVDynamicGenerator.WebApi.Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Dynamic;

namespace CVDynamicGenerator.WebApi.Controllers
{
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
        [HttpPost("/ms/api/v1_0/generator")]
        [ProducesResponseType(typeof(DefaultResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CVGenerator([FromBody] DefaultRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Modelo Invalido, por favor verifique los datos enviados en la estructura requerida");

                DefaultResponse response = await _GService.CVGenerator(request);

                return Ok(response);
            }
            catch (LoginException ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, ex.loginResponse);
            }
        }
    }
}
