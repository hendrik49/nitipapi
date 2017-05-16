using System.Collections.Generic;
using nitipApi.Models;

namespace nitipApi.Repositroy
{
    public interface IUserRepository
    {
        void Add(User item);
        IEnumerable<User> GetAll();
        User Login(User user);
        User Find(int key);
        User Find(string jwtstring);
        void Remove(int key);
        void Update(User item);
    }
}