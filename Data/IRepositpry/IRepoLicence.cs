using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IRepoLicence
    {
        bool Add(Licence licence);
        List<Licence> GetAll();
        bool EditExpire(int id);
        bool EditUsable(int id);

    }
}
