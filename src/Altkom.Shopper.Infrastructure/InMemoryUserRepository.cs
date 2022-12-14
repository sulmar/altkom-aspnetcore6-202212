using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altkom.Shopper.Domain;

namespace Altkom.Shopper.Infrastructure
{
    public class InMemoryUserRepository : InMemoryEntityRepository<User>, IUserRepository
    {
        public InMemoryUserRepository(IEnumerable<User> entities) : base(entities)
        {
        }

        public User GetByUsername(string username)
        {
            return entities.Values.SingleOrDefault(e=>e.Username == username);
        }
    }
}