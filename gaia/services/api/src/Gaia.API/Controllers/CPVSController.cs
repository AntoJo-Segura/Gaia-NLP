// <copyright file="CPVSController.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using Gaia.Application.Dtos;
using Gaia.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Gaia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CPVSController : ControllerBase
    {
        private readonly ICPVAppService _cpvAppService;

        public CPVSController(ICPVAppService cpvAppService)
        {
            _cpvAppService = cpvAppService ?? throw new System.ArgumentNullException(nameof(cpvAppService));
        }

        [HttpPost]
        public async Task<IActionResult> ImportAsync(IEnumerable<CPVDto> cpvs)
        {
            await _cpvAppService.ImportAsync(cpvs);

            return Ok();
        }
    }
}
