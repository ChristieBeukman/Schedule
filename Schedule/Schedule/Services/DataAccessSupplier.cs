using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.Model;

namespace Schedule.Services
{
    public class DataAccessSupplier : IDataAccessSupplier
    {
        ToDoDbEntities cxt = new ToDoDbEntities();

        public DataAccessSupplier()
        {
            cxt = new ToDoDbEntities();
        }

        public void CreateSupplier(Suppliier Sup)
        {
            cxt.Suppliiers.Add(Sup);
            cxt.SaveChanges();
        }

        public void DeleteSupplier(Suppliier Sup)
        {
            cxt.Suppliiers.Remove(Sup);
            cxt.SaveChanges();
        }

        public ObservableCollection<Suppliier> GetSuppliers()
        {
            ObservableCollection<Suppliier> Suppliers = new ObservableCollection<Suppliier>();

            var Query = from a in cxt.Suppliiers
                        select a;
            foreach (var item in Query)
            {
                Suppliers.Add(item);
            }
            return Suppliers;
        }

        public void UpdateSupplier(Suppliier NewSup, Suppliier OldSup)
        {
            if (NewSup != null)
            {
                var v = from c in cxt.Suppliiers
                        where c.SupplierId == NewSup.SupplierId
                        select c;
                cxt.Entry(NewSup).State = System.Data.Entity.EntityState.Modified;
                cxt.SaveChanges();

            }
        }
    }
}
