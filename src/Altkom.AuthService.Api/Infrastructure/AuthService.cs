using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altkom.AuthService.Api.Domain;
using Altkom.Shopper.Domain;

namespace Altkom.AuthService.Api.Infrastructure;

public class AuthService : IAuthService
{
    private readonly IUserRepository repository;

    public AuthService(IUserRepository repository)
    {
        this.repository = repository;
    }

    public bool TryAuthorize(string username, string password, out User user)
    {
        user = repository.GetByUsername(username);

        // TODO: hash password
        return user != null && user.HashedPassword == password;
    }
}
