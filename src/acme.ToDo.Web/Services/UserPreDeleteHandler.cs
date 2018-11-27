using acme.ToDo.Models;
using cloudscribe.Core.Models.EventHandlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace acme.ToDo.Web.Services
{
    public class UserPreDeleteHandler : IHandleUserPreDelete
    {
        public UserPreDeleteHandler(IToDoCommands toDoCommands)
        {
            _toDoCommands = toDoCommands;
        }

        private readonly IToDoCommands _toDoCommands;

        public async Task HandleUserPreDelete(
            Guid siteId,
            Guid userId,
            CancellationToken cancellationToken = default(CancellationToken)
            )
        {
            await _toDoCommands.DeleteByUser(siteId, userId).ConfigureAwait(false);
        }
    }
}
