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
    public class SupplierViewModel : ViewModelBase
    {
        #region Properties

        #region Private
        /// <summary>
        /// Private Public Properties
        /// </summary>
        IDataAccessSupplier _ServiceProxy;
        private ObservableCollection<Suppliier> _Suppliers;
        private Suppliier _Supplier;
        private Suppliier _SelectedSupplier;
        private Suppliier _Sup;
        private bool controlIsReadOnly = true;
        private bool hiddenControlEnabled = false;
        private bool visibleCOntrolEnabled = true;
        private bool visibileMainControl = true;
        private bool visibleAddControl = false; 
        string catName;
        string SupDescription;
        int SupID;
        string supLocation;
        private Suppliier _OldSupplier;
        private bool _MainView =true;
        private bool _AddView = false;

        #endregion Private

        #region Public
        /// <summary>
        /// Public Properties
        /// </summary>
        public ObservableCollection<Suppliier> Suppliers
        {
            get
            {
                return _Suppliers;
            }

            set
            {
                _Suppliers = value;
                RaisePropertyChanged("Suppliers");
            }
        }

        public Suppliier Supplier
        {
            get
            {
                return _Supplier;
            }

            set
            {
                _Supplier = value;
                RaisePropertyChanged("Supplier");
            }
        }

        public Suppliier SelectedSupplier
        {
            get
            {
                return _SelectedSupplier;
            }

            set
            {
                _SelectedSupplier = value;
                RaisePropertyChanged("SelectedSupplier");
            }
        }

        public Suppliier Sup
        {
            get
            {
                return _Sup;
            }

            set
            {
                _Sup = value;
                RaisePropertyChanged("Sup");
            }
        }

        public string SupName
        {
            get
            {
                return catName;
            }

            set
            {
                catName = value;
                RaisePropertyChanged("CatName");
            }
        }

        public string CatDescription
        {
            get
            {
                return SupDescription;
            }

            set
            {
                SupDescription = value;
                RaisePropertyChanged("CatDescription");
            }
        }

        public int CatID
        {
            get
            {
                return SupID;
            }

            set
            {
                SupID = value;
                RaisePropertyChanged("CatID");
            }
        }

        public bool ControlIsReadOnly
        {
            get
            {
                return controlIsReadOnly;
            }

            set
            {
                controlIsReadOnly = value;
                RaisePropertyChanged("ControlIsReadOnly");
            }
        }

        public bool HiddenControlEnabled
        {
            get
            {
                return hiddenControlEnabled;
            }

            set
            {
                hiddenControlEnabled = value;
                RaisePropertyChanged("HiddenControlEnabled");
            }
        }

        public bool VisibleCOntrolEnabled
        {
            get
            {
                return visibleCOntrolEnabled;
            }

            set
            {
                visibleCOntrolEnabled = value;
                RaisePropertyChanged("VisibleCOntrolEnabled");
            }
        }

        public string SupLocation
        {
            get
            {
                return supLocation;
            }

            set
            {
                supLocation = value;
                RaisePropertyChanged("SUpLocation");
            }
        }

        public Suppliier OldSupplier
        {
            get
            {
                return _OldSupplier;
            }

            set
            {
                _OldSupplier = value;
                RaisePropertyChanged("OldSupplier");
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

        #endregion Public

        #region Commands

        public RelayCommand AddSupplierCommand { get; set; }
        public RelayCommand OpenAddSupplierView { get; set; }
        public RelayCommand DeleteSupplierCommand { get; set; }
        public RelayCommand ControlActivatorCommand { get; set; }
        public RelayCommand UpdateSupplierCommand { get; set; }



        #endregion
        #endregion Properties

        #region Constructor
        /// <summary>
        /// COnstructor Initialises all objects
        /// </summary>
        /// <param name="prxy"></param>
        public SupplierViewModel(IDataAccessSupplier prxy)
        {
            _ServiceProxy = prxy;
            Suppliers = new ObservableCollection<Suppliier>();
            Supplier = new Suppliier();
            SelectedSupplier = new Suppliier();
            GetSupplier();
            Sup = new Suppliier();
            OldSupplier = new Suppliier();
            AddSupplierCommand = new RelayCommand(AddSupplier);
            OpenAddSupplierView = new RelayCommand(OpenAddSupplierWindow);
            DeleteSupplierCommand = new RelayCommand(DeleteSupplier);
            ControlActivatorCommand = new RelayCommand(ToggleControl);
            UpdateSupplierCommand = new RelayCommand(UpdateSupplier);
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// disables text readonly and hides combobox
        /// </summary>
        void ToggleControl()
        {
            if (VisibleCOntrolEnabled == false)
            {
                ControlIsReadOnly = true;
                HiddenControlEnabled = false;
                VisibleCOntrolEnabled = true;
            }
            else if (VisibleCOntrolEnabled == true)
            {
                ControlIsReadOnly = false;
                HiddenControlEnabled = true;
                VisibleCOntrolEnabled = false;

                SupDescription = SelectedSupplier.Description;
                SupName = SelectedSupplier.Name;
                SupID = SelectedSupplier.SupplierId;
                SupLocation = SelectedSupplier.Location;
                RaisePropertyChanged("SupName");
                RaisePropertyChanged("SupID");
                RaisePropertyChanged("SupLocation");
            }
            RaisePropertyChanged("ControlIsReadOnly");
            RaisePropertyChanged("HiddenControlEnabled");
            RaisePropertyChanged("VisibleCOntrolEnabled");

        }

        /// <summary>
        /// Fill the Combobox with the database data
        /// </summary>
        void GetSupplier()
        {
            Suppliers.Clear();
            foreach (var item in _ServiceProxy.GetSuppliers())
            {
                Suppliers.Add(item);
            }
        }

        /// <summary>
        /// Add new record to the database
        /// </summary>
        void AddSupplier()
        {
            Suppliers.Add(Sup);
            _ServiceProxy.CreateSupplier(Sup);
            RaisePropertyChanged("Sup");
            MessageBox.Show(Sup + " has been succesfully added");
            AddView = false;
            MainView = true;
            RaisePropertyChanged("AddView");
            RaisePropertyChanged("MainView");
        }

        /// <summary>
        /// Opens the Add Supplier Window
        /// </summary>
        void OpenAddSupplierWindow()
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
        void DeleteSupplier()
        {
            var Result = MessageBox.Show("Delete " + SelectedSupplier.Name, "DELETE", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (Result)
            {
                case MessageBoxResult.Yes:
                    _ServiceProxy.DeleteSupplier(SelectedSupplier);
                    Suppliers.Remove(SelectedSupplier);
                    MessageBox.Show("Deleted", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    GetSupplier();
                    RaisePropertyChanged("SelectedSupplier");
                    RaisePropertyChanged("Suppliers");
                    break;
                case MessageBoxResult.No:

                    break;
                default:
                    break;
            }

        }

        void UpdateSupplier()
        {
            if (SelectedSupplier != null)
            {

                Suppliers.Add(SelectedSupplier);
                OldSupplier.Description = SupDescription;
                OldSupplier.Name = SupLocation;
                OldSupplier.Location = SupLocation;
                OldSupplier.SupplierId = SupID;
                Suppliers.Remove(OldSupplier);
                _ServiceProxy.UpdateSupplier(SelectedSupplier, OldSupplier);
                GetSupplier();
                MessageBox.Show("Updated");
                RaisePropertyChanged("SelectedSupplier");
                ToggleControl();
            }
        }
        #endregion Constructor

    }
}
