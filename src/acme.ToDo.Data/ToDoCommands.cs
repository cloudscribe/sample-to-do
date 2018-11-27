using acme.ToDo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace acme.ToDo.Data
{
    public class ToDoCommands : IToDoCommands
    {
        public ToDoCommands(ToDoDbContextFactory toDoDbContextFactory)
        {
            _contextFactory = toDoDbContextFactory;
        }

        private readonly ToDoDbContextFactory _contextFactory;

        public async Task Create(ToDoItem item)
        {
            using (var _db = _contextFactory.CreateContext())
            {
                _db.Items.Add(item);
                int rowsAffected = await _db.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task Update(ToDoItem item)
        {
            using (var _db = _contextFactory.CreateContext())
            {
                _db.Items.Update(item);
                int rowsAffected = await _db.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task Delete(
            Guid siteId,
            Guid userId,
            Guid itemId
            )
        {
            // itemid would be sufficient to delete an item but as extra security
            // to block data from one site or user from being deleted by a different user or different site
           
            using (var _db = _contextFactory.CreateContext())
            {
                var itemToRemove = await _db.Items.SingleOrDefaultAsync(x => x.Id == itemId && x.SiteId == siteId && x.UserId == userId).ConfigureAwait(false);

                if (itemToRemove == null) throw new InvalidOperationException("item to delete not found");

                _db.Items.Remove(itemToRemove);
                int rowsAffected = await _db.SaveChangesAsync()
                    .ConfigureAwait(false);
            }
        }

        public async Task DeleteBySite(Guid siteId)
        {
            using (var _db = _contextFactory.CreateContext())
            {
                var itemsToRemove = _db.Items.Where(x =>  x.SiteId == siteId);
                _db.Items.RemoveRange(itemsToRemove);
                int rowsAffected = await _db.SaveChangesAsync()
                    .ConfigureAwait(false);
            }
        }

        public async Task DeleteByUser(
            Guid siteId,
            Guid userId
            )
        {
            using (var _db = _contextFactory.CreateContext())
            {
                var itemsToRemove = _db.Items.Where(x => x.SiteId == siteId && x.UserId == userId);
                _db.Items.RemoveRange(itemsToRemove);
                int rowsAffected = await _db.SaveChangesAsync()
                    .ConfigureAwait(false);
            }
        }

    }
}
