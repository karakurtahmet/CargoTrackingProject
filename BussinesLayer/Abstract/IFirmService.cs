using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Abstract
{
    public interface IFirmService
    {
        List<Firm> GetList();
        Firm GetByID(int id);
        void FirmAdd(Firm firm);
        void FirmDelete(Firm firm);
        void FirmUpdate(Firm firm);

    }
}
