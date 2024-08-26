using Service;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Model;
using Stimulsoft.Report;

namespace RealEstate
{
    public partial class SettingForm : Form
    {
        AccountServ accServ = new AccountServ();
        public SettingForm()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            checkBox2.Checked = Properties.Settings.Default.ShowSlide;
            ShowPic.Checked = Properties.Settings.Default.ShowPic;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                txt_oldPass.UseSystemPasswordChar = false;
            }
            else
            {
                txt_oldPass.UseSystemPasswordChar = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txt_username.Text) && !string.IsNullOrEmpty(txt_oldPass.Text) &&
                !string.IsNullOrEmpty(txt_newPass.Text) && !string.IsNullOrEmpty(txt_repetPass.Text))
            {
                var checkUser = accServ.GetAll().Where(x => x.UserName.Equals(txt_username.Text)).SingleOrDefault();

                if(checkUser !=null)
                {

                    var checkPass = accServ.Chek(checkUser.UserName, new EncriptPassword().encrypt(txt_oldPass.Text));

                    if(checkPass != null)
                    {
                        if(txt_newPass.Text.Equals(txt_repetPass.Text))
                        {
                            Account acc = new Account()
                            {
                                Id = checkPass.Id,
                                PassWord = new EncriptPassword().encrypt(txt_newPass.Text)
                            };
                            var changePassRes = accServ.Edit(acc);

                            if( changePassRes == true)
                            {
                                Notification.notiShow("رمز عبور با موفقیت تغییر کرد",null, Notification.msgType.success);
                                txt_newPass.ResetText();
                                txt_oldPass.ResetText();
                                txt_repetPass.ResetText();
                                txt_username.ResetText();
                            }
                        }
                        else
                        {
                            Notification.notiShow("رمز و تکرار ان یکسان نیست","Setting , button 1", Notification.msgType.error);
                            txt_repetPass.ResetText();
                            txt_repetPass.Focus();
                        }
                    }
                    else
                    {
                        Notification.notiShow("رمز عبور قدیم اشتباه است", "Setting , button 1", Notification.msgType.error);
                        txt_oldPass.ResetText();
                        txt_oldPass.Focus();
                    }
                }
                else
                {
                    Notification.notiShow("نام کاربری اشتباه است", "Setting , button 1", Notification.msgType.error);
                    txt_username.ResetText();
                    txt_username.Focus();
                }
            }
        }
        private new void TextChanged(object sender, EventArgs e)
        {
            buttonControl();
        }
        void buttonControl()
        {
            if (txt_newPass.Text.Length >= 4 && txt_repetPass.Text.Length >= 4 && !string.IsNullOrEmpty(txt_username.Text) &&
                !string.IsNullOrEmpty(txt_oldPass.Text))
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowSlide = checkBox2.Checked;
            Properties.Settings.Default.Save();
        }

        private void ShowPic_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowPic = ShowPic.Checked;
            Properties.Settings.Default.Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                StiReport sti = new StiReport();
                sti.Load(Application.StartupPath + "/Reports/BillFullReport.mrt");
                sti.Design();
            }
            catch (Exception ext)
            {
                MessageBox.Show($"{ext.Message}");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                StiReport sti = new StiReport();
                sti.Load(Application.StartupPath + "/Reports/SingleEstateReport.mrt");
                sti.Design();
            }
            catch (Exception ext)
            {
                MessageBox.Show($"{ext.Message}");
            }
            
        }
    }
}
