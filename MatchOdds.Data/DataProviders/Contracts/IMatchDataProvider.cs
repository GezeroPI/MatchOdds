using MatchOdds.Data.Db.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatchOdds.Data.DataProviders.Contracts
{
    public interface IMatchDataProvider: IRepository<Match>
    {
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Match GetByID(long id);
        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        /// <returns></returns>
        Match Update(Match entityToUpdate);
    }
}
