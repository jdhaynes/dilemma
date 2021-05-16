using DilemmaApp.Services.Dilemma.Application.Queries.GetDilemma;
using DilemmaApp.Services.Dilemma.Application.Queries.GetDilemma.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DilemmaSvc.WebApi.Controllers
{
    [ApiController]
    public class DilemmaController : Controller
    {
        private IMediator _mediator;
        
        public DilemmaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Dilemma GetDilemma(GetDilemmaQuery query)
        {
            var result = new GetDilemmaQueryHandler().Handle(query);
        }
    }
}