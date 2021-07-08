using System;
using DilemmaApp.Services.Common.Application;
using DilemmaApp.Services.Dilemma.Application.Commands.PostDilemma;
using DilemmaApp.Services.Dilemma.Application.Commands.WithdrawDilemma;
using DilemmaApp.Services.Dilemma.Application.Queries.GetDilemma;
using DilemmaApp.Services.Dilemma.Application.Queries.GetDilemma.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DilemmaSvc.WebApi.Controllers
{
    [ApiController]
    public class DilemmaController : Controller
    {
        private IMediator _mediator;
        private ILogger _logger;
        
        public DilemmaController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("dilemmas/{dilemmaId}")]
        public Response<Dilemma> GetDilemma(Guid dilemmaId)
        {
            _logger.Information("Request to get dilemma {@dilemmaId}", dilemmaId);
            return _mediator.Send(new GetDilemmaQuery(dilemmaId)).Result;
        }
        
        [HttpPost]
        [Route("dilemmas")]
        public Response<PostDilemmaCommandResult> PostDilemma(PostDilemmaCommand request)
        {
            return _mediator.Send(request).Result;
        }

        [HttpPut]
        [Route("dilemmas/{dilemmaId}/withdraw")]
        public Response WithdrawDilemma(Guid dilemmaId)
        {
            return _mediator.Send(new WithdrawDilemmaCommand(dilemmaId)).Result;
        }
    }
}