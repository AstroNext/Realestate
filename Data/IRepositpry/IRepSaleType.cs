using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Threading.Tasks;

namespace Data
{
    public interface IRepSaleType
    {
        bool Add(SaleType saletype);

        List<SaleType> GetAll();
        bool Edit(SaleType saleType);
        bool Delete(SaleType saleType);
        bool Recovery(int id);
    }
}
