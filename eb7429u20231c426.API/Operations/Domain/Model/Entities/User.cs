using eb7429u20231c426.API.Assets.Domain.Model.ValueObject;
using eb7429u20231c426.API.Operations.Domain.Model.Commands;

namespace eb7429u20231c426.API.Operations.Domain.Model.Entities;

public class User
{
   public int Id { get; set; }
   public string FirstName { get; set; }
   public string LastName { get; set; }
   public string Email { get; set; }
   public EStore StoreId { get; set; }

   public User()
   {
      FirstName = string.Empty;
      LastName = string.Empty;
      Email = string.Empty;
      StoreId = default;
   }
   
   public User(string firstName, string lastName, string email, EStore storeId)
   {
      FirstName = firstName;
      LastName = lastName;
      Email = email;
      StoreId = storeId;
   }
   
   
   public User(CreateUserCommand command)
   {
      FirstName = command.FirstName;
      LastName = command.LastName;
      Email = command.Email;
      StoreId = command.StoreId;
   }
}