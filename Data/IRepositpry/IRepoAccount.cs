using System;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IRepoAccount
    {
        bool Add(Account account);
        Account Chek(string userName, string passWord);
        List<Account> GetAll();
        bool Edit(Account acc);
        
    }
}
