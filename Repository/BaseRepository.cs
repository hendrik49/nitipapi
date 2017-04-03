using nitipApi.DataAccess;

namespace nitipApi.Repositroy
{
    public class BaseRepository 
    {
        private readonly NitipContext _context;

        protected BaseRepository(NitipContext context)
        {
            _context = context;
        }

   }
}