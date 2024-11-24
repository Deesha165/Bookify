using Bookify.Domain.Abstractions;
using Bookify.Domain.Users.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Users
{
    public  sealed class User:Entity
    {
        private User (Guid id,FirstName firstName,SecondName secondName,Email email) :base(id)
        {
            FirstName = firstName;
            SecondName = secondName;
            Email = email;  
        }

        public FirstName FirstName { get;private set; }

        public SecondName SecondName { get; private set; }
        public Email Email { get; private set; }
      
        public static User Create(FirstName firstName,SecondName secondName,Email email)
        {
            var user=new User(new Guid(),firstName,secondName,email);

            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
            return user;    
        }

    }
}
