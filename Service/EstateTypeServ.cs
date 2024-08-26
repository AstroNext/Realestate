using Data;
using Model;
using System.Collections.Generic;

namespace Service
{
    public class EstateTypeServ
    {
        private readonly RepoEstateType estateTypeRepo = new RepoEstateType();
        public bool Add(EstateType estateType)
        {
            return estateTypeRepo.Add(estateType);
        }
        public bool Edit(EstateType estateType)
        {
            return estateTypeRepo.Edit(estateType);
        }
        public bool Delete(EstateType estateType)
        {
            return estateTypeRepo.Delete(estateType);
        }
        public List<EstateType> GetAll()
        {
            return estateTypeRepo.GetAll();
        }
        public bool Recovery(int id)
        {
            return estateTypeRepo.Recovery(id);
        }
    }
}
