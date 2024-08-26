using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IRepArea
    {
        bool Add(Area area);
        List<Area> GetAll();
        bool Edit(Area area);
        bool Delete(Area area);
        bool Recovery(int id);
    }
}
