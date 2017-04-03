using System.Collections.Generic;
using nitipApi.Models;

namespace nitipApi.Repositroy
{
    public interface IUserRepository
    {
        void Add(User item);
        IEnumerable<User> GetAll();
        User Login(User user);
        User Find(long key);
        User Find(string jwtstring);
        void Remove(long key);
        void Update(User item);
    }
}