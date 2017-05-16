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
        }

        public IEnumerable<NitipItem> GetAll()
        {
            return _context.NitipItems.ToList();
        }
        public IEnumerable<NitipItem> FindByUser(int userid)
        {
            return _context.NitipItems.Where(o => o.IdUser == userid).ToList();
        }


        public void Add(NitipItem item, User user)
        {   
            item.IdUser= (int)user.Id;
            item.Amount = this.Amount(item);
            _context.NitipItems.Add(item);
            _context.SaveChanges();
        }
        public void Add(NitipItem item)
        {   
            _context.NitipItems.Add(item);
            _context.SaveChanges();
        }
        public decimal Amount(NitipItem item)
        {
            var product = _context.Products.Find(item.IdProduct);
            if (product != null)
            {
                var amount = product.Price * item.Qty;
                return amount;
            }
            else
            {
                return 0;

            }
        }

        public NitipItem Find(int key)
        {
            return _context.NitipItems.FirstOrDefault(t => t.Id == key);
        }

        public void Remove(int key)
        {
            var entity = _context.NitipItems.First(t => t.Id == key);
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