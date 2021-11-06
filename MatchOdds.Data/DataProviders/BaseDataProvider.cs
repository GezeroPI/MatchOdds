using MatchOdds.Data.DataProviders.Contracts;
using MatchOdds.Data.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatchOdds.Data.DataProviders
{
    public abstract class BaseDataProvider<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly MatchContext _context;
        internal DbSet<TEntity> _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDataProvider{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected BaseDataProvider(MatchContext context)
        {
            _context = context;
            _db = context.Set<TEntity>();
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return _db.AsNoTracking().ToList();
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.ArgumentNullException">entity</exception>
        /// <exception cref="DbUpdateException">Cannot apply action due to database restrictions.</exception>
        public virtual void Insert(ref TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                _db.Add(entity);

                _context.SaveChanges();

            }
            catch (Exception e)
            {
                throw new DbUpdateException("Cannot apply action due to database restrictions.", e);
            }
        }

        /// <summary>
        /// Deletes the entity by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <exception cref="System.InvalidOperationException">Entity with id {id} does not exist</exception>
        /// <exception cref="DbUpdateException"></exception>
        public virtual void Delete(long id)
        {
            try
            {
                TEntity entityToDelete = _db.Find(id);
                if (entityToDelete == null)
                    throw new InvalidOperationException($"Entity with id {id} does not exist");
                Delete(entityToDelete);
            }
            catch (Exception e)
            {
                throw new DbUpdateException(e.Message,e);
            }
            
        }
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        /// <exception cref="DbUpdateException">Cannot apply action due to database restrictions.</exception>
        public virtual void Delete(TEntity entityToDelete)
        {
            try
            {
                if (_context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    _db.Attach(entityToDelete);
                }
                _db.Remove(entityToDelete);


                _context.SaveChanges();

            }
            catch (Exception e)
            {
                throw new DbUpdateException("Cannot apply action due to database restrictions.", e);
            }
        }

    }
}
