// <copyright file="IOperationAppService.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using System.Threading.Tasks;
using Gaia.Application.Dtos;

namespace Gaia.Application.Services.Contracts
{
    public interface IOperationAppService
    {
        Task<OperationDto> InsertNewOperationAsync(InsertOperationDto operationDto);

        Task<OperationDto> GetOperationByIdAsync(Guid operationId);
    }
}
