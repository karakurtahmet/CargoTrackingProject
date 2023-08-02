using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Abstract
{
    public interface IStoreService
    {
        List<Store> GetList();
        Store GetByID(int id);
        void StoreAdd(Store store);
        void StoreDelete(Store store);
        void StoreUpdate(Store store);
    }
}
