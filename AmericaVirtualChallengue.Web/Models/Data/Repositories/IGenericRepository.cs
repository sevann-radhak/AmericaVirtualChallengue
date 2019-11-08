namespace AmericaVirtualChallengue.Web.Models.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity);

        /// <summary>
        /// ExistAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistAsync(int id);
    }

}
