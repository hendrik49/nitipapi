using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using nitipApi.Models;

namespace nitipApi.Repositroy
{
    public interface IUserRepository
    {
        void Add(User item);

        int umur(DateTime lahir, DateTime now);
        int umur(DateTime lahir);

        IEnumerable<User> GetAll();
        User Login(User user);
        User Find(int key);
        User Find(string jwtstring);
        void Remove(int key);
        void Update(User item);
        User jwtData(HttpRequest request);
    }
}