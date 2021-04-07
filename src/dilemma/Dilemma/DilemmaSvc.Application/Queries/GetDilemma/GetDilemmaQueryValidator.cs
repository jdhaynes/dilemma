using System;
using FluentValidation;

namespace DilemmaSvc.Application.Queries.GetDilemma
{
    public class GetDilemmaQueryValidator: AbstractValidator<GetDilemmaQuery>
    {
        public GetDilemmaQueryValidator()
        {
            RuleFor(x => x.DilemmaId).NotEqual(Guid.Empty);
        }
    }
}