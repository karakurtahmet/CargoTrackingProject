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
    public class StoreManager : IStoreService
    {
        IStoreDal _storeDal;
        public StoreManager(IStoreDal storeDal) 
        {
            _storeDal = storeDal;
        }
        public Store GetByID(int id)
        {
            return _storeDal.Get(x => x.StoreId == id);
        }

        public List<Store> GetList()
        {
            return _storeDal.List();
        }

        public void StoreAdd(Store store)
        {
            _storeDal.Insert(store);
        }

        public void StoreDelete(Store store)
        {
            _storeDal.Delete(store);
        }

        public void StoreUpdate(Store store)
        {
            _storeDal.Update(store);
        }

        
    }
}
