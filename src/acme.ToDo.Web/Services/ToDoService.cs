using acme.ToDo.Models;
using cloudscribe.Core.Models;
using cloudscribe.Core.Web.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace acme.ToDo.Web.Services
{
    /// <summary>
    /// this service is a wrapper around the queries and commands
    /// if you have business rules, the service layer would be the place to put them
    /// here we are also depending on the cloudscribe ISiteContextResolver and IUserContextResolver
    /// which gets the userid and siteid we can use to tag our feature data.
    /// </summary>
    public class ToDoService
    {
        public ToDoService(
            IToDoCommands toDoCommands,
            IToDoQueries toDoQueries,
            IUserContextResolver userContextResolver,
            ILogger<ToDoService> logger
            )
        {
            _toDoCommands = toDoCommands;
            _toDoQueries = toDoQueries;
            _userContextResolver = userContextResolver;
            _log = logger;
        }

        private readonly IToDoCommands _toDoCommands;
        private readonly IToDoQueries _toDoQueries;
        private readonly IUserContextResolver _userContextResolver;
        private readonly ILogger _log;

        public async Task AddItem(ToDoItem item)
        {
            var user = await _userContextResolver.GetCurrentUser();
            item.UserId = user.Id;
            item.SiteId = user.SiteId;
            await _toDoCommands.Create(item);

        }

        public async Task MarkAsDone(Guid itemId)
        {
            var user = await _userContextResolver.GetCurrentUser();
            var item = await _toDoQueries.Fetch(user.Id, itemId);
            if(item == null)
            {
                throw new InvalidOperationException("Item not found");
            }
            item.IsComplete = true;
            await _toDoCommands.Update(item);
        }

        public async Task<List<ToDoItem>> GetIncompleteItems(
            CancellationToken cancellationToken = default(CancellationToken)
            )
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _userContextResolver.GetCurrentUser();
            return await _toDoQueries.GetIncompleteItems(user.Id, cancellationToken);

        }

    }
}
