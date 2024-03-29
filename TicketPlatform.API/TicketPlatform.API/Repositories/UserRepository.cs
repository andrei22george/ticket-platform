﻿using Dapper.Contrib.Extensions;
using ErrorOr;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using TicketPlatform.API.Model;
using TicketPlatform.API.Repositories.Interfaces;
using TicketPlatform.API.ServiceErrors;
using TicketPlatform.API.Services;

namespace TicketPlatform.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly Configurations _configuration;

        public UserRepository(IOptions<Configurations> configurations)
        {
            _configuration = configurations.Value;
            _sqlConnection = new SqlConnection(_configuration.ConnectionString);
        }

        public List<User> GetAllUsers(QueryParameters parameters)
        {
            return _sqlConnection.GetAll<User>().ToList();
        }

        public ErrorOr<User> GetUserById(int id)
        {
            var user = _sqlConnection.Get<User>(id);

            return user == null ? Errors.User.NotFound : user;
        }

        public int InsertUser(User user)
        {
            return (int)_sqlConnection.Insert(user);
        }

        public bool UpsertUser(int id, User user)
        {
            user.Id = id;

            return _sqlConnection.Update(user);
        }

        public bool DeleteUser(int id)
        {
            return _sqlConnection.Delete(new User { Id = id });
        }

        public bool DeleteUsers(List<int> ids)
        {
            var usersToDelete = ids.Select(id => new User { Id = id }).ToList();

            return _sqlConnection.Delete(usersToDelete);
        }
    }
}
