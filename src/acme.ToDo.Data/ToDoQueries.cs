using acme.ToDo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace acme.ToDo.Data
{
    public class ToDoQueries : IToDoQueries
    {
        public ToDoQueries(ToDoDbContextFactory toDoDbContextFactory)
        {
            _contextFactory = toDoDbContextFactory;
        }

        private readonly ToDoDbContextFactory _contextFactory;

        public async Task<List<ToDoItem>> GetIncompleteItems(
            Guid userId,
            CancellationToken cancellationToken = default(CancellationToken)
            )
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var _db = _contextFactory.CreateContext())
            {
                var query = _db.Items
                    .Where(x =>
                        x.UserId == userId
                        && x.IsComplete == false
                        )
                        .OrderBy(x => x.CreatedUtc)
                        ;

                return await query.AsNoTracking().ToListAsync<ToDoItem>(cancellationToken).ConfigureAwait(false);

            }

        }

        public async Task<ToDoItem> Fetch(
            Guid userId,
            Guid itemId,
            CancellationToken cancellationToken = default(CancellationToken)
            )
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var _db = _contextFactory.CreateContext())
            {
                return await _db.Items.AsNoTracking().SingleOrDefaultAsync(p => p.Id == itemId && p.UserId == userId).ConfigureAwait(false);
            }
        }

    }
}
