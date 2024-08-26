using Data;
using Model;
using System.Collections.Generic;

namespace Service
{
    public class AreaServ
    {
        private readonly RepoArea areaRepo = new RepoArea();
        public bool Add(Area area)
        {
            return areaRepo.Add(area);
        }
        public bool Edit(Area area)
        {
            return areaRepo.Edit(area);
        }
        public bool Delete(Area area)
        {
            return areaRepo.Delete(area);
        }
        public List<Area> GetAll()
        {
            return areaRepo.GetAll();
        }
        public bool Recovery(int id)
        {
            return areaRepo.Recovery(id);
        }
    }
}
