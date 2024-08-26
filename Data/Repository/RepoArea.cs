using System;
using System.Collections.Generic;
using System.Linq;
using Model;

namespace Data
{
    public class RepoArea : IRepArea
    {
        context db = DataBaseConnection.dB;
        public bool Add(Area area)
        {
            bool flag = false;

            if (area != null)
            {
                db.Areas.Add(area);
                db.SaveChanges();
                flag = true;
            }
            return flag;
        }
        public bool Delete(Area area)
        {
            bool flag = false;

            if(area != null)
            {
                var found = db.Areas.Where(item => item.Id == area.Id).SingleOrDefault();

                if(found != null)
                {
                    found.ShowType = EnumsModel.showType.hide;
                    db.SaveChanges();
                    flag = true;
                }
            }
            return flag;
        }
        public bool Edit(Area area)
        {
            bool flag = false;

            if (area != null)
            {
                var found = db.Areas.Where(item => item.Id == area.Id).SingleOrDefault();

                if (found != null)
                {
                    found.Name = area.Name;
                    found.ShowType = area.ShowType;
                    db.SaveChanges();
                    flag = true;
                }
            }
            return flag;
        }
        public List<Area> GetAll()
        {
            return db.Areas.ToList();
        }

        public bool Recovery(int id)
        {
            bool flag = false;
            if(!string.IsNullOrEmpty(id.ToString()))
            {
                var found = db.Areas.Where(x => x.Id == id).SingleOrDefault();

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
