using System;
using System.Collections.Generic;
using DilemmaApp.Services.Dilemma.Application.Queries.GetDilemma;
using DilemmaApp.Services.Dilemma.Application.Queries.GetDilemma.DTOs;
using DilemmaApp.Services.Dilemma.Application.Queries.GetTopics;
using DilemmaApp.Services.Dilemma.Application.Queries.GetTopics.DTOs;
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
        public Dilemma GetDilemma(Guid dilemmaId)
        {
            return _mediator.Send(new GetDilemmaQuery(dilemmaId)).Result;
        }

        [HttpGet]
        [Route("topics")]
        public ICollection<Topic> GetTopics()
        {
            return _mediator.Send(new GetTopicsQuery()).Result;
        }
    }
}