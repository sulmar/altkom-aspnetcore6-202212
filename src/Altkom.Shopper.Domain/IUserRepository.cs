using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shopper.Domain;

public interface IUserRepository : IEntityRepository<User>
{
    User GetByUsername(string username);
}
