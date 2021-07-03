using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VotingSvc.Application.Interfaces;
using VotingSvc.Domain.Dilemma.Model;

namespace VotingSvc.Infrastructure.Postgres
{
    public class PostgresDilemmaRepository : IDilemmaRepository
    {
        private VotingContext _context;

        public PostgresDilemmaRepository(VotingContext context)
        {
            _context = context;
        }

        public Dilemma GetDilemmaForOption(Guid optionId)
        {
            return _context
                .Options
                .Where(o => o.Id == optionId)
                .Select(o => _context
                    .Dilemmas
                    .SingleOrDefault(d => d.Id == o.DilemmaId))
                .SingleOrDefault();
        }
    }
}