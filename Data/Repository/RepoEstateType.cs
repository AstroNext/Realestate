using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Data
{
    public class RepoEstateType : IRepEstateType
    {
        context db = DataBaseConnection.dB;
        public bool Add(EstateType estatetype)
        {
            bool flag = false;

            if (estatetype != null)
            {
                db.EstateTypes.Add(estatetype);
                db.SaveChanges();
                flag = true;
            }
            return flag;
        }

        public bool Delete(EstateType estatetype)
        {
            bool flag = false;

            if (estatetype != null)
            {
                var found = db.EstateTypes.Where(item => item.Id == estatetype.Id).SingleOrDefault();

                if(found !=null)
                {
                    found.ShowType = EnumsModel.showType.hide;
                    db.SaveChanges();
                    flag = true;
                }
            }
            return flag;
        }

        public bool Edit(EstateType estatetype)
        {
            bool flag = false;

            if(estatetype != null)
            {
                var found = db.EstateTypes.Where(item => item.Id == estatetype.Id).SingleOrDefault();

                if (found != null)
                {
                    found.Name = estatetype.Name;
                    found.ShowType = estatetype.ShowType;
                    db.SaveChanges();
                    flag = true;
                }
            }
            return flag;
        }

        public List<EstateType> GetAll()
        {
            return db.EstateTypes.ToList();
        }

        public bool Recovery(int id)
        {
            bool flag = false;
            if(string.IsNullOrEmpty(id.ToString()))
            {
                var found = db.EstateTypes.Where(x => x.Id.Equals(id)).SingleOrDefault();
                if(found != null)
                {
                    found.ShowType = EnumsModel.showType.show;
                    db.SaveChanges();
                    flag = true;
                }
            }
            return flag;
        }
    }
}
