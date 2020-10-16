using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System;

namespace Locadora.Services
{
    public partial interface ITClientService : IEntityService<TClient>, IService
    {
        Byte[] HashPassword(String password);
        void Edit(TClient model);
        TClient FindById(Int32 id);
        TClient FindByEmail(String email);
        TClient FindByUsername(String username);
        TClient Authenticate(Login login);
    }
}