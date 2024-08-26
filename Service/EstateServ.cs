using Data;
using Model;
using System.Collections.Generic;

namespace Service
{
    public class EstateServ
    {
        private readonly RepoEstate estateRepo = new RepoEstate();

        public bool Add(Estate estate)
        {
            return estateRepo.Add(estate);
        }
        public bool Edit(Estate estate)
        {
            return estateRepo.Edit(estate);
        }
        public bool Delete(int id)
        {
            return estateRepo.Delete(id);
        }
        public List<Estate> GetAll()
        {
            return estateRepo.GetAll();
        }
        public bool ChangeSale(Estate estate)
        {
            return estateRepo.ChangeSale(estate);
        }
        public bool Recovery(int id)
        {
            return estateRepo.Recovery(id);
        }
    }
}
