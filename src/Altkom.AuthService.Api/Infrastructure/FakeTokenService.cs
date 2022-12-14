using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altkom.AuthService.Api.Domain;
using Altkom.Shopper.Domain;

namespace Altkom.AuthService.Api.Infrastructure
{
    public class FakeTokenService : ITokenService
    {
        public string Create(User user)
        {
            return $"token-for-{user.Username}";
        }
    }
}