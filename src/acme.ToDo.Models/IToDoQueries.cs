using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace acme.ToDo.Models
{
    public interface IToDoQueries
    {
        Task<List<ToDoItem>> GetIncompleteItems(
            Guid userId,
            CancellationToken cancellationToken = default(CancellationToken)
            );

        Task<ToDoItem> Fetch(
            Guid userId,
            Guid itemId,
            CancellationToken cancellationToken = default(CancellationToken)
            );

    }
}
