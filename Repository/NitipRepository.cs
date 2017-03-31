using System.Collections.Generic;
using System.Linq;
using nitipApi.Models;
using nitipApi.DataAccess;

namespace nitipApi.Repositroy
{
    public class NitipRepository : INitipRepository
    {
        private readonly NitipContext _context;

        public NitipRepository(NitipContext context)
        {
            _context = context;

            if( _context.NitipItems.Count() == 0)
                Add(new NitipItem { Name = "Item1" });
        }

        public IEnumerable<NitipItem> GetAll()
        {
            return _context.NitipItems.ToList();
        }
        public IEnumerable<NitipItem> FindByUser(int userid)
        {
            return _context.NitipItems.Where(o=>o.IdUser==userid).ToList();
        }

        public void Add(NitipItem item)
        {
            _context.NitipItems.Add(item);
            _context.SaveChanges();
        }

        public NitipItem Find(long key)
        {
            return _context.NitipItems.FirstOrDefault(t => t.Key == key);
        }

        public void Remove(long key)
        {
            var entity = _context.NitipItems.First(t => t.Key == key);
            _context.NitipItems.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(NitipItem item)
        {
            _context.NitipItems.Update(item);
            _context.SaveChanges();
        }
    }
}