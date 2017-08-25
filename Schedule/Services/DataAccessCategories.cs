using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.Model;


namespace Schedule.Services
{
    public class DataAccessCategories : IDataAccessCategory
    {
        ToDoDbEntities ctx = new ToDoDbEntities();

        public DataAccessCategories()
        {
            ctx = new ToDoDbEntities();
        }

        public void CreateCategory(CategoryItem Cat)
        {
            ctx.CategoryItems.Add(Cat);
            ctx.SaveChanges();
        }

        public void DeleteCategory(CategoryItem Cat)
        {
            ctx.CategoryItems.Remove(Cat);
            ctx.SaveChanges();
        }

        public ObservableCollection<CategoryItem> GetCategories()
        {
            ObservableCollection<CategoryItem> Categories = new ObservableCollection<CategoryItem>();

            var Q = from a in ctx.CategoryItems
                    select a;
            foreach (var item in Q)
            {
                Categories.Add(item);
            }
            return Categories;
        }

        public void UpdateCategory(CategoryItem NewCat, CategoryItem OldCat)
        {
            if (NewCat != null)
            {
                var v = from c in ctx.CategoryItems
                        where c.CategoryItemId== NewCat.CategoryItemId
                        select c;
                ctx.Entry(NewCat).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();

            }
        }
    }
}
