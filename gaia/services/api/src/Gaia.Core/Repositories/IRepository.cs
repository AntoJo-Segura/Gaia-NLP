// <copyright file="IRepository.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gaia.Core.Repositories
{
    public interface IRepository<T, TKey>
        where T : class
    {
        Task<T> GetAsync(TKey id);

        Task InsertOrUpdateAsync(T obj);

        Task BatchInsertAsync(IList<T> items);
    }
}
