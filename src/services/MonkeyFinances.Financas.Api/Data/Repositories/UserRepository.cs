using MonkeyFinances.Core.Data;
using MonkeyFinances.Financas.Api.Models.Entities;

namespace MonkeyFinances.Financas.Api.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        void Adicionar(User user);
    }
    public class UserRepository : IUserRepository
    {
        private readonly FinancasContext _context;

        public UserRepository(FinancasContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(User user)
        {
            _context.Users.Add(user);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                _context.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
