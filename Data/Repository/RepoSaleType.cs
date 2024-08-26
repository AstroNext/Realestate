using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Data
{
    public class RepoSaleType : IRepSaleType
    {
        context db = DataBaseConnection.dB;
        public bool Add(SaleType saletype)
        {
            bool flag = false;

            if(saletype != null)
            {
                db.SaleTypes.Add(saletype);
                db.SaveChanges();
                flag = true;
            }
            return flag;
        }
        public bool Delete(SaleType saleType)
        {
            bool flag = false;

            if (saleType != null)
            {
                var found = db.SaleTypes.Where(item => item.Id == saleType.Id).SingleOrDefault(); ;

                if(found!= null)
                {
                    found.ShowType = EnumsModel.showType.hide;
                    db.SaveChanges();
                    flag = true;
                }
            }
            return flag;
        }
        public bool Edit(SaleType saleType)
        {
            bool flag = false;

            if (saleType != null)
            {
                var found = db.SaleTypes.Where(item => item.Id == saleType.Id).SingleOrDefault(); ;

                if (found != null)
                {
                    found.Name = saleType.Name;
                    found.ShowType = saleType.ShowType;
                    db.SaveChanges();
                    flag = true;
                }
            }
            return flag;
        }
        public List<SaleType> GetAll()
        {
            return db.SaleTypes.ToList();
        }

        public bool Recovery(int id)
        {
            bool flag = false;
            if(!string.IsNullOrEmpty(id.ToString()))
            {
                var found = db.SaleTypes.Where(x => x.Id == id).SingleOrDefault();
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
