using MatchOdds.Data.DataProviders.Contracts;
using MatchOdds.Data.Db;
using MatchOdds.Data.Db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatchOdds.Data.DataProviders
{
    /// <summary>
    /// Match data provider
    /// </summary>
    /// <seealso cref="MatchOdds.Data.DataProviders.BaseDataProvider{MatchOdds.Data.Db.Models.Match}" />
    /// <seealso cref="MatchOdds.Data.DataProviders.Contracts.IMatchDataProvider" />
    public class MatchDataProvider : BaseDataProvider<Match>, IMatchDataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatchDataProvider"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public MatchDataProvider(MatchContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets the match by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Match object</returns>
        public Match GetByID(long id)
        {
            return _db.AsNoTracking().Include(x=>x.MatchOdds).SingleOrDefault(x => x.ID == id);
        }

        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        /// <returns>Match object</returns>
        /// <exception cref="DbUpdateException">Cannot apply action due to database restrictions.</exception>
        public Match Update(Match entityToUpdate)
        {
            try
            {
                //Check in db if the entity exist
                if(!_db.Any(x=>x.ID==entityToUpdate.ID))
                    throw new InvalidOperationException($"Match not found for update");

                _context.Attach(entityToUpdate);
                _context.Entry(entityToUpdate).State = EntityState.Modified;
                //if children exists attach them
                if (entityToUpdate.MatchOdds.Count != 0)
                {
                    foreach (var odd in entityToUpdate.MatchOdds)
                    {
                        _context.Attach(odd);
                        _context.Entry(odd).State = EntityState.Modified;
                    }
                }

                _context.SaveChanges();
                _context.Entry(entityToUpdate).State = EntityState.Detached;

                return entityToUpdate;
            }
            catch (Exception e)
            {
                throw new DbUpdateException(e.Message, e);
            }
        }

    }
}
