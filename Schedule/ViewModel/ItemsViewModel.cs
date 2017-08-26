﻿using System;
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
        private Item _OldItem;
        private ItemCategoryJoinModel _OldJoinedItem;
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

        public Item OldItem
        {
            get
            {
                return _OldItem;
            }

            set
            {
                _OldItem = value;
                RaisePropertyChanged("OldItem");
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

        public ItemCategoryJoinModel OldJoinedItem
        {
            get
            {
                return _OldJoinedItem;
            }

            set
            {
                _OldJoinedItem = value;
                RaisePropertyChanged("OldJoinedItem");
            }
        }

        #endregion Public

        #region Commmands

        public RelayCommand ToggleEditCommand { get; set; }
        public RelayCommand UpdateItemCommand { get; set; }

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
            SeletedCategory = new CategoryItem();
            SelectedJoinedItem = new ItemCategoryJoinModel();
            NewItem = new Item();

            GetCategories();
            GetJoinedItems();

            ToggleEditCommand = new RelayCommand(ToggleControl);
            UpdateItemCommand = new RelayCommand(UpdateItem);

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

                OldItem = new Item();
                OldJoinedItem = new ItemCategoryJoinModel();

                OldItem.ItemId = SelectedJoinedItem.ItemId;
                OldItem.Name = SelectedJoinedItem.ItemName;
                OldItem.Description = SelectedJoinedItem.Description;
                OldItem.QuantityPerItem = SelectedJoinedItem.QuantityPerItem;
                OldItem.CategoryItemId = SelectedJoinedItem.CategoryItemId;
                OldJoinedItem = SelectedJoinedItem;


                ItemName = SelectedJoinedItem.ItemName;
                ItemDescription = SelectedJoinedItem.Description;
                QuantityPerItem = SelectedJoinedItem.QuantityPerItem;
                CatName = SeletedCategory.Name;

                RaisePropertyChanged("OldItem");
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

        void UpdateItem()
        {
            if (SelectedJoinedItem != null)
            {
                NewItem.ItemId = SelectedJoinedItem.ItemId;
                NewItem.Description = SelectedJoinedItem.Description;
                NewItem.Name = SelectedJoinedItem.ItemName;
                NewItem.QuantityPerItem = SelectedJoinedItem.QuantityPerItem;
                NewItem.CategoryItemId = SeletedCategory.CategoryItemId;

                //JoinedItems.Add(SelectedJoinedItem);
                JoinedItems.Remove(OldJoinedItem);
              
                

               // _ServiceProxy.UpdateItem(NewItem, OldItem);
                GetJoinedItems();
                MessageBox.Show("Updated");
                RaisePropertyChanged("SelectedJoinedItem");
                ToggleControl();
            }
        }
        #endregion
    }
}
