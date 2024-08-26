using Service;
using System;
using System.Data;
using System.Linq;
using Model;
using System.Windows.Forms;

namespace RealEstate
{
    public partial class AddNewLicence : Form
    {
        #region secretPhase
        private string _secretPhase = new EncriptPassword().secretPhase();
        #endregion
        private readonly LicenceServ liServ = new LicenceServ();
        public AddNewLicence()
        {
            InitializeComponent();

            foreach (var item in liServ.GetAll())
            {
                if(CheckExpireLicence(item.LicenceCode))
                {
                    liServ.EditExpire(item.id);
                }
            }
        }

        private void btn_submitli_Click(object sender, EventArgs e)
        {

            string inputValidLicence = null;
            string inputCrackLicence = null;

            if(!string.IsNullOrEmpty(txt_licence.Text))
            {
                if(CheckLicenceIsValid(txt_licence.Text))
                {
                    inputValidLicence = txt_licence.Text;
                }
                else
                {
                    inputCrackLicence = txt_licence.Text;
                }
                
                if(inputValidLicence != null && inputCrackLicence == null)
                {
                    
                    var usedLicence = liServ.GetAll().Where(x => x.licenceUsable == Model.EnumsModel.LicenceUsable.use).ToList();
                    if(usedLicence.Count <= 1)
                    {
                        int days = 0;

                        foreach (var item in liServ.GetAll().Where(x => x.licencevalidation == EnumsModel.LicenceValidation.valid && x.licenceUsable == EnumsModel.LicenceUsable.use).ToList())
                        {
                            days += checkDaysLeft(item.LicenceCode);
                        }
                        
                        foreach (var item in usedLicence)
                        {
                            liServ.EditUsable(item.id);
                        }

                        var appGenerate = licenceForSave(genrateLicence(inputValidLicence,days+licenceForSave(inputValidLicence).DaysLeft).Key);

                        Licence inputLi = new Licence()
                        {
                            Creation = appGenerate.Creation,
                            DaysLeft = appGenerate.DaysLeft,
                            Expire = appGenerate.Expire,
                            LicenceCode = appGenerate.LicenceCode,
                            licenceType = EnumsModel.LicenceType.appGenerate,
                            licenceUsable = EnumsModel.LicenceUsable.use,
                            licencevalidation = EnumsModel.LicenceValidation.valid,

                            Type = EnumsModel.LicenceType.appGenerate.ToString(),
                            Validation = EnumsModel.LicenceValidation.valid.ToString()
                        };

                        var saveLicenceRes = liServ.Add(inputLi);

                        if(saveLicenceRes)
                        {
                            txt_licence.ResetText();

                            MessageBox.Show($"مدت اعتبار برنامه به  {inputLi.DaysLeft} روز افزایش یافت.", "پیام", MessageBoxButtons.OK,
                                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
                                            MessageBoxOptions.RtlReading);
                            if(Application.OpenForms.OfType<LoginForm>().Count() == 1)
                            {
                                this.Hide();
                            }
                            else
                            {
                                foreach (var item in Application.OpenForms.OfType<LoginForm>().ToList())
                                {
                                    item.Close();
                                } 
                                this.Hide();
                                LoginForm lgf = new LoginForm();
                                lgf.Show();
                            }
                            
                        }
                        else
                        {
                            Notification.notiShow("خطا در ادغام لایسنس ها", "Add New Licence ,Btn Submit Combine Licence", Notification.msgType.error);
                            txt_licence.ResetText();
                        }
                    }
                    else
                    {
                        var buyLicence = licenceForSave(inputValidLicence);

                        Licence buyLi = new Licence()
                        {
                            Creation = buyLicence.Creation,
                            DaysLeft = buyLicence.DaysLeft,
                            Expire = buyLicence.Expire,
                            LicenceCode = buyLicence.LicenceCode,
                            licenceType = EnumsModel.LicenceType.buyLicence,
                            licenceUsable = EnumsModel.LicenceUsable.use,
                            licencevalidation = EnumsModel.LicenceValidation.valid,
                            Validation = EnumsModel.LicenceValidation.valid.ToString(),
                            Type = EnumsModel.LicenceType.buyLicence.ToString()
                        };

                        var validLicenceSaveRes = liServ.Add(buyLi);

                        if(validLicenceSaveRes)
                        {
                            txt_licence.ResetText();
                            Notification.notiShow("لایسنس جدید ثبت شد", "Add New Licence , Add Licence Else", Notification.msgType.success);
                            this.Hide();
                            LoginForm lgf = new LoginForm();
                            lgf.Show();
                        }
                        else
                        {
                            Notification.notiShow("خطا در ثبت لایسنس جدید", "Add New Licence , Add Licence Else / else", Notification.msgType.error);
                            txt_licence.ResetText();
                        }
                    }
                }
                else
                {
                    txt_licence.ResetText();
                    MessageBox.Show("از ثبت لایسنس غیر مجاز خود داری کنید !"," لایسنس غیر مجاز !",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RtlReading);
                    
                }
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<LoginForm>().Count() == 1)
            {
                this.Close();
            }
            else
            {
                var res = MessageBox.Show("آیا میخواهید وارد برنامه شوید ؟", "پیام", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                if(res == DialogResult.Yes)
                {
                    foreach (var item in Application.OpenForms.OfType<LoginForm>().ToList())
                    {
                        item.Close();
                    }
                    this.Hide();
                    LoginForm lgf = new LoginForm();
                    lgf.Show();
                }
                else
                {
                    Application.Exit();
                }
                
            }
        }

        private void txt_licence_TextChanged(object sender, EventArgs e)
        {
            if(txt_licence.Text.Length == 23)
            {
                btn_submitli.Enabled = true;
            }
            else
            {
                btn_submitli.Enabled = false;
            }
        }
        private bool CheckLicenceIsValid(string liInput)
        {
            SKGL.Validate valid = new SKGL.Validate();
            valid.secretPhase = _secretPhase;
            valid.Key = liInput;

            if(valid.IsValid && !valid.IsExpired)
            {
                return true;
            }
            return false;
        }
        private Licence licenceForSave(string liInput)
        {
            SKGL.Validate valid = new SKGL.Validate();
            valid.secretPhase = _secretPhase;
            valid.Key = liInput;

            Licence li =new Licence();
            li.Creation = valid.CreationDate;
            li.Expire = valid.ExpireDate;
            li.DaysLeft = valid.DaysLeft;
            li.LicenceCode = valid.Key;

            return li;
        }
        public bool CheckExpireLicence(string liInput)
        {
            SKGL.Validate valid = new SKGL.Validate();
            valid.secretPhase = _secretPhase;
            valid.Key = liInput;

            if (valid.IsExpired)
            {
                return true;
            }
            return false;
        }
        private int checkDaysLeft(string liInput)
        {
            int sum = 0;
            SKGL.Validate valid = new SKGL.Validate();
            valid.secretPhase = _secretPhase;
            valid.Key = liInput;
            if(valid.DaysLeft > 0)
            {
                sum = valid.DaysLeft;
            }
            return sum;
        }
        private SKGL.Generate genrateLicence(string liInput, int Days)
        {
            SKGL.Generate genrate = new SKGL.Generate();
            genrate.Key = liInput;
            genrate.secretPhase = _secretPhase;
            genrate.doKey(Days);

            return genrate;
        }
    }
}
