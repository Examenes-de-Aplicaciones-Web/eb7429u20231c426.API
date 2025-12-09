using eb7429u20231c426.API.Operations.Domain.Model.Entities;
using eb7429u20231c426.API.Operations.Domain.Model.Queries;

namespace eb7429u20231c426.API.Operations.Domain.Services;

public interface IUserQueryService
{
    Task<User?> Handle(GetUserByIdQuery query);
}