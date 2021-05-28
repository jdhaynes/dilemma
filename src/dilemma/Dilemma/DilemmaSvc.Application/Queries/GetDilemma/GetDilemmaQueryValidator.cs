using System;
using FluentValidation;

namespace DilemmaApp.Services.Dilemma.Application.Queries.GetDilemma
{
    public class GetDilemmaQueryValidator: AbstractValidator<GetDilemmaQuery>
    {
        public GetDilemmaQueryValidator()
        {
            RuleFor(x => x.DilemmaId).NotEmpty();
        }
    }
}