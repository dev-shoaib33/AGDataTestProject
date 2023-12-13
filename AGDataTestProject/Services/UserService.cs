using AGDataTestProject.Models;
using AGDataTestProject.Interfaces;
using MongoDB.Driver;
using System;

namespace AGDataTestProject.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserService(IMongoDatabase database)
        {
            _usersCollection = database.GetCollection<User>("Users");
        }

        public List<User> GetAllUsers()
        {
            return _usersCollection.Find(_ => true).ToList();
        }

        public (bool IsSuccess, string Message) AddUser(User user)
        {
            // Validate Name uniqueness
            if (_usersCollection.Find(u => u.Name == user.Name).Any())
            {
                return (false, "User with the same name already exists.");
            }

            _usersCollection.InsertOne(user);
            return (true, "User added successfully.");
        }

        public (bool IsSuccess, string Message) UpdateUser(User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Name, user.Name);

            var updateDefinition = Builders<User>.Update.Set(u => u.Address, user.Address);
            var updateResult = _usersCollection.UpdateOne(filter, updateDefinition);

            if (updateResult.ModifiedCount > 0)
            {
                return (true, "User updated successfully.");
            }
            else
            {
                return (false, "User not found for update.");
            }
        }

        public (bool IsSuccess, string Message) DeleteUser(string name)
        {
            var result = _usersCollection.DeleteOne(u => u.Name == name);

            return (result.DeletedCount > 0, result.DeletedCount > 0 ? "User deleted successfully." : "User not found.");
        }
    }

}
