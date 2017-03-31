using System.Collections.Generic;
using System.Linq;
using nitipApi.Models;
using nitipApi.DataAccess;

namespace nitipApi.Repositroy
{
    public class UserRepository : IUserRepository
    {
        private readonly NitipContext _context;

        public UserRepository(NitipContext context)
        {
            _context = context;

            if( _context.Users.Count() == 0)
                Add(new User { Name = "Item1" });
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }
        public User Login(User user)
        {
            return _context.Users.Where(o=>o.Name==user.Name && o.Password==user.Password).FirstOrDefault();
        }

        public void Add(User item)
        {
            _context.Users.Add(item);
            _context.SaveChanges();
        }

        public User Find(long key)
        {
            return _context.Users.FirstOrDefault(t => t.Id == key);
        }

        public void Remove(long key)
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
    }
}