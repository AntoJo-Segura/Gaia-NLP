// <copyright file="ICPVAppService.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using Gaia.Application.Dtos;

namespace Gaia.Application.Services.Contracts
{
    public interface ICPVAppService
    {
        Task ImportAsync(IEnumerable<CPVDto> cpvs);
    }
}
