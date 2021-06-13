using System;
using System.Linq;
using DilemmaApp.IdentitySvc.Application.Interfaces;
using DilemmaApp.IdentitySvc.Domain.Models;

namespace DilemmaApp.IdentitySvc.Infrastructure.Postgres
{
    public class PostgresUserRepository : IUserRepository
    {
        private readonly IdentityContext _context;
        
        public PostgresUserRepository(IdentityContext context)
        {
            _context = context;
        }
        
        public User GetUser(Guid id)
        {
            return _context.Users.SingleOrDefault(x => x.Id == id);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}