// <copyright file="RepositoryProfile.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using Gaia.Core.Entities;
using Gaia.Insfrastructure.Data.Models;

namespace Gaia.Insfrastructure.Data.Profiles
{
    public class RepositoryProfile : AutoMapper.Profile
    {
        public RepositoryProfile()
        {
            CreateMap<Operation, OperationFlat>()
                .ReverseMap();

            CreateMap<CPV, CPVFlat>()
                .ReverseMap();
        }
    }
}
