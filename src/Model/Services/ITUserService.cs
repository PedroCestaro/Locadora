using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System;

namespace Locadora.Services
{
    public partial interface ITUserService : IEntityService<TUser>, IService
    {
        Byte[] HashPassword(String password);
        void Editar(TUser model);
        TUser FindById(Int32 id);
        TUser FindByEmail(String email);
        TUser FindByUsername(String username);
        TUser Authenticate(Login login);
    }
}