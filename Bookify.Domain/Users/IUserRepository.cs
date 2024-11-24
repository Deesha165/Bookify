using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Users
{
    public interface IUserRepository
    {

        Task<User?>GetUserById(Guid id,CancellationToken cancellationToken=default);
        void AddUser(User user);
    }
}
