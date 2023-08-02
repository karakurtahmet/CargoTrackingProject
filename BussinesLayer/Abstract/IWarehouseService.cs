using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Abstract
{
    public interface IWarehouseService
    {
        List<Warehouse> GetList();
        Warehouse GetByID(int id);
        void WarehouseAdd(Warehouse warehouse);
        void WarehouseDelete(Warehouse warehouse);
        void WarehouseUpdate(Warehouse warehouse);
    }
}
