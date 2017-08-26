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
        private ItemCategoryJoinModel _oldJoined;
        int _CatID;
        private string _catName;
        private string _ItemName;
        private string _ItemDescription;
        private int? _QuantityPerItem;
        private bool _ReadOnlyControlItem = true;
        private bool _HiddenControlItem = false;
        private bool _VisibleControlItem = true;

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

        public string ItemName
        {
            get
            {
                return _ItemName;
            }

            set
            {
                _ItemName = value;
                RaisePropertyChanged("ItemName");
            }
        }

        public string ItemDescription
        {
            get
            {
                return _ItemDescription;
            }

            set
            {
                _ItemDescription = value;
                RaisePropertyChanged("ItemDescription");
            }
        }

        public int? QuantityPerItem
        {
            get
            {
                return _QuantityPerItem;
            }

            set
            {
                _QuantityPerItem = value;
                RaisePropertyChanged("QuantityPerItem");
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

        public string CatName
        {
            get
            {
                return _catName;
            }

            set
            {
                _catName = value;
                RaisePropertyChanged("CatName");
            }
        }

        public bool ReadOnlyControlItem
        {
            get
            {
                return _ReadOnlyControlItem;
            }

            set
            {
                _ReadOnlyControlItem = value;
                RaisePropertyChanged("ReadOnlyControlItem");
            }
        }

        public bool HiddenControlItem
        {
            get
            {
                return _HiddenControlItem;
            }

            set
            {
                _HiddenControlItem = value;
                RaisePropertyChanged("HiddenControlItem");
            }
        }

        public bool VisibleControlItem
        {
            get
            {
                return _VisibleControlItem;
            }

            set
            {
                _VisibleControlItem = value;
                RaisePropertyChanged("VisibleControlItem");
            }
        }

        public ItemCategoryJoinModel OldJoined
        {
            get
            {
                return _oldJoined;
            }

            set
            {
                _oldJoined = value;
                RaisePropertyChanged("OldJoined");
            }
        }

        #endregion Public

        #region Commmands

        public RelayCommand ToggleEditCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }

        #endregion Commamds
        #endregion Properties

        #region Constructor
        /// <summary>
        /// COnstructor
        /// </summary>
        public ItemsViewModel(IDataAccessItems prxy)
        {
            _ServiceProxy = prxy;

            JoinedItems = new ObservableCollection<ItemCategoryJoinModel>();
            CategoryItems = new ObservableCollection<CategoryItem>();

            GetCategories();
            GetJoinedItems();

            ToggleEditCommand = new RelayCommand(ToggleControl);
            UpdateCommand = new RelayCommand(UpdateItems);

        }
        #endregion

#region Methods

        void ToggleControl()
        {
            if (VisibleControlItem == false)
            {
                ReadOnlyControlItem = true;
                HiddenControlItem = false;
                VisibleControlItem = true;
            }
            else if (VisibleControlItem == true)
            {
                ReadOnlyControlItem = false;
                HiddenControlItem = true;
                VisibleControlItem = false;

                
                OldJoined = SelectedJoinedItem;

                RaisePropertyChanged("CatName");
                RaisePropertyChanged("ItemName");
                RaisePropertyChanged("ItemDescription");
                RaisePropertyChanged("QuantityPerItem");
            }
            RaisePropertyChanged("ReadOnlyControlItem");
            RaisePropertyChanged("HiddenControlItem");
            RaisePropertyChanged("VisibleControlItem");

        }

        void GetJoinedItems()
        {
            JoinedItems.Clear();
            foreach (var item in _ServiceProxy.GetItems())
            {
                JoinedItems.Add(item);
            }
        }

        void GetCategories()
        {
            CategoryItems.Clear();
            foreach (var item in _ServiceProxy.GetCategories())
            {
                CategoryItems.Add(item);
            }

        }

        void AddItems()
        {

        }

        void UpdateItems()
        {
            if (SelectedJoinedItem != null)
            {
                CatID = SeletedCategory.CategoryItemId;
                ItemName = SelectedJoinedItem.ItemName;
                ItemDescription = SelectedJoinedItem.Description;
                QuantityPerItem = SelectedJoinedItem.QuantityPerItem;

                NewItem = new Item();

                NewItem.Name = ItemName;
                NewItem.Description = ItemDescription;
                NewItem.QuantityPerItem = QuantityPerItem;
                NewItem.CategoryItemId = CatID;

                JoinedItems.Remove(OldJoined);
                JoinedItems.Add(SelectedJoinedItem);
                _ServiceProxy.UpdateItem(NewItem, NewItem);
                GetJoinedItems();
                MessageBox.Show("Updated");
                RaisePropertyChanged("SelectedJoinedItem");
                ToggleControl();
            }
        }
        #endregion
    }
}
