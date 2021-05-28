using System;
using System.Collections.Generic;
using DilemmaApp.Services.Common.Application.RequestPipeline;
using MediatR;

namespace DilemmaApp.Services.Dilemma.Application.Commands.PostDilemma
{
    public class PostDilemmaCommand : IRequest<Response<PostDilemmaCommandResult>>
    {
        public Guid TopicId { get; set; }
        public Guid PosterId { get; set; }
        public string Question { get; set; }
        public List<Option> Options { get; set; }

        public class Option
        {
            public string Description { get; set; }
        }
    }
}