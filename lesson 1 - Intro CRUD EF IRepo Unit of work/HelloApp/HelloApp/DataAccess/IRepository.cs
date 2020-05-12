using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloApp.DataAccess
{
    public interface IRepository<T> where T : class
    {
        public Task CreateAsync(T item);

        public Task UpdateAsync(T item);

        public Task<T> GetAsync(int id);

        public Task DeleteAsync(int id);
    }
}
