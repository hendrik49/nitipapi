using System.Collections.Generic;
using System.Linq;
using nitipApi.Models;
using nitipApi.DataAccess;

namespace nitipApi.Repositroy
{
    public class ProductRepository : IProductRepository
    {
        private readonly NitipContext _context;

        public ProductRepository(NitipContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public void Add(Product item)
        {
            _context.Products.Add(item);
            _context.SaveChanges();
        }

        public Product Find(long key)
        {
            return _context.Products.FirstOrDefault(t => t.Id == key);
        }

        public void Remove(long key)
        {
            var entity = _context.Products.First(t => t.Id == key);
            _context.Products.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Product item)
        {
            _context.Products.Update(item);
            _context.SaveChanges();
        }
    }
}