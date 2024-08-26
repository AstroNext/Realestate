using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Estate
    {
        [Key]
        public int Id { get; set; }
        public string Info { get; set; }
        public decimal RahnDeposit { get; set; }
        public decimal EjareDeposit { get; set; }
        public decimal SaleDeposit { get; set; }
        public virtual Adress Adress { get; set; }
        public virtual EstateType EstateType { get; set; }
        public virtual SaleType SaleType { get; set; }
        public virtual Area Area { get; set; }
        public virtual EstateDetail EstateDetail { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual Buyer Buyer { get; set; }
        public virtual EnumsModel.Sele Sale { get; set; }
        public virtual EnumsModel.showType Show { get; set; }
    }
}
