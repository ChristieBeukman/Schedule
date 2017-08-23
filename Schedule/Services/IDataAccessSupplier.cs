using Schedule.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Services
{
    public interface IDataAccessSupplier
    {
        ObservableCollection<Suppliier> GetSuppliers();
        void CreateSupplier(Suppliier Sup);
        void DeleteSupplier(Suppliier Sup);
        void UpdateSupplier(Suppliier NewSup, Suppliier OldSup);
    }
}
