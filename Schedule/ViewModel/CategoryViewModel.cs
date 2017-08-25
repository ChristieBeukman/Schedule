using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Schedule.Services;
using Schedule.Model;
using System.Windows;
using Schedule.View;

namespace Schedule.ViewModel
{
    public class CategoryViewModel : ViewModelBase
    {
        private IDataAccessCategory _ServiceProxy;

        #region Properties
        #region Private Properties
        private ObservableCollection<CategoryItem> _Categories;
        private CategoryItem _Category;
        private CategoryItem _SelectedCategory;
        private CategoryItem _Cat;
        private bool _ReadOnlyControl = true;
        private bool _HiddenControl = false;
        private bool _VisibleControl = true;
        private string _catName;
        private string _catDescription;
        private CategoryItem _OldCategory;
        private bool _MainView;
        private bool _AddView;
        #endregion Private Properties

        #region Public Properties
        public ObservableCollection<CategoryItem> Categories
        {
            get
            {
                return _Categories;
            }

            set
            {
                _Categories = value;
                RaisePropertyChanged("Categories");
            }
        }

        public CategoryItem Category
        {
            get
            {
                return _Category;
            }

            set
            {
                _Category = value;
                RaisePropertyChanged("Category");
            }
        }

        public CategoryItem SelectedCategory
        {
            get
            {
                return _SelectedCategory;
            }

            set
            {
                _SelectedCategory = value;
                RaisePropertyChanged("SelectedCategory");
            }
        }

        public CategoryItem Cat
        {
            get
            {
                return _Cat;
            }

            set
            {
                _Cat = value;
                RaisePropertyChanged("Cat");
            }
        }

        public bool ReadOnlyControl
        {
            get
            {
                return _ReadOnlyControl;
            }

            set
            {
                _ReadOnlyControl = value;
                RaisePropertyChanged("ReadOnlyControl");
            }
        }

        public bool HiddenControl
        {
            get
            {
                return _HiddenControl;
            }

            set
            {
                _HiddenControl = value;
                RaisePropertyChanged("HiddenControl");
            }
        }

        public bool VisibleControl
        {
            get
            {
                return _VisibleControl;
            }

            set
            {
                _VisibleControl = value;
                RaisePropertyChanged("VisibleControl");
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

        public string CatDescription
        {
            get
            {
                return _catDescription;
            }

            set
            {
                _catDescription = value;
                RaisePropertyChanged("CatDescription");
            }
        }

        public CategoryItem OldCategory
        {
            get
            {
                return _OldCategory;
            }

            set
            {
                _OldCategory = value;
                RaisePropertyChanged("OldCategory");
            }
        }

        public bool MainView
        {
            get
            {
                return _MainView;
            }

            set
            {
                _MainView = value;
                RaisePropertyChanged("MainView");
            }
        }

        public bool AddView
        {
            get
            {
                return _AddView;
            }

            set
            {
                _AddView = value;
                RaisePropertyChanged("AddView");
            }
        }

        #endregion Public Properties

        #region Commands
        public RelayCommand AddCategoryCommand { get; set; }
        public RelayCommand OpenAddCategoryViewCommand { get; set; }
        public RelayCommand DeleteCategoryCommand { get; set; }
        public RelayCommand CategoryControlActivatorCommand { get; set; }
        public RelayCommand UpdateCategoryCommand { get; set; }

        #endregion Commands

        #endregion Properties

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="prxy"></param>
        public CategoryViewModel(IDataAccessCategory prxy)
        {
            _ServiceProxy = prxy;
            Categories = new ObservableCollection<CategoryItem>();
            Category= new CategoryItem();
            SelectedCategory= new CategoryItem();
            GetCategories();
            Cat= new CategoryItem();
            OldCategory= new CategoryItem();
            AddCategoryCommand= new RelayCommand(AddCategory);
            OpenAddCategoryViewCommand= new RelayCommand(OpenAddCategoryWindow);
            DeleteCategoryCommand = new RelayCommand(DeleteCategory);
            CategoryControlActivatorCommand = new RelayCommand(ToggleControl);
            UpdateCategoryCommand = new RelayCommand(UpdateCategory);
        }

        #region Methods

        /// <summary>
        /// disables text readonly and hides combobox
        /// </summary>
        void ToggleControl()
        {
            if (VisibleControl == false)
            {
                ReadOnlyControl= true;
                HiddenControl= false;
                VisibleControl = true;
            }
            else if (VisibleControl== true)
            {
                ReadOnlyControl = false;
                HiddenControl = true;
                VisibleControl= false;

                CatDescription = SelectedCategory.Description;
                CatName= SelectedCategory.Name;
                
                RaisePropertyChanged("CatName");
                RaisePropertyChanged("CatDescription");
            }
            RaisePropertyChanged("ReadOnlyControl");
            RaisePropertyChanged("HiddenControl");
            RaisePropertyChanged("VisibleControl");

        }

        /// <summary>
        /// Fill the Combobox with the database data
        /// </summary>
        void GetCategories()
        {
            Categories.Clear();
            foreach (var item in _ServiceProxy.GetCategories())
            {
                Categories.Add(item);
            }
        }

        /// <summary>
        /// Add new record to the database
        /// </summary>
        void AddCategory()
        {
            Categories.Add(Cat);
            _ServiceProxy.CreateCategory(Cat);
            RaisePropertyChanged("Cat");
            MessageBox.Show(Cat.Name + " has been succesfully added");
            AddView = false;
            MainView = true;
            RaisePropertyChanged("AddView");
            RaisePropertyChanged("MainView");
        }

        /// <summary>
        /// Opens the Add Supplier Window
        /// </summary>
        void OpenAddCategoryWindow()
        {
            if (MainView == true)
            {
                MainView = false;
                AddView = true;
            }
            else
            {
                MainView = true;
                AddView = false;
            }
            RaisePropertyChanged("MainView");
            RaisePropertyChanged("AddView");
        }

        /// <summary>
        /// delete selected record
        /// </summary>
        void DeleteCategory()
        {
            var Result = MessageBox.Show("Delete " + SelectedCategory.Name, "DELETE", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (Result)
            {
                case MessageBoxResult.Yes:
                    _ServiceProxy.DeleteCategory(SelectedCategory);
                    Categories.Remove(SelectedCategory);
                    MessageBox.Show("Deleted", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    GetCategories();
                    RaisePropertyChanged("SelectedCategory");
                    RaisePropertyChanged("Categories");
                    break;
                case MessageBoxResult.No:

                    break;
                default:
                    break;
            }

        }

        void UpdateCategory()
        {
            if (SelectedCategory != null)
            {

                Categories.Add(SelectedCategory);
                OldCategory.Description = CatDescription;
                OldCategory.Name = CatName;
                
                Categories.Remove(OldCategory);
                _ServiceProxy.UpdateCategory(SelectedCategory, OldCategory);
                GetCategories();
                MessageBox.Show("Updated");
                RaisePropertyChanged("SelectedCategory");
                ToggleControl();
            }
        }

        #endregion
    }
}
