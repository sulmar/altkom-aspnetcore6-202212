using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altkom.Shopper.Domain;

namespace Altkom.AuthService.Api.Domain;

public interface ITokenService
{
    string Create(User user);
}
