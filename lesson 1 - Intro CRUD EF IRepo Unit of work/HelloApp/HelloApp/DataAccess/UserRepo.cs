using System;
using System.Linq;
using System.Threading.Tasks;

namespace HelloApp.DataAccess
{
    class UserRepo : IRepository<User>
    {
        private readonly AppContext _userContext;

        public UserRepo(AppContext context)
        {
            _userContext = context;
        }

        public async Task CreateAsync(User item)
        {
            if (item != null)
            {
                await _userContext.AddAsync(item);

                //await _userContext.SaveChangesAsync();
            }
            else
            throw new NullReferenceException("user cannot de null");
        }

        public async Task DeleteAsync(int id)
        {
            var user = _userContext.Users.FirstOrDefault(item => item.Id == id);

            if (user != null)
            {
                _userContext.Remove(user);
                //await _userContext.SaveChangesAsync();
            }
else
            throw new InvalidOperationException("user id is not found");
        }

        public async Task<User> GetAsync(int id)
        {
            return await _userContext.FindAsync<User>(id);
        }

        public async Task UpdateAsync(User item)
        {
            var user = await _userContext.FindAsync<User>(item.Id);

            if (user != null)
            {
                user.Name = item.Name;

                user.Age = item.Age;

                _userContext.Update(user);
                
                //await _userContext.SaveChangesAsync();
            }
        }
    }
}
