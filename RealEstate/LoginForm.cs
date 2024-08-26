using System;
using System.Security.Cryptography;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Service;
using RealEstate.Properties;

namespace RealEstate
{
    public partial class LoginForm : Form
    {
        AccountServ account = new AccountServ();
        CreateAccount cra = new CreateAccount();
        LicenceServ liServ = new LicenceServ();
        private string secretPhase = new EncriptPassword().secretPhase();
        public LoginForm()
        {
            foreach (var item in liServ.GetAll())
            {
                if (CheckExpireLicence(item.LicenceCode))
                {
                    liServ.EditExpire(item.id);
                }
            }

            InitializeComponent();

            com_userName.DataSource = account.GetAll();
        }
        public bool CheckExpireLicence(string liInput)
        {
            //SKGL.Validate valid = new SKGL.Validate();
            //valid.secretPhase = secretPhase;
            //valid.Key = liInput;

            //if (valid.IsExpired)
            //{
            //    return true;
            //}
            return true;
        }
        ///////////////////////////////////////////////////////////////////

        private bool _dragging;
        private Point _startPoint = new Point(0, 0);
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _startPoint = new Point(e.X, e.Y);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_dragging) return;
            var p = PointToScreen(e.Location);
            Location = new Point(p.X - this._startPoint.X, p.Y - this._startPoint.Y);
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }
        ///////////////////////////////////////////////////////////////////

        private void button2_Click(object sender, EventArgs e)//login btn
        {
            var chek = account.Chek(com_userName.Text, new EncriptPassword().encrypt(txt_passWord.Text));//pass
            var licence = liServ.GetAll().Where(x => x.licenceUsable == Model.EnumsModel.LicenceUsable.use).SingleOrDefault();

            if (chek != null )
            {
                if(licence != null)
                {
                    //#region Hiden
                    //SKGL.Validate validate = new SKGL.Validate();
                    //validate.secretPhase = secretPhase;
                    //validate.Key = licence.LicenceCode;
                    //#endregion
                    if (true)
                    {
                        //if (validate.DaysLeft > 1)//open dashbord if username and password correct
                        if (true)//open dashbord if username and password correct
                        {
                            Dashbord dsh = new Dashbord();

                            if (remmberCheckBox.Checked == true)
                            {
                                Properties.Settings.Default.username = com_userName.Text;
                                Properties.Settings.Default.password = txt_passWord.Text;
                                Properties.Settings.Default.Save();
                            }
                            else
                            {
                                Properties.Settings.Default.username = string.Empty;
                                Properties.Settings.Default.password = string.Empty;
                                Properties.Settings.Default.Save();
                            }

                            this.Hide();
                            dsh.Show();

                            notifyIcon1.Visible = true;

                            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                            notifyIcon1.BalloonTipText = "ورود با موفقیت";
                            notifyIcon1.BalloonTipTitle = "به برنامه خوش امدید";

                            notifyIcon1.ShowBalloonTip(1000);
                        }
                        //else if (validate.DaysLeft == 1)
                        else if (true)
                        {
                            Dashbord dsh = new Dashbord();

                            if (remmberCheckBox.Checked == true)
                            {
                                Properties.Settings.Default.username = com_userName.Text;
                                Properties.Settings.Default.password = txt_passWord.Text;
                                Properties.Settings.Default.Save();
                            }
                            else
                            {
                                Properties.Settings.Default.username = string.Empty;
                                Properties.Settings.Default.password = string.Empty;
                                Properties.Settings.Default.Save();
                            }

                            this.Hide();
                            dsh.Show();

                            Notification.notiShow("لایسنس شما کمتر از یک روز اعتبار دارد", "Login Form , Button 2", Notification.msgType.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("لایسنس غیر مجاز لطفا از لایسنس معتبر استفاده کنید یا با شماره 09208284051 تماس حاصل فرمایید", "خطا",
                            MessageBoxButtons.OK, MessageBoxIcon.Error
                                , MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    }
                }
                else
                {
                    MessageBox.Show("لایسنس غیر مجاز لطفا از لایسنس معتبر استفاده کنید یا با شماره 09208284051 تماس حاصل فرمایید", "خطا",
                            MessageBoxButtons.OK, MessageBoxIcon.Error
                                , MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
            }
            else
            {
                Notification.notiShow("نام کاربری یا رمز عبور اشتباه است","Login Form , Button 2", Notification.msgType.error);

                txt_passWord.ResetText();
            }
            

        }
        static string GetMd5Hash(MD5 md5Hash, string input)//converter
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)//minimize
        {
          this.WindowState =  FormWindowState.Minimized;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)//exit
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            AddNewLicence addnewli = new AddNewLicence();
            addnewli.ShowDialog();
        }

        private void txt_passWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                button2_Click(sender, e);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txt_passWord.Text) && remmberCheckBox.Checked == false)
            {
                txt_passWord.ResetText();
                txt_passWord.Focus();
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            var allLicence = liServ.GetAll();
            if(allLicence.Count != 0)
            {

                var curentLicence = liServ.GetAll().Where(x => x.licenceUsable == Model.EnumsModel.LicenceUsable.use).SingleOrDefault();
                if (curentLicence != null)
                {
                    //#region Hide

                    //SKGL.Validate validate = new SKGL.Validate();
                    //validate.secretPhase = secretPhase;
                    //validate.Key = curentLicence.LicenceCode;
                    //#endregion

                    if (Properties.Settings.Default.username != string.Empty)
                    {
                        remmberCheckBox.Checked = true;
                        com_userName.Text.Equals(Settings.Default.username);
                        txt_passWord.Text = Settings.Default.password;
                    }

                    //validate.

                    if (account.GetAll().Count > 1)
                    {
                        MessageBox.Show("به علت ویرایش در دیتابیس برنامه قفل شده است با شماره 09208284051 تماس حاصل فرمایید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error
                                        , MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);

                        groupBox1.Enabled = false;
                    }
                    //else if (validate.IsExpired || validate.DaysLeft <= 0)
                    //else if (true)
                    //{
                    //    MessageBox.Show("اعتبار برنامه به پایان رسیده است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error
                    //                    , MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);

                    //    AddNewLicence addnewli = new AddNewLicence();
                    //    addnewli.Exit.Enabled = false;
                    //    addnewli.ShowDialog();
                    //}
                }
                else
                {
                    AddNewLicence addNewLicence = new AddNewLicence();
                    addNewLicence.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("هیچ گونه لایسنس فعالی وجود ندارد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);

                AddNewLicence addNewLicence = new AddNewLicence();
                addNewLicence.ShowDialog();
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                txt_passWord.UseSystemPasswordChar = false;
            }
            else
            {
                txt_passWord.UseSystemPasswordChar = true;
            }
        }

        private void txt_passWord_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
