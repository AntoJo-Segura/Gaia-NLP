// <copyright file="OperationsController.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using System.Threading.Tasks;
using Gaia.Application.Dtos;
using Gaia.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Gaia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationAppService _operationAppService;

        public OperationsController(IOperationAppService operationAppService)
        {
            _operationAppService = operationAppService ?? throw new System.ArgumentNullException(nameof(operationAppService));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationDto>> GetAsync(Guid id)
        {
            var operation = await _operationAppService.GetOperationByIdAsync(id);

            return Ok(operation);
        }

        [HttpPost]
        public async Task<ActionResult<OperationDto>> CreateAsync([FromForm]InsertOperationDto operationDto)
        {
            var operation = await _operationAppService.InsertNewOperationAsync(operationDto);

            return Ok(operation);
        }
    }
}
