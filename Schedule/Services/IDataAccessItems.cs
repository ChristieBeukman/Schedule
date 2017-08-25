using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Schedule.Model.Joints;
using Schedule.Model;


namespace Schedule.Services
{
    public interface IDataAccessItems
    {
        ObservableCollection<ItemCategoryJoinModel> GetItems();
        ObservableCollection<CategoryItem> GetCategories();
        void AddItem(Item newItem);
        void DeleteItem(Item del);
        void UpdateItem(Item Old, Item New);
    }
}
