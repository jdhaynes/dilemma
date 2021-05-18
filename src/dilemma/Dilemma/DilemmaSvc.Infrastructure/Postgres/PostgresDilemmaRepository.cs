using System;
using System.Linq;
using DilemmaApp.Services.Dilemma.Application.Interfaces;

namespace DilemmaApp.Services.Dilemma.Infrastructure.Postgres
{
    public class PostgresDilemmaRepository : IDilemmaRepository
    {
        private DilemmaContext _context;
        
        public PostgresDilemmaRepository(DilemmaContext context)
        {
            _context = context;
        }
        
        public Domain.Dilemma.Model.Dilemma GetDilemma(Guid id)
        {
            return _context.Dilemmas.Single(d => d.Id == id);
        }

        public void AddDilemma(Domain.Dilemma.Model.Dilemma dilemma)
        {
            _context.Dilemmas.Add(dilemma);
            _context.SaveChanges();
        }

        public void UpdateDilemma(Domain.Dilemma.Model.Dilemma dilemma)
        {
            _context.Dilemmas.Update(dilemma);
            _context.SaveChanges();
        }

        public void DeleteDilemma(Domain.Dilemma.Model.Dilemma dilemma)
        {
            _context.Dilemmas.Remove(dilemma);
            _context.SaveChanges();
        }
    }
}