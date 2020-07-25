// <copyright file="CPVAppService.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dawn;
using Gaia.Application.Dtos;
using Gaia.Application.Services.Contracts;
using Gaia.Core.Entities;
using Gaia.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace Gaia.Application.Services
{
    public class CPVAppService : ICPVAppService
    {
        private readonly ILogger<CPVAppService> _logger;
        private readonly ICPVRepository _cpvRepository;

        public CPVAppService(
                ILogger<CPVAppService> logger,
                ICPVRepository cpvRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cpvRepository = cpvRepository ?? throw new ArgumentNullException(nameof(cpvRepository));
        }

        public async Task ImportAsync(IEnumerable<CPVDto> cpvs)
        {
            try
            {
                Guard.Argument(cpvs, nameof(cpvs)).NotNull().NotEmpty();

                _logger.LogInformation($"Importing {cpvs.Count()} cpvs");

                var entities = new List<CPV>();

                foreach (var item in cpvs)
                {
                    var cpv = new CPV(item.Code, item.Type);

                    cpv.AddDescription(item.Description);

                    entities.Add(cpv);
                }

                await _cpvRepository.BatchInsertAsync(entities);

                _logger.LogInformation($"CPV's imported succesfuly.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error importing cpvs");
                throw new Exception($"Error importing cpvs. Please contact with support team.");
            }
        }
    }
}
