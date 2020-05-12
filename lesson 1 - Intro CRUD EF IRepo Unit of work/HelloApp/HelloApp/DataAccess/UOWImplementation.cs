using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HelloApp.DataAccess
{
    class UOWImplementation : IDisposable
    {
        private readonly AppContext _context;

        private UserRepo _userRepo;
        private bool isDispose;

        public UserRepo UserRepo {
            get {

                if (_userRepo == null)
                {
                    _userRepo = new UserRepo(_context);
                }
                
                return _userRepo;
            }
        }

        public UOWImplementation(DbContextOptions<AppContext> options)
        {
            _context = new AppContext(options);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await DisposeAsync(true);

            GC.SuppressFinalize(this);
        }

        public async Task DisposeAsync(bool disposing)
        {
            if (!isDispose && disposing)
            {
                isDispose = true;
                await _context.DisposeAsync();
            }
        }
    }
}
