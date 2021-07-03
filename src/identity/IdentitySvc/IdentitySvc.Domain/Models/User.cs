using System;
using DilemmaApp.IdentitySvc.Domain.Events;
using DilemmaApp.Services.Common.Domain;

namespace DilemmaApp.IdentitySvc.Domain.Models
{
    public class User : Entity
    {
        public Guid Id { get; private set; }
        public Name Name { get; private set; }
        public string Email { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Password Password { get; private set; }
        public DateTime AccountRegistered { get; private set; }
        public DateTime? AccountClosed { get; private set; }

        public bool IsClosed => AccountClosed != null;

        private User()
        {
            // Instantiate object through factory methods.
        }

        public static User Register(Guid id, string firstName, string lastName, string email,
            DateTime dob, byte[] passwordHash, byte[] passwordSalt)
        {
            User user = new User()
            {
                Id = id,
                Name = new Name(firstName, lastName),
                Email = email,
                DateOfBirth = dob,
                Password = new Password(passwordHash, passwordSalt),
                AccountRegistered = DateTime.Now
            };
            
            user.RaiseDomainEvent(new UserRegisteredDomainEvent(id));

            return user;
        }
        
        
        public void ChangePassword(byte[] newPasswordHash, byte[] newPasswordSalt)
        {
            Password = new Password(newPasswordHash, newPasswordSalt);
        }

        public void CloseAccount()
        {
            if (IsClosed)
            {
                throw new DomainRuleException("ACCOUNT_ALREADY_CLOSED",
                    "User account has already been closed");
            }

            AccountClosed = DateTime.Now;
        }
    }
}