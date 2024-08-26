using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Area
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public EnumsModel.showType ShowType { get; set; }
    }
}
