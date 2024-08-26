using System;
using Data;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AccountServ
    {
        private readonly RepoAccount accountRepo = new RepoAccount();
        public bool Add(Account account)
        {
            return accountRepo.Add(account);
        }
        public Account Chek (string userName,string passWord)
        {
            return accountRepo.Chek(userName, passWord);
        }
        public List<Account> GetAll()
        {
            return accountRepo.GetAll();
        }
        public bool Edit(Account acc)
        {
            return accountRepo.Edit(acc);
        }
    }
}
