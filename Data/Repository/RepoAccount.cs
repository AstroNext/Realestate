using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Data
{
    public class RepoAccount : IRepoAccount
    {
        context db = DataBaseConnection.dB;
        public bool Add(Account account)
        {
            bool flag = false;
            if(account !=null)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                flag = true;
            }
            return flag;
        }

        public Account Chek(string userName, string passWord)
        {
            Account acc = null;

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(passWord))
            {
                var found = db.Accounts.Where(item => item.UserName == userName && item.PassWord == passWord).SingleOrDefault();

                if (found != null)
                {
                    acc = found;
                }
            }
            return acc;
        }

        public bool Edit(Account acc)
        {
            var flag = false;
            if (acc != null)
            {
                var found = db.Accounts.Where(item => item.Id == acc.Id).SingleOrDefault();

                if(found != null)
                {
                    found.PassWord = acc.PassWord;

                    db.SaveChanges();
                    flag = true;
                }
            }
            return flag;
        }

        public List<Account> GetAll()
        {
            return db.Accounts.ToList();
        }
    }
}
