using System.Collections.Generic;
using System.Linq;
using nitipApi.Models;
using nitipApi.DataAccess;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using Jwt;
using System;

namespace nitipApi.Repositroy
{
    public class UserRepository : IUserRepository
    {
        private readonly NitipContext _context;

        public UserRepository(NitipContext context)
        {
            _context = context;
        }

        public int umur(DateTime tgllahir, DateTime now)
        {
            return now.Year - tgllahir.Year;
        }
        public int umur(DateTime tgllahir)
        {
            return DateTime.Now.Year - tgllahir.Year;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }
        public User Login(User user)
        {
            var data = _context.Users.FirstOrDefault(o => o.UserName == user.UserName && o.Password == user.Password);
            return data;
        }
        public User Find(string jwtstring)
        {
            if (jwtstring.Contains("id"))
            {
                JToken token = JObject.Parse(jwtstring);
                long id = (long)token.SelectToken("id");
                var data = _context.Users.Find(id);
                return data;
            }
            else
            {
                return null;
            }
        }

        public void Add(User item)
        {
            _context.Users.Add(item);
            _context.SaveChanges();
        }

        public User Find(int key)
        {
            return _context.Users.FirstOrDefault(t => t.Id == key);
        }

        public void Remove(int key)
        {
            var entity = _context.Users.First(t => t.Id == key);
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(User item)
        {
            _context.Users.Update(item);
            _context.SaveChanges();
        }
        public User jwtData(HttpRequest request)
        {

            var secret = "didok49";
            string token = string.Empty;
            var result = new Dictionary<string, object>();

            if (!request.Headers.ContainsKey("Authorization"))
            {
                return null;
            }
            else
            {
                token = request.Headers["Authorization"].FirstOrDefault();
                try
                {
                    var data = JsonWebToken.Decode(token, secret);
                    JToken tokenData = JObject.Parse(data);
                    int id = (int)tokenData.SelectToken("id");
                    var user = _context.Users.Find(id);
                    return user;
                }
                catch (SignatureVerificationException ex)
                {
                    return null;
                }
            }
        }

    }
}