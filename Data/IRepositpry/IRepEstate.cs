using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Threading.Tasks;

namespace Data
{
    public interface IRepEstate
    {
        bool Add(Estate estate);
        List<Estate> GetAll();
        bool Edit(Estate estate);
        bool ChangeSale(Estate estate);
        bool Delete(int id);
        bool Recovery(int id);
    }
}
