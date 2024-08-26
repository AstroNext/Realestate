using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Adress
    {
        public int Id { get; set; }
        public string EstateName { get; set; }
        public int Floor { get; set; }
        public string Street { get; set; }
        public string Alley { get; set; }
        public long Postcode { get; set; }
        public int Unit { get; set; }
    }
}
