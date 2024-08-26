using System;
using Model;
using System.Drawing;
using System.Windows.Forms;
using Service;

namespace RealEstate
{
    public partial class CreateAccount : Form
    {
        AccountServ accountSvr = new AccountServ();
        LicenceServ licenceServ = new LicenceServ();
        private string secretPhase = new EncriptPassword().secretPhase();
        public CreateAccount()
        {
            InitializeComponent();
        }
        //////////////////////////////////////////////////////////

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
        //////////////////////////////////////////////////////////

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            LoginForm lf = new LoginForm();
            this.Hide();
            lf.ShowDialog();
            lf.com_userName.DataSource = accountSvr.GetAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(string.IsNullOrEmpty(txt_userName.Text) && string.IsNullOrEmpty(txt_password.Text) && string.IsNullOrEmpty(txt_Licence.Text))
            {
                MessageBox.Show("تمام فیلد ها را پر کنید","خطا",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    ,MessageBoxDefaultButton.Button1
                    ,MessageBoxOptions.RtlReading);
            }
            else
            {
                if (accountSvr.GetAll().Count == 0 )
                {
                    #region Hiden
                    SKGL.Validate validate = new SKGL.Validate();
                    validate.secretPhase = secretPhase;
                    validate.Key = txt_Licence.Text;
#endregion
                    if (validate.IsValid)
                    {

                        Licence li = new Licence()
                        {
                            LicenceCode = validate.Key,
                            Creation = validate.CreationDate,
                            DaysLeft = validate.DaysLeft,
                            Expire = validate.ExpireDate,
                            licenceType = EnumsModel.LicenceType.buyLicence,
                            licenceUsable = EnumsModel.LicenceUsable.use,
                            licencevalidation = EnumsModel.LicenceValidation.valid
                        };

                        Account acc = new Account()
                        {
                            UserName = txt_userName.Text,
                            PassWord = new EncriptPassword().encrypt(txt_password.Text),
                            showType = EnumsModel.showType.show,
                        };

                        var check = accountSvr.Add(acc);
                        var chekLi = licenceServ.Add(li);

                        if (check == true && chekLi == true)
                        {

                            var res = MessageBox.Show($"با موفقیت ذخیره شد مدت اعتبار حساب شما  :  {validate.DaysLeft} ", "پیام", MessageBoxButtons.OK
                                 , MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);

                            if (res == DialogResult.OK)
                            {
                                LoginForm lf = new LoginForm();
                                this.Hide();
                                lf.ShowDialog();
                                lf.com_userName.DataSource = accountSvr.GetAll();
                            }
                        }
                        else
                        {
                            MessageBox.Show("خطا در پردازش اطلاعات", "خطا",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                            , MessageBoxDefaultButton.Button1
                            , MessageBoxOptions.RtlReading);
                        }
                    }
                    else
                    {
                        MessageBox.Show("لایسنس نا معتبر است", "خطا",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                            , MessageBoxDefaultButton.Button1
                            , MessageBoxOptions.RtlReading);

                        txt_Licence.ResetText();
                        txt_password.ResetText();
                    }
                }
                
            }
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
