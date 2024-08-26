using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SaleType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public EnumsModel.showType ShowType { get; set; }
    }
}
