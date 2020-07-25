// <copyright file="InsertOperationDtoValidator.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using FluentValidation;
using Gaia.Application.Dtos;

namespace Gaia.Application.Validators
{
    public class InsertOperationDtoValidator : AbstractValidator<InsertOperationDto>
    {
        public InsertOperationDtoValidator()
        {
            RuleFor(p => p.Document).NotNull();
        }
    }
}
