using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Schedule.Model;
using Schedule.Model.Joints;
using Schedule.Services;

namespace Schedule.ViewModel
{
    public class ItemsViewModel : ViewModelBase
    {
        IDataAccessItems _ServiceProxy;
#region Properties
        #region Private Properties
        private ObservableCollection<ItemCategoryJoinModel> _JoinedItems;
        private ItemCategoryJoinModel _SelectedJoinedItem;
        private ObservableCollection<CategoryItem> _CategoryItems;
        private CategoryItem _SeletedCategory;
        private Item _NewItem;
        int _CatID;
        #endregion Private

        #region Public Properties
        public ObservableCollection<ItemCategoryJoinModel> JoinedItems
        {
            get
            {
                return _JoinedItems;
            }

            set
            {
                _JoinedItems = value;
                RaisePropertyChanged("JoinedItems");
            }
        }

        public ItemCategoryJoinModel SelectedJoinedItem
        {
            get
            {
                return _SelectedJoinedItem;
            }

            set
            {
                _SelectedJoinedItem = value;
                RaisePropertyChanged("SelectedJoinedItem");
            }
        }

        public ObservableCollection<CategoryItem> CategoryItems
        {
            get
            {
                return _CategoryItems;
            }

            set
            {
                _CategoryItems = value;
                RaisePropertyChanged("CategoryItems");
            }
        }

        public CategoryItem SeletedCategory
        {
            get
            {
                return _SeletedCategory;
            }

            set
            {
                _SeletedCategory = value;
                RaisePropertyChanged("SeletedCategory");
            }
        }

        public Item NewItem
        {
            get
            {
                return _NewItem;
            }

            set
            {
                _NewItem = value;
                RaisePropertyChanged("NewItem");
            }
        }

        public int CatID
        {
            get
            {
                return _CatID;
            }

            set
            {
                _CatID = value;
                RaisePropertyChanged("CatID");
            }
        }
        #endregion Public
        #endregion Properties

#region Constructor
        /// <summary>
        /// COnstructor
        /// </summary>
        public ItemsViewModel(IDataAccessItems prxy)
        {
            _ServiceProxy = prxy;
            JoinedItems = new ObservableCollection<ItemCategoryJoinModel>();
            GetJoinedItems();
        }
        #endregion

#region Methods
    void GetJoinedItems()
        {
            JoinedItems.Clear();
            foreach (var item in _ServiceProxy.GetItems())
            {
                JoinedItems.Add(item);
            }
        }
#endregion
    }
}
