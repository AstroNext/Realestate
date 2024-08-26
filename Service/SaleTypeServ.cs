using Data;
using Model;
using System.Collections.Generic;

namespace Service
{
    public class SaleTypeServ
    {
        private readonly RepoSaleType saleTypeRepo = new RepoSaleType();
        public bool Add(SaleType saleType)
        {
            return saleTypeRepo.Add(saleType);
        }
        public bool Edit(SaleType saleType)
        {
            return saleTypeRepo.Edit(saleType);
        }
        public bool Delete(SaleType saleType)
        {
            return saleTypeRepo.Delete(saleType);
        }
        public List<SaleType> GetAll()
        {
            return saleTypeRepo.GetAll();
        }
        public bool Recovery(int id)
        {
            return saleTypeRepo.Recovery(id);
        }
    }
}
