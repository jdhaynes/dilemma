using System;
using DilemmaApp.IdentitySvc.Domain.Models;

namespace DilemmaApp.IdentitySvc.Application.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(Guid id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}