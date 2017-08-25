using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Model.Joints
{
    public class ItemCategoryJoinModel
    {
        public ItemCategoryJoinModel()
        {

        }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public Nullable<int> QuantityPerItem { get; set; }
        public int CategoryItemId { get; set; }
        public string CategoryName { get; set; }
    }
}
