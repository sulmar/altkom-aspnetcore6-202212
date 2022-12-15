using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altkom.AuthService.Api.Domain;
using Altkom.Shopper.Domain;
using Microsoft.AspNetCore.Identity;

namespace Altkom.AuthService.Api.Infrastructure;

public class AuthService : IAuthService
{
    private readonly IUserRepository repository;
    private readonly IPasswordHasher<User> passwordHasher;

    public AuthService(IUserRepository repository, IPasswordHasher<User> passwordHasher)
    {
        this.repository = repository;
        this.passwordHasher = passwordHasher;
    }

    public bool TryAuthorize(string username, string password, out User user)
    {
        user = repository.GetByUsername(username);

        var result = passwordHasher.VerifyHashedPassword(user, user.HashedPassword, password);

        return user != null &&  result == PasswordVerificationResult.Success;
    }
}
