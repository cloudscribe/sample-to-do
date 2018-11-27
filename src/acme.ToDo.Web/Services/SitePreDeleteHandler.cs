using acme.ToDo.Models;
using cloudscribe.Core.Models.EventHandlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace acme.ToDo.Web.Services
{
    public class SitePreDeleteHandler : IHandleSitePreDelete
    {
        public SitePreDeleteHandler(IToDoCommands toDoCommands)
        {
            _toDoCommands = toDoCommands;
        }

        private readonly IToDoCommands _toDoCommands;

        public async Task HandleSitePreDelete(
            Guid siteId,
            CancellationToken cancellationToken = default(CancellationToken)
            )
        {
            await _toDoCommands.DeleteBySite(siteId).ConfigureAwait(false);
        }
    }
}
