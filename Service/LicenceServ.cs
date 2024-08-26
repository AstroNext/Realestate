using System;
using System.Collections.Generic;
using Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service
{
    public class LicenceServ
    {
        private readonly RepoLicence liceneceRepo = new RepoLicence();

        public bool Add(Licence licence)
        {
            return liceneceRepo.Add(licence);
        }
        public List<Licence> GetAll()
        {
            return liceneceRepo.GetAll();
        }
        public bool EditExpire(int id)
        {
            return liceneceRepo.EditExpire(id);
        }
        public bool EditUsable(int id)
        {
            return liceneceRepo.EditUsable(id);
        }
    }
}
