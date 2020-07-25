// <copyright file="GaiaProfile.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using Gaia.Application.Dtos;
using Gaia.Application.Profiles.MappingActions;
using Gaia.Core.Entities;

namespace Gaia.Application.Profiles
{
    public class GaiaProfile : AutoMapper.Profile
    {
        public GaiaProfile()
        {
            CreateMap<Operation, OperationDto>()
                .ForMember(dest => dest.OperationId, opt => opt.MapFrom(src => src.Id.Id))
                .AfterMap<OperationSignedURLMappingAction>();
        }
    }
}
