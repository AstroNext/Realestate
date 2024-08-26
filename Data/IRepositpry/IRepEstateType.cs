using System;
using System.Collections.Generic;
using Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IRepEstateType
    {
        bool Add(EstateType estatetype);
        List<EstateType> GetAll();
        bool Edit(EstateType estatetype);
        bool Delete(EstateType estatetype);
        bool Recovery(int id);
    }
}
