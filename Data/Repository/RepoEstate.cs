using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Data
{
    public class RepoEstate : IRepEstate
    {
        context db = DataBaseConnection.dB;
        public bool Add(Estate estate)
        {
            bool flag = false;
            if(estate != null)
            {
                db.Estates.Add(estate);
                db.SaveChanges();
                flag = true;
            }
            return flag;
        }

        public bool ChangeSale(Estate estate)
        {
            bool flag = false;
            if (estate != null)
            {
                var found = db.Estates.Where(x => x.Id == estate.Id).SingleOrDefault();

                if( found != null)
                {
                    found.Buyer = estate.Buyer;
                    found.EstateDetail.DischargeTime = estate.EstateDetail.DischargeTime;
                    found.Sale = EnumsModel.Sele.sale;

                    db.SaveChanges();
                    flag = true;
                }
            }
            return flag;
        }

        public bool Delete(int id)
        {
            bool flag = false;
            if(id != 0)
            {
                var found = db.Estates.Where(item => item.Id == id).SingleOrDefault();
                if (found != null)
                {
                    found.Show = EnumsModel.showType.hide;
                    db.SaveChanges();
                    flag = true;
                }
            }
            return flag;
        }
        public bool Edit(Estate estate)
        {
            bool flag = false;
            if (estate != null)
            {
                var found = db.Estates.Where(item => item.Id == estate.Id).SingleOrDefault();
                if (found != null)
                {
                    found.Adress = estate.Adress;
                    found.Area = estate.Area;
                    found.Buyer = estate.Buyer;
                    found.EjareDeposit = estate.EjareDeposit;
                    found.EstateDetail = estate.EstateDetail;
                    found.EstateType = estate.EstateType;
                    found.Owner = estate.Owner;
                    found.RahnDeposit = estate.RahnDeposit;
                    found.Sale = estate.Sale;
                    found.SaleType = estate.SaleType;
                    found.Show = estate.Show;
                    found.Info = estate.Info;
                    
                    db.SaveChanges();
                    flag = true;
                }
            }
            return flag;
        }
        public List<Estate> GetAll()
        {
            return db.Estates.ToList();
        }

        public bool Recovery(int id)
        {
            bool flag = false;
            if(!string.IsNullOrEmpty(id.ToString()))
            {
                var found = db.Estates.Where(x => x.Id.Equals(id)).SingleOrDefault();
                if(found != null)
                {
                    found.Show = EnumsModel.showType.show;
                    db.SaveChanges();
                    flag = true;
                }
            }
            return flag;
        }
    }
}
