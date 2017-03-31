using System.Collections.Generic;
using nitipApi.Models;

namespace nitipApi.Repositroy
{
    public interface IProductRepository
    {
        void Add(Product product);
        IEnumerable<Product> GetAll();
        Product Find(long key);
        void Remove(long key);
        void Update(Product product);
    }
}