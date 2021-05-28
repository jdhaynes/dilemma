using System;
using DilemmaApp.Services.Common.Application;
using DilemmaApp.Services.Common.Application.RequestPipeline;
using DilemmaApp.Services.Dilemma.Application.Commands.PostDilemma;
using DilemmaApp.Services.Dilemma.Application.Commands.WithdrawDilemma;
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
        [Route("dilemmas/{dilemmaId}")]
        public Response<Dilemma> GetDilemma(Guid dilemmaId)
        {
            return _mediator.Send(new GetDilemmaQuery(dilemmaId)).Result;
        }
        
        [HttpPost]
        [Route("dilemmas")]
        public PostDilemmaCommandResult PostDilemma(PostDilemmaCommand request)
        {
            return _mediator.Send(request).Result;
        }

        [HttpPut]
        [Route("dilemmas/{dilemmaId}/withdraw")]
        public ActionResult WithdrawDilemma(Guid dilemmaId)
        {
            _mediator.Send(new WithdrawDilemmaCommand(dilemmaId));

            return Ok();
        }
    }
}