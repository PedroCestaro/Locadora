using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Locadora.Domain;
using System.Security.Cryptography;
using NHibernate.Criterion;

namespace Locadora.Services
{
    public partial class TUserService : EntityService<TUser>, ITUserService
    {

        public byte[] HashPassword(string password)
        {
            return SHA384.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public void Editar(TUser model)
        {
            var user = TUser.Load(model.Id);
            user.Name = model.Name;
            user.EnumProfileUser = model.EnumProfileUser;
            user.Update();
        }

        public TUser FindById(int id)
        {
            return Session.CreateCriteria<TUser>()
                .Add(Restrictions.Eq("Id", id))
                .UniqueResult<TUser>();
        }

        public TUser FindByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            return Session.CreateCriteria<TUser>()
                .Add(Restrictions.Eq("Email", email))
                .SetMaxResults(1)
                .UniqueResult<TUser>();
        }

        public TUser FindByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            return Session.CreateCriteria<TUser>()
                .Add(Restrictions.Eq("Login", username))
                .SetMaxResults(1)
                .UniqueResult<TUser>();
        }

        public TUser Authenticate(Login login)
        {
            if (string.IsNullOrWhiteSpace(login.User) || string.IsNullOrWhiteSpace(login.Password))
                return null;


            var criteria = Session.CreateCriteria<TUser>()
                .Add(Restrictions.Eq("Login", login.User));


            var user = criteria.SetMaxResults(1).UniqueResult<TUser>();
            if (user == null || user.Password == null)
                return null;

            var userPassword = user.Password;
            var hashTryPassword = HashPassword(login.Password);
            return userPassword.SequenceEqual(hashTryPassword) ? user : null;
        }

    }
}