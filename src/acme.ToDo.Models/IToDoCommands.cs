using System;
using System.Threading;
using System.Threading.Tasks;

namespace acme.ToDo.Models
{
    public interface IToDoCommands
    {
        Task Create(ToDoItem item);

        Task Update(ToDoItem item);

        Task Delete(
            Guid siteId,
            Guid userId,
            Guid itemId
            );

        Task DeleteBySite(
            Guid siteId
            );

        Task DeleteByUser(
            Guid siteId,
            Guid userId
            );

    }
}
