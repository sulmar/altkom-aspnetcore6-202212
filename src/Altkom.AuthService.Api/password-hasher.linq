<Query Kind="Statements">
  <NuGetReference>Microsoft.Extensions.Identity.Core</NuGetReference>
  <Namespace>Microsoft.AspNetCore.Identity</Namespace>
</Query>

var users = new List<User>
{
	new User {
		Id = 1,
		Username = "john",
		HashedPassword = "123",
		Email = "john@domain.com",
		DateOfBirth = DateTime.Parse("2000-12-31"),
		},

	new User {
		Id = 2,
		Username = "kate",
		HashedPassword = "321",
		Email = "kate@domain.com",
		DateOfBirth = DateTime.Parse("2010-12-31"),
		},

	new User {
		Id = 3,
		Username = "Bob",
		HashedPassword = "123",
		Email = "bob@domain.com",
		DateOfBirth = DateTime.Parse("1990-01-30"),
		},
};	

// dotnet add package Microsoft.Extensions.Identity.Core
IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();	

HashPasswords(passwordHasher, users);
users.Dump();


void HashPasswords(IPasswordHasher<User> passwordHasher, IEnumerable<User> users)
{	
	foreach (var user in users)
	{
		user.HashedPassword = passwordHasher.HashPassword(user, user.HashedPassword);
	}
}


#region Models

public abstract class Base { }

public abstract class BaseEntity : Base
{
	public int Id { get; set; }
}

public class User : BaseEntity
{
	public string Username { get; set; }
	public string HashedPassword { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	public DateTime DateOfBirth { get; set; }
}

#endregion
