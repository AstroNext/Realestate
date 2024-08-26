using System;
using Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace RealEstate
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            AccountServ accountSvr = new AccountServ();
            Mutex mutex = new Mutex(initiallyOwned: true, "RealEstate", out bool Res);

            if(!Res)
            {
                MessageBox.Show("برنامه در حال اجرا است", "پیام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (accountSvr.GetAll().Count == 0)
                {
                    Application.Run(new CreateAccount());
                }
                else if(new LicenceServ().GetAll().Count == 0)
                {
                    Application.Run(new AddNewLicence());
                }
                else
                {
                    Application.Run(new LoginForm());
                }
            }
            
        }

    }
}
