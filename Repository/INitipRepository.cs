using System.Collections.Generic;
using nitipApi.Models;

namespace nitipApi.Repositroy
{
    public interface INitipRepository
    {
        void Add(NitipItem item);
        void Add(NitipItem item, User user);
        IEnumerable<NitipItem> GetAll();
        IEnumerable<NitipItem> FindByUser(int id);
        NitipItem Find(long key);
        void Remove(long key);
        void Update(NitipItem item);
    }
}