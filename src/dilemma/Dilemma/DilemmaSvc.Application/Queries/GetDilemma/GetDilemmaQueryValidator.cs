using System;
using FluentValidation;

namespace DilemmaSvc.Application.Queries.GetDilemma.DTOs
{
    public class GetDilemmaQueryValidator: AbstractValidator<GetDilemmaQuery>
    {
        public GetDilemmaQueryValidator()
        {
            RuleFor(x => x.DilemmaId).NotEqual(Guid.Empty);
        }
    }
}