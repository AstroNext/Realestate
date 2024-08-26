using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EstateDetail
    {
        public int Id { get; set; }
        public string TheArea { get; set; }
        public int RoomCount { get; set; }
        public int MasterRoomCount { get; set; }
        public EnumsModel.Direction Direction { get; set; }
        public int BuildYear { get; set; }
        public bool KeySale { get; set; }
        public string DischargeTime { get; set; }
        public string Info { get; set; }
    }
}
