// <copyright file="InsertOperationDto.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using Microsoft.AspNetCore.Http;

namespace Gaia.Application.Dtos
{
    public class InsertOperationDto
    {
        public IFormFile Document { get; set; }
    }
}
