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
    public class FirmManager : IFirmService
    {
        IFirmDal _firmDal;
        public FirmManager(IFirmDal firmDal)
        {
            _firmDal = firmDal;
        }
        public void FirmAdd(Firm firm)
        {
            _firmDal.Insert(firm);
        }

        public void FirmDelete(Firm firm)
        {
            _firmDal.Delete(firm);
        }

        public void FirmUpdate(Firm firm)
        {
            _firmDal.Update(firm);
        }

        public Firm GetByID(int id)
        {
            return _firmDal.Get(x=>x.FirmId==id);
        }

        public List<Firm> GetList()
        {
            return _firmDal.List();
        }
    }
}
