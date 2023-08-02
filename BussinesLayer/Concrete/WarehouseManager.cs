using BussinesLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Concrete
{
    public class WarehouseManager : IWarehouseService
    {
        IWarehouseDal _warehouseDal;

        public WarehouseManager(IWarehouseDal warehouseDal) 
        {
            _warehouseDal = warehouseDal;
        }
        public Warehouse GetByID(int id)
        {
            return _warehouseDal.Get(x=>x.WarehouseId == id);
        }

        public List<Warehouse> GetList()
        {
            return _warehouseDal.List();
        }

        public void WarehouseAdd(Warehouse warehouse)
        {
            _warehouseDal.Insert(warehouse);
        }

        public void WarehouseDelete(Warehouse warehouse)
        {
            _warehouseDal.Delete(warehouse);
        }

        public void WarehouseUpdate(Warehouse warehouse)
        {
            _warehouseDal.Update(warehouse);
        }
    }
}
