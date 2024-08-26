using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Data
{
    public class RepoLicence : IRepoLicence
    {
        context db = DataBaseConnection.dB;
        public bool Add(Licence licence)
        {
            bool flag = false;
            if (licence != null && db.Licences.ToList().Where(x => x.LicenceCode == licence.LicenceCode).ToList().Count == 0)
            {
                db.Licences.Add(licence);
                db.SaveChanges();
                flag = true;
            }
            return flag;
        }

        public bool EditExpire(int id)
        {
            bool flag = false;
            if (id != 0)
            {
                var found = db.Licences.Where(x => x.id.Equals(id)).SingleOrDefault();
                if (found != null)
                {
                    found.licencevalidation = EnumsModel.LicenceValidation.expiered;
                    found.Validation = EnumsModel.LicenceValidation.expiered.ToString();
                    db.SaveChanges();
                    flag = true;
                }
            }
            return flag;
        }

        public bool EditUsable(int id)
        {
            bool flag = false;
            if (id != 0)
            {
                var found = db.Licences.Where(x => x.id.Equals(id)).SingleOrDefault();
                if (found != null)
                {
                    found.licenceUsable = EnumsModel.LicenceUsable.notuse;
                    db.SaveChanges();
                    flag = true;
                }
            }
            return flag;
        }

        public List<Licence> GetAll()
        {
            return db.Licences.ToList();
        }
    }
}
