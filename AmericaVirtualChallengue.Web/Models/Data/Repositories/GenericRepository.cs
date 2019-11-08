namespace AmericaVirtualChallengue.Web.Models.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly DataContext context;

        public GenericRepository(DataContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return this.context.Set<T>().AsNoTracking();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(int id)
        {
            return await this.context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> CreateAsync(T entity)
        {
            await this.context.Set<T>().AddAsync(entity);
            await SaveAllAsync();
            return entity;
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> UpdateAsync(T entity)
        {
            this.context.Set<T>().Update(entity);
            await SaveAllAsync();
            return entity;
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(T entity)
        {
            this.context.Set<T>().Remove(entity);
            await SaveAllAsync();
        }

        /// <summary>
        /// ExistAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ExistAsync(int id)
        {
            return await this.context.Set<T>().AnyAsync(e => e.Id == id);

        }

        /// <summary>
        /// SaveAllAsync
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }
    }

}
