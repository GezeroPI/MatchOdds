using System;
using System.Collections.Generic;
using System.Text;

namespace MatchOdds.Data.DataProviders.Contracts
{
    public interface IRepository<TEntity> where TEntity : class

    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();
        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Insert(ref TEntity entity);
        /// <summary>
        /// Deletes the entity by id.
        /// </summary>
        /// <param name="id">The id.</param>
        void Delete(long id);
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        void Delete(TEntity entityToDelete);
    }
}
