using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Schedule.Model;

namespace Schedule.Services
{
    public interface IDataAccessCategory
    {
        ObservableCollection<CategoryItem> GetCategories();
        void CreateCategory(CategoryItem Cat);
        void DeleteCategory(CategoryItem Cat);
        void UpdateCategory(CategoryItem NewCat, CategoryItem OldCat);
    }
}
