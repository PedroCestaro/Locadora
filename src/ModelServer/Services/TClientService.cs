using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Locadora.Domain;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using FluentNHibernate.Automapping;
using NHibernate.Criterion;

namespace Locadora.Services
{
    public partial class TClientService : EntityService<TClient>, ITClientService
    {
        public byte[] HashPassword(string password)
        {
            return SHA384.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public void Edit(TClient model)
        {
            var client = TClient.Load(model.Id);
            client.Name = model.Name;
            client.Email = model.Email;
            client.Telephone = model.Telephone;
            client.Login = model.Login;
            client.TPreferences.ToList();
            client.Update();
        }

        public TClient FindById(int id)
        {
            return Session.CreateCriteria<TClient>()
                .Add(Restrictions.Eq("Id", id))
                .UniqueResult<TClient>();
        }

        public TClient FindByEmail(string email)
        {
            return Session.CreateCriteria<TClient>()
                .Add(Restrictions.Eq("Email", email))
                .Add(Restrictions.Eq("IsCanceled", false))
                .SetMaxResults(1)
                .UniqueResult<TClient>();
        }

        public TClient FindByUsername(string username)
        {
            return Session.CreateCriteria<TClient>()
                .Add(Restrictions.Eq("Login", username))
                .SetMaxResults(1)
                .UniqueResult<TClient>();
        }

        public TClient Authenticate(Login login)
        {
            if (string.IsNullOrWhiteSpace(login.User) || string.IsNullOrWhiteSpace(login.Password))
                return null;


            var criteria = Session.CreateCriteria<TClient>()
                .Add(Restrictions.Eq("Login", login.User));
                

            var user = criteria.SetMaxResults(1).UniqueResult<TClient>();
            if (user == null || user.Password == null)
                return null;

            var userPassword = user.Password;
            var hashTryPassword = HashPassword(login.Password);
            return userPassword.SequenceEqual(hashTryPassword)? user : null;
        }
    }
}