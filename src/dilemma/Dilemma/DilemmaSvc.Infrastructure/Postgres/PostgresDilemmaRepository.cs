using System;
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
        
        public void GetDilemma(Guid id)
        {
            throw new NotImplementedException();
        }

        public void AddDilemma(Domain.Dilemma.Model.Dilemma dilemma)
        {
            _context.Dilemmas.Add(dilemma);
            _context.SaveChanges();
        }

        public void UpdateDilemma(Domain.Dilemma.Model.Dilemma dilemma)
        {
            throw new NotImplementedException();
        }

        public void DeleteDilemma(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}