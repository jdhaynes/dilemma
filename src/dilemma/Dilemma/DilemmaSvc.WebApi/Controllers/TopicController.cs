using System.Collections.Generic;
using DilemmaApp.Services.Common.Application;
using DilemmaApp.Services.Dilemma.Application.Queries.GetTopics;
using DilemmaApp.Services.Dilemma.Application.Queries.GetTopics.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DilemmaSvc.WebApi.Controllers
{
    [Controller]
    public class TopicController : Controller
    {
        private IMediator _mediator;
        
        public TopicController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [Route("topics")]
        public Response<ICollection<Topic>> GetTopics()
        {
            return _mediator.Send(new GetTopicsQuery()).Result;
        }
    }
}