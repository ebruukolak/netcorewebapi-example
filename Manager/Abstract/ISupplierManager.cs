using System.Collections.Generic;
using Entity;

namespace Manager.Abstract
{
    public interface ISupplierManager
    {
        Suppliers GetByID(int ID);
        List<Suppliers> GetListSupplier();
    }
}