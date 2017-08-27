using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.Model;
using Schedule.Model.Joints;

namespace Schedule.Services
{
    public class DataAccessItems : IDataAccessItems
    {
        ToDoDbEntities ctx;

        public DataAccessItems()
        {
            ctx = new ToDoDbEntities();
        }

        public void AddItem(Item newItem)
        {
            ctx.Items.Add(newItem);
            ctx.SaveChanges();
        }

        public void DeleteItem(Item del)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<CategoryItem> GetCategories()
        {
            ObservableCollection<CategoryItem> cat = new ObservableCollection<CategoryItem>();
            var query = from a in ctx.CategoryItems
                        select a;

            foreach (var item in query)
            {
                cat.Add(item);
            }
            return cat;
        }

        public ObservableCollection<ItemCategoryJoinModel> GetItems()
        {
            ObservableCollection<ItemCategoryJoinModel> items = new ObservableCollection<ItemCategoryJoinModel>();

            var Q = (from c in ctx.Items
                     join m in ctx.CategoryItems
                     on c.CategoryItemId equals m.CategoryItemId
                     select new ItemCategoryJoinModel
                     {
                         ItemName = c.Name,
                         Description = c.Description,
                         QuantityPerItem = c.QuantityPerItem,
                         CategoryItemId = c.CategoryItemId,
                         CategoryName = m.Name
                     }).ToList();
            foreach (var item in Q)
            {
                items.Add(item);
            }
            return items;   
        }

        public void UpdateItem(Item Old, Item New)
        {
            if (New != null)
            {
                var v = from i in ctx.Items
                        where i.ItemId == 2
                        select i;
                ctx.Entry(New).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
