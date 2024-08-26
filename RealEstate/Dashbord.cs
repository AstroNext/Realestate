using System;
using Service;
using Model;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace RealEstate
{
    public partial class Dashbord : Form
    {
        EstateServ estateserv = new EstateServ();
        EstateTypeServ estateTypeServ = new EstateTypeServ();
        SaleTypeServ saleTypeServ = new SaleTypeServ();
        AreaServ areaServ = new AreaServ();
        public Dashbord()
        {
            InitializeComponent();
        }
        ////////////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////////////
        //exprot to exel
        ////////////////////////////////////////////////////////////////////////////////
        List<Estate> dataItem = new List<Estate>();
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
                return;

            if( dataItem.Count != 0)
            {
                for (int i = 0; i < dataItem.Count; i++)
                {
                    dataItem.RemoveRange(i, dataItem.Count);
                }
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                var found = estateserv.GetAll().Where(x => x.Id == Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value)).SingleOrDefault();
                if(found !=null)
                {
                    dataItem.Add(found);
                }
            }
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Exel Wrokbook|*.xls" })
            {
                if(sfd.ShowDialog() == DialogResult.OK)
                {
                    _inputPrameter.fileName = sfd.FileName;
                    _inputPrameter.estateList = dataItem as List<Estate>;
                    progressBar.Minimum = 0;
                    progressBar.Value = 0;
                    backgroundWorker.RunWorkerAsync(_inputPrameter);
                }
            }
        }
        ////////////////////////////////////////////////////////////////////////////////
        private void toolStripButton1_Click(object sender, EventArgs e)//exit
        {

            var res = MessageBox.Show("آیا میخواهید از برنامه خارج شوید ؟", "پیام", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                                        MessageBoxOptions.RtlReading);
            if(res == DialogResult.Yes)
            {
                if (Application.OpenForms.OfType<Add>().Count() == 1)
                {
                    Application.OpenForms.OfType<Add>().First().Close();
                    new LoginForm().notifyIcon1.Visible = false;
                    Application.Exit();
                }
                else if (Application.OpenForms.OfType<ForSelectEstate>().Count() == 1)
                {
                    Application.OpenForms.OfType<ForSelectEstate>().First().Close();
                    new LoginForm().notifyIcon1.Visible = false;
                    Application.Exit();
                }
                else if (Application.OpenForms.OfType<Information>().Count() == 1)
                {
                    Application.OpenForms.OfType<Information>().First().Close();
                    new LoginForm().notifyIcon1.Visible = false;
                    Application.Exit();
                }
                else if (Application.OpenForms.OfType<EstateDetail>().Count() == 1)
                {
                    Application.OpenForms.OfType<EstateDetail>().First().Close();
                    new LoginForm().notifyIcon1.Visible = false;
                    Application.Exit();
                }
                else if (Application.OpenForms.OfType<RecoveryForm>().Count() == 1)
                {
                    Application.OpenForms.OfType<RecoveryForm>().First().Close();
                    new LoginForm().notifyIcon1.Visible = false;
                    Application.Exit();
                }
                else
                {
                    new LoginForm().notifyIcon1.Visible = false;
                    Application.Exit();
                }
                
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)//minimize
        {
            WindowState = FormWindowState.Minimized;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)//log out
        {
            var res = MessageBox.Show("آیا میخواهید از حساب کاربری خود خارج شوید ؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            if(res == DialogResult.Yes)
            {
                if (Application.OpenForms.OfType<Add>().Count() == 1)
                {
                    Application.OpenForms.OfType<Add>().First().Close();
                    showLogin();
                }
                else if (Application.OpenForms.OfType<ForSelectEstate>().Count() == 1)
                {
                    Application.OpenForms.OfType<ForSelectEstate>().First().Close();
                    showLogin();
                }
                else if (Application.OpenForms.OfType<Information>().Count() == 1)
                {
                    Application.OpenForms.OfType<Information>().First().Close();
                    showLogin();
                }
                else if (Application.OpenForms.OfType<EstateDetail>().Count() == 1)
                {
                    Application.OpenForms.OfType<EstateDetail>().First().Close();
                    showLogin();
                }
                else if (Application.OpenForms.OfType<RecoveryForm>().Count() == 1)
                {
                    Application.OpenForms.OfType<RecoveryForm>().First().Close();
                    showLogin();
                }
                else
                {
                    showLogin();
                }
            }
        }
        void showLogin()// login form
        {
            this.Hide();
            LoginForm lf = new LoginForm();
            lf.ShowDialog();
        }
        TextBox tx = new TextBox();

        private void ChangePic_Tick(object sender , EventArgs e)
        {
            
        }
        private new void KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (ch == 46 && tx.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(com_SaleType.Text) || string.IsNullOrEmpty(com_estateType.Text) ||
                string.IsNullOrEmpty(com_AreaType.Text))
            {
                MessageBox.Show("لطفا سه فیلد بالا را به طور صحیح انتخاب کنید .", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            else
            {
                com_AreaType.Enabled = false;
                com_estateType.Enabled = false;
                com_SaleType.Enabled = false;
                btnSubmit.Enabled = false;
                btnchangeInfromation.Enabled = true;
                btnAddEstate.Enabled = true;
                adressGB.Enabled = true;
                OwnerAndPayGB.Enabled = true;
                estateDetailGB.Enabled = true;
                SearchGB.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OwnerAndPayGB.Enabled = false;
            firstInformationGB.Enabled = true;
            estateDetailGB.Enabled = false;
            adressGB.Enabled = false;
            firstInformationGB.Enabled = true;
            com_AreaType.Enabled = true;
            com_estateType.Enabled = true;
            com_SaleType.Enabled = true;
            btnSubmit.Enabled = true;
            btnchangeInfromation.Enabled = false;
        }
        public EnumsModel.Direction CheckDirection(int index)
        {
            EnumsModel.Direction result = EnumsModel.Direction.Empty;
            switch (index)
            {
                case 1:
                    result = EnumsModel.Direction.shomal;
                    break;
                case 2:
                    result =  EnumsModel.Direction.jonob;
                    break;
                case 3:
                    result = EnumsModel.Direction.shargh;
                    break;
                case 4:
                    result = EnumsModel.Direction.gharb;
                    break;
            }
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Int32.TryParse(txt_id.Text, out int curentID);

            var foundEst = estateserv.GetAll().Where(x => x.Id == curentID).SingleOrDefault();

            if(string.IsNullOrEmpty(txt_id.Text) && foundEst == null)
            {
                int.TryParse(txt_mastercount.Text, out int masteCount);
                int.TryParse(txt_roomcount.Text, out int roomCount);

                if (masteCount <= roomCount && !string.IsNullOrEmpty(txt_street.Text) && !string.IsNullOrEmpty(txt_alley.Text) && !string.IsNullOrEmpty(txt_name.Text) &&
                    !string.IsNullOrEmpty(txt_family.Text) && !string.IsNullOrEmpty(txt_phonenumber.Text))
                {

                    var res = MessageBox.Show("آیا میخواهید اطلاعات ملک جدید ذخیره شود ؟", "پیام", MessageBoxButtons.OKCancel
                        , MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);

                    if (res == DialogResult.OK)
                    {
                        Int32.TryParse(txt_unit.Text, out int unit);
                        Int32.TryParse(txt_postcode.Text, out int postCode);
                        Int32.TryParse(txt_floor.Text, out int floor);

                        Adress newAdress = new Adress()
                        {
                            Street = txt_street.Text,
                            Unit = unit,
                            Alley = txt_alley.Text,
                            EstateName = txt_estateName.Text,
                            Postcode = postCode,
                            Floor = floor
                        };

                        var foundEstateType = estateTypeServ.GetAll().Where(item => item.Name == com_estateType.Text).FirstOrDefault();
                        var foundSaleType = saleTypeServ.GetAll().Where(item => item.Name == com_SaleType.Text).FirstOrDefault();
                        var foundArea = areaServ.GetAll().Where(item => item.Name == com_AreaType.Text).FirstOrDefault();

                        if (foundEstateType != null && foundSaleType != null && foundArea != null)
                        {

                            Int32.TryParse(txt_buildyear.Text, out int buildYear);
                            Int32.TryParse(txt_mastercount.Text, out int masterRoom);


                            Model.EstateDetail newEstateDetail = new Model.EstateDetail()
                            {
                                BuildYear = buildYear,
                                KeySale = keySale.Checked,
                                MasterRoomCount = masterRoom,
                                RoomCount = roomCount,
                                TheArea = txt_thearea.Text,
                                Direction = CheckDirection(ComDirection.SelectedIndex),
                            };

                            Owner newOwner = new Owner()
                            {
                                Family = txt_family.Text,
                                Name = txt_name.Text,
                                Phonenumber = txt_phonenumber.Text,
                                Info = txt_ownerinfo.Text,
                            };

                            Estate newEstate = new Estate()
                            {
                                Adress = newAdress,
                                Area = foundArea,
                                EstateDetail = newEstateDetail,
                                EstateType = foundEstateType,
                                Owner = newOwner,
                                SaleType = foundSaleType,
                                Show = EnumsModel.showType.show,
                                Sale = EnumsModel.Sele.notsale,
                                EjareDeposit = ejare.Value,
                                RahnDeposit = rahn.Value,
                                SaleDeposit = Sale.Value,
                                Info = txt_estateinfo.Text,
                            };

                            var check = estateserv.Add(newEstate);

                            if (check == true)
                            {
                                Notification.notiShow(" با موفقیت ذخیره شد .",null , Notification.msgType.success);
                                estateBinding(estateserv.GetAll());

                                resetTexts();

                                com_AreaType.Enabled = true;
                                com_estateType.Enabled = true;
                                com_SaleType.Enabled = true;
                                btnSubmit.Enabled = true;
                                btnchangeInfromation.Enabled = false;
                                btnAddEstate.Enabled = false;
                                adressGB.Enabled = false;
                                estateDetailGB.Enabled = false;
                                OwnerAndPayGB.Enabled = false;

                            }
                            else
                            {
                                Notification.notiShow("خطا در ذخیره اطلاعات ملک جدید","Dashboard , Add new Estate", Notification.msgType.error);
                            }
                        }
                        else
                        {
                            Notification.notiShow("خطا در جستجو ملک ، منطقه یا نوع پرداخت", "Dashboard , Add new Estate", Notification.msgType.error);
                        }
                    }
                    else
                    {
                        resetTexts();
                    }
                }
                else
                {
                    Notification.notiShow("لطفا فیلدهای ستاره دار را کامل کنید در صورت کامل بودن تعداد خواب ها را با تعداد مستر کنترل کنید", "Dashboard , Add new Estate", Notification.msgType.error);
                }
            }
            else if(!string.IsNullOrEmpty(txt_id.Text) && foundEst !=null)
            {
                int.TryParse(txt_mastercount.Text, out int masteCount);
                int.TryParse(txt_roomcount.Text, out int roomCount);

                if (masteCount <= roomCount && !string.IsNullOrEmpty(txt_street.Text) && !string.IsNullOrEmpty(txt_alley.Text) && !string.IsNullOrEmpty(txt_name.Text) &&
                    !string.IsNullOrEmpty(txt_family.Text) && !string.IsNullOrEmpty(txt_phonenumber.Text))
                {

                    var res = MessageBox.Show("آیا میخواهید اطلاعات ملک ویرایش شود ؟", "پیام", MessageBoxButtons.OKCancel
                        , MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);

                    if (res == DialogResult.OK)
                    {
                        Int32.TryParse(txt_unit.Text, out int unit);
                        Int32.TryParse(txt_postcode.Text, out int postCode);
                        Int32.TryParse(txt_floor.Text, out int floor);
                        Adress newAdress = new Adress()
                        {
                            Street = txt_street.Text,
                            Unit = unit,
                            Alley = txt_alley.Text,
                            EstateName = txt_estateName.Text,
                            Postcode = postCode,
                            Floor = floor
                        };

                        var foundEstateType = estateTypeServ.GetAll().Where(item => item.Name == com_estateType.Text).FirstOrDefault();
                        var foundSaleType = saleTypeServ.GetAll().Where(item => item.Name == com_SaleType.Text).FirstOrDefault();
                        var foundArea = areaServ.GetAll().Where(item => item.Name == com_AreaType.Text).FirstOrDefault();
                        if (foundEstateType != null && foundSaleType != null && foundArea != null)
                        {

                            Int32.TryParse(txt_buildyear.Text, out int buildYear);
                            Int32.TryParse(txt_mastercount.Text, out int masterRoom);


                            Model.EstateDetail newEstateDetail = new Model.EstateDetail()
                            {
                                BuildYear = buildYear,
                                KeySale = keySale.Checked,
                                MasterRoomCount = masterRoom,
                                RoomCount = roomCount,
                                TheArea = txt_thearea.Text,
                                Direction = CheckDirection(ComDirection.SelectedIndex),
                            };

                            Owner newOwner = new Owner()
                            {
                                Family = txt_family.Text,
                                Name = txt_name.Text,
                                Phonenumber = txt_phonenumber.Text,
                                Info = txt_ownerinfo.Text,
                            };
                            Estate newEstate = new Estate()
                            {
                                Id = foundEst.Id,
                                Adress = newAdress,
                                Area = foundArea,
                                EstateDetail = newEstateDetail,
                                EstateType = foundEstateType,
                                Owner = newOwner,
                                SaleType = foundSaleType,
                                Show = EnumsModel.showType.show,
                                Sale = EnumsModel.Sele.notsale,
                                EjareDeposit = ejare.Value,
                                RahnDeposit = rahn.Value,
                                SaleDeposit = Sale.Value,
                                Info = txt_estateinfo.Text
                                
                            };

                            var check = estateserv.Edit(newEstate);

                            if (check == true)
                            {
                                Notification.notiShow(" با موفقیت ویرایش شد .",null , Notification.msgType.success);
                                estateBinding(estateserv.GetAll());

                                resetTexts();

                                txt_search.Enabled = true;
                                DGEstates.Enabled = true;
                                com_AreaType.Enabled = true;
                                com_estateType.Enabled = true;
                                com_SaleType.Enabled = true;
                                btnSubmit.Enabled = true;
                                btnchangeInfromation.Enabled = false;
                                btnAddEstate.Enabled = false;
                                adressGB.Enabled = false;
                                estateDetailGB.Enabled = false;
                                OwnerAndPayGB.Enabled = false;
                            }
                            else
                            {
                                Notification.notiShow("خطا در ذخیره اطلاعات ملک ویرایش شده .", "Dashboard , Edit Estate", Notification.msgType.success);
                            }
                        }
                        else
                        {
                            Notification.notiShow("خطا در جستجو ملک ، منطقه یا نوع پرداخت", "Dashboard , Edit Estate", Notification.msgType.error);
                        }
                    }
                    else
                    {
                        resetTexts();
                    }
                }
                else
                {
                    Notification.notiShow("لطفا فیلد های ستاره دار را کامل کنید در صورت کامل بودن تعداد خواب ها را با تعداد مستر کنترل کنید", "Dashboard , Edit Estate", Notification.msgType.error);
                }
            }
            
        }

        private void com_searchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(com_searchBy.SelectedIndex == 0)
            {
                numericUpDown1.Visible = false;
                com_detail.Visible = false;
                comlowup.Visible = false;

                ShowItems(estateserv.GetAll().ToList());
            }
            else if (com_searchBy.SelectedIndex == 1)
            {
                numericUpDown1.Visible = false;
                com_detail.Visible = false ;
                comlowup.Visible = false;

                ShowItems(estateserv.GetAll().Where(x => x.Sale == EnumsModel.Sele.sale).ToList());
            }
            else if (com_searchBy.SelectedIndex == 2)
            {
                numericUpDown1.Visible = false;
                com_detail.Visible = true;
                comlowup.Visible = false;

                com_detail.DataSource = estateTypeServ.GetAll().Where(x => x.ShowType == EnumsModel.showType.show).ToList();
            }
            else if (com_searchBy.SelectedIndex == 3)
            {
                numericUpDown1.Visible = false;
                com_detail.Visible = true;
                comlowup.Visible = false;

                com_detail.DataSource = saleTypeServ.GetAll().Where(x => x.ShowType == EnumsModel.showType.show).ToList();
            }
            else if (com_searchBy.SelectedIndex == 4)
            {
                numericUpDown1.Visible = false;
                com_detail.Visible = true;
                comlowup.Visible = false;

                com_detail.DataSource = areaServ.GetAll().Where(x => x.ShowType == EnumsModel.showType.show).ToList();
            }
            else if (com_searchBy.SelectedIndex == 5)
            {
                numericUpDown1.Visible = true;
                comlowup.Visible = true;
                numericUpDown1.Value = 0;
                com_detail.Visible = false;
                comlowup.SelectedIndex = 0;
            }
            else if (com_searchBy.SelectedIndex == 6)
            {
                numericUpDown1.Visible = true;
                numericUpDown1.Value = 0;
                com_detail.Visible = false;
                comlowup.Visible = true;
                comlowup.SelectedIndex = 0;
            }
            else if (com_searchBy.SelectedIndex == 7)
            {
                
                numericUpDown1.Visible = true;
                numericUpDown1.Value = 0;
                com_detail.Visible = false;
                comlowup.Visible = true;
                comlowup.SelectedIndex = 0;
            }
            else if (com_searchBy.SelectedIndex == 8)
            {
                numericUpDown1.Visible = false;
                com_detail.Visible = false;
                comlowup.Visible = false;
                ShowItems(estateserv.GetAll().Where(x => x.Show == EnumsModel.showType.hide).ToList());
            }

        }
        private void com_detail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (com_searchBy.SelectedIndex == 2  )
            {
                ShowItems(estateserv.GetAll().Where(x => x.EstateType.Name == com_detail.Text).ToList());
            }
            else if(com_searchBy.SelectedIndex == 3)
            {
                ShowItems(estateserv.GetAll().Where(x => x.SaleType.Name == com_detail.Text).ToList());
            }
            else if(com_searchBy.SelectedIndex == 4)
            {
                ShowItems(estateserv.GetAll().Where(x => x.Area.Name == com_detail.Text).ToList());
            }
        }
        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if( e.KeyChar == (char)13)
            {
                if (com_searchBy.SelectedIndex == 5 && comlowup.SelectedIndex == 0)
                {
                    ShowItems(estateserv.GetAll().Where(x => x.EjareDeposit < numericUpDown1.Value && x.Show == EnumsModel.showType.show).ToList());
                }
                else if (com_searchBy.SelectedIndex == 5 && comlowup.SelectedIndex == 1)
                {
                    ShowItems(estateserv.GetAll().Where(x => x.EjareDeposit > numericUpDown1.Value && x.Show == EnumsModel.showType.show).ToList());
                }
                else if (com_searchBy.SelectedIndex == 6 && comlowup.SelectedIndex == 0)
                {
                    ShowItems(estateserv.GetAll().Where(x => x.RahnDeposit < numericUpDown1.Value && x.Show == EnumsModel.showType.show).ToList());
                }
                else if (com_searchBy.SelectedIndex == 6 && comlowup.SelectedIndex == 1)
                {
                    ShowItems(estateserv.GetAll().Where(x => x.RahnDeposit > numericUpDown1.Value && x.Show == EnumsModel.showType.show).ToList());
                }
                else if (com_searchBy.SelectedIndex == 7 && comlowup.SelectedIndex == 0)
                {
                    ShowItems(estateserv.GetAll().Where(x => x.SaleDeposit < numericUpDown1.Value && x.Show == EnumsModel.showType.show).ToList());
                }
                else if (com_searchBy.SelectedIndex == 7 && comlowup.SelectedIndex == 1)
                {
                    ShowItems(estateserv.GetAll().Where(x => x.SaleDeposit > numericUpDown1.Value && x.Show == EnumsModel.showType.show).ToList());
                }
            }
        }
        public void ShowItems(List<Estate> Items)
        {
            if(Items.Count != 0)
            {
                dataGridView1.Rows.Clear();
                foreach (var item in Items)
                {
                    try
                    {
                        dataGridView1.Rows.Add(item.Id, $" خیابان {item.Adress.Street} / کوچه {item.Adress.Alley} / نام ساختمان {item.Adress.EstateName}"
                            , item.Area.Name, item.SaleType.Name, item.EstateType.Name, item.EstateDetail.TheArea,
                            item.EjareDeposit, item.RahnDeposit, item.SaleDeposit);
                    }
                    catch (Exception ext)
                    {

                        Notification.notiShow(ext.Message, "Dashboard , Show Items", Notification.msgType.error);
                    }
                }
            }
            else
            {
                dataGridView1.Rows.Clear();
            }
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int.TryParse(dataGridView1.CurrentRow.Cells[0].Value.ToString() , out int curentId);

                var item = (from a in estateserv.GetAll()
                            where a.Id == curentId
                            select a).SingleOrDefault();

                if (item != null)
                {
                    
                    if (Application.OpenForms.OfType<EstateDetail>().Count() == 1)
                    {
                        Application.OpenForms.OfType<EstateDetail>().First().Close();
                        showDetail(curentId);
                    }
                    else
                    {
                        showDetail(curentId);
                    }
                }
                else
                {
                    Notification.notiShow(" خطا در جستجو ملک","Dashboar , GridView Double Click", Notification.msgType.error);
                }
            }
        }
        void showDetail(int id)
        {
            EstateDetail eD = new EstateDetail();
            eD.curentItem(id);
            eD.Show();
        }


        private void checkBoxrahn_CheckedChanged(object sender, EventArgs e)
        {
           
            if(checkBoxrahn.Checked == true)
            {
                rahn.Enabled = true;
                rahn.Value = 0;
            }
            else
            {
                rahn.Enabled = false;
                rahn.Value = 0;
            }
        }

        private void checkBoxejare_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxejare.Checked == true)
            {
                ejare.Enabled = true;
                ejare.Value = 0;
            }
            else
            {
                ejare.Enabled = false;
                ejare.Value = 0;
            }
        }
        void estateBinding(List<Estate> estates)
        {
            DGEstates.Rows.Clear();
            if (estates.Count != 0)
            {
                foreach (var item in estates)
                {
                    try
                    {
                        DGEstates.Rows.Add(item.Id,
                                                $"خیابان {item.Adress.Street.ToString()} " +
                                                $"/ کوچه {item.Adress.Alley.ToString()} " +
                                                $"/ ساختمان {item.Adress.EstateName.ToString()} " +
                                                $"/ طبقه {item.Adress.Floor.ToString()} " +
                                                $"/ واحد {item.Adress.Unit.ToString()} " +
                                                $"/ کدپستی {item.Adress.Postcode.ToString()}",
                                                $"{item.Sale}",
                                                item.Area.Name,
                                                item.EstateType.Name,
                                                item.SaleType.Name,
                                                $"{item.Owner.Name} {item.Owner.Family}",
                                                item.Owner.Phonenumber,
                                                item.RahnDeposit,
                                                item.EjareDeposit,
                                                item.SaleDeposit);

                    }
                    catch (Exception ext)
                    {
                        Notification.notiShow(ext.Message,"Dashboard , Estate Binding", Notification.msgType.error);
                    }
                }
            }
        }
        
        private void btnArea_Click(object sender, EventArgs e) //area type
        {
            movePanelColor(btnArea);

            if (Application.OpenForms.OfType<Add>().Count() == 1)
            {
                Application.OpenForms.OfType<Add>().First().Close();
                openArea();
            }
            else if (Application.OpenForms.OfType<ForSelectEstate>().Count() == 1)
            {
                Application.OpenForms.OfType<ForSelectEstate>().First().Close();
                openArea();
            }
            else if (Application.OpenForms.OfType<Information>().Count() == 1)
            {
                Application.OpenForms.OfType<Information>().First().Close();
                openArea();
            }
            else if(Application.OpenForms.OfType<EstateDetail>().Count() == 1)
            {
                Application.OpenForms.OfType<EstateDetail>().First().Close();
                openArea();
            }
            else if (Application.OpenForms.OfType<RecoveryForm>().Count() == 1)
            {
                Application.OpenForms.OfType<RecoveryForm>().First().Close();
                openArea();
            }
            else
            {
                openArea();
            }
        }

        void openArea()
        {
            Add addArea = new Add(Add.AddType.AreaType);
            
            pnl_add.Visible = false;
            panel_List.Visible = false;
            
            addArea.Show();
        }
        private void btnEstate_Click(object sender, EventArgs e) //estate type
        {
            movePanelColor(btnEstate);

            if (Application.OpenForms.OfType<Add>().Count() == 1 )
            {
                Application.OpenForms.OfType<Add>().First().Close();
                openEstateType();
            }
            else if (Application.OpenForms.OfType<ForSelectEstate>().Count() == 1)
            {

                Application.OpenForms.OfType<ForSelectEstate>().First().Close();
                openEstateType();
            }
            else if ( Application.OpenForms.OfType<Information>().Count() == 1)
            {

                Application.OpenForms.OfType<Information>().First().Close();
                openEstateType();
            }

            else if (Application.OpenForms.OfType<EstateDetail>().Count() == 1)
            {
                Application.OpenForms.OfType<EstateDetail>().First().Close();
                openEstateType();
            }
            else if (Application.OpenForms.OfType<RecoveryForm>().Count() == 1)
            {
                Application.OpenForms.OfType<RecoveryForm>().First().Close();
                openEstateType();
            }
            else
            {
                openEstateType();
            }
        }
        void openEstateType()
        {
            Add addEstateType = new Add(Add.AddType.EstateType);

            pnl_add.Visible = false;
            panel_List.Visible = false;
            addEstateType.Show();
        }

        private void btnPay_Click(object sender, EventArgs e) //sale type
        {
            movePanelColor(btnPay);

            if (Application.OpenForms.OfType<Add>().Count() == 1)
            {
                Application.OpenForms.OfType<Add>().First().Close();
                openPay();
            }
            else if (Application.OpenForms.OfType<ForSelectEstate>().Count() == 1)
            {
                Application.OpenForms.OfType<ForSelectEstate>().First().Close();
                openPay();
            }
            else if (Application.OpenForms.OfType<Information>().Count() == 1)
            {
                Application.OpenForms.OfType<Information>().First().Close();
                openPay();
            }
            else if (Application.OpenForms.OfType<EstateDetail>().Count() == 1)
            {
                Application.OpenForms.OfType<EstateDetail>().First().Close();
                openPay();
            }
            else if (Application.OpenForms.OfType<RecoveryForm>().Count() == 1)
            {
                Application.OpenForms.OfType<RecoveryForm>().First().Close();
                openPay();
            }
            else
            {
                openPay();
            }
        }
        void openPay()//pautype
        {
            Add addSaleType = new Add(Add.AddType.SaleType);

            pnl_add.Visible = false;
            panel_List.Visible = false;
            addSaleType.Show();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            movePanelColor(btnNew);

            if (Application.OpenForms.OfType<Add>().Count() == 1)
            {
                Application.OpenForms.OfType<Add>().First().Close();
                openPanelNewEstate();
            }
            else if (Application.OpenForms.OfType<ForSelectEstate>().Count() == 1)
            {
                Application.OpenForms.OfType<ForSelectEstate>().First().Close();
                openPanelNewEstate();
            }
            else if (Application.OpenForms.OfType<Information>().Count() == 1)
            {
                Application.OpenForms.OfType<Information>().First().Close();
                openPanelNewEstate();
            }
            else if (Application.OpenForms.OfType<EstateDetail>().Count() == 1)
            {
                Application.OpenForms.OfType<EstateDetail>().First().Close();
                openPanelNewEstate();
            }
            else if (Application.OpenForms.OfType<RecoveryForm>().Count() == 1)
            {
                Application.OpenForms.OfType<RecoveryForm>().First().Close();
                openPanelNewEstate();
            }
            else
            {
                openPanelNewEstate();
            }
        }
        void openPanelNewEstate()
        {
            if (areaServ.GetAll().Where(x => x.ShowType == EnumsModel.showType.show).ToList().Count != 0 &&
                estateTypeServ.GetAll().Where(x => x.ShowType == EnumsModel.showType.show).ToList().Count != 0 &&
                saleTypeServ.GetAll().Where(x => x.ShowType == EnumsModel.showType.show).ToList().Count != 0)
            {
                if(pnl_add.Visible == false)
                {
                    pictureBox2.Visible = false;
                    panel_List.Visible = false;
                    pnl_add.Visible = true;

                    resetTexts();

                    DGEstates.Enabled = true;
                    adressGB.Enabled = false;
                    estateDetailGB.Enabled = false;
                    OwnerAndPayGB.Enabled = false;
                    btnchangeInfromation.Enabled = false;
                    btnSubmit.Enabled = true;

                    estateBinding(estateserv.GetAll().Where(x => x.Show == EnumsModel.showType.show).ToList());
                    comBinding();
                }
                else
                {
                    pictureBox2.Visible = true;
                    pnl_add.Visible = false;
                }
                
            }
            else
            {
                Notification.notiShow("نوع ملک ، نوع پرداخت یا منطقه ای ثبت نشده است","Dasboard , Open Panel New Estate", Notification.msgType.error);
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)//gozaresh
        {
            movePanelColor(btnSearch);
            
            if (Application.OpenForms.OfType<Add>().Count() == 1)
            {
                Application.OpenForms.OfType<Add>().First().Close();
                openReportList();
            }
            else if (Application.OpenForms.OfType<ForSelectEstate>().Count() == 1)
            {
                Application.OpenForms.OfType<ForSelectEstate>().First().Close();
                openReportList();
            }
            else if (Application.OpenForms.OfType<Information>().Count() == 1)
            {
                Application.OpenForms.OfType<Information>().First().Close();
                openReportList();
            }
            else if (Application.OpenForms.OfType<EstateDetail>().Count() == 1)
            {
                Application.OpenForms.OfType<EstateDetail>().First().Close();
                openReportList();
            }
            else if (Application.OpenForms.OfType<RecoveryForm>().Count() == 1)
            {
                Application.OpenForms.OfType<RecoveryForm>().First().Close();
                openReportList();
            }
            else
            {
                openReportList();
            }
        }
        void openReportList()// open report list
        {
            if (estateserv.GetAll().Where(x => x.Show == EnumsModel.showType.show).ToList().Count != 0)
            {
                if(panel_List.Visible == false)
                {
                    pictureBox2.Visible = false;
                    pnl_add.Visible = false;
                    panel_List.Visible = true;
                    
                    ShowItems(estateserv.GetAll());
                }
                else
                {
                    panel_List.Visible = false;
                    pnl_add.Visible = false;
                    pictureBox2.Visible = true;
                }
            }
            else
            {

                Notification.notiShow("هیچ ملکی ثبت نشده است","Dasboard , Open Report List" , Notification.msgType.error);
            }
        }

        private void btnBill_Click(object sender, EventArgs e)//gharardad
        {
            movePanelColor(btnBill);

            if (Application.OpenForms.OfType<Add>().Count() == 1)
            {
                Application.OpenForms.OfType<Add>().First().Close();
                openEstateNotSaleBill();
            }
            else if (Application.OpenForms.OfType<ForSelectEstate>().Count() == 1)
            {
                Application.OpenForms.OfType<ForSelectEstate>().First().Close();
                openEstateNotSaleBill();
            }
            else if (Application.OpenForms.OfType<Information>().Count() == 1)
            {
                Application.OpenForms.OfType<Information>().First().Close();
                openEstateNotSaleBill();
            }
            else if (Application.OpenForms.OfType<EstateDetail>().Count() == 1)
            {
                Application.OpenForms.OfType<EstateDetail>().First().Close();
                openEstateNotSaleBill();
            }
            else if (Application.OpenForms.OfType<RecoveryForm>().Count() == 1)
            {
                Application.OpenForms.OfType<RecoveryForm>().First().Close();
                openEstateNotSaleBill();
            }
            else
            {
                openEstateNotSaleBill();
            }
        }
        void openEstateNotSaleBill()//open gharardad
        {
            if (estateserv.GetAll().Where(x => x.Show == EnumsModel.showType.show).ToList().Count != 0)
            {
                var notSales = (from a in estateserv.GetAll()
                                where a.Sale == EnumsModel.Sele.notsale
                                select a).ToList();
                if (notSales.Count != 0)
                {
                    ForSelectEstate fse = new ForSelectEstate();
                    pnl_add.Visible = false;
                    panel_List.Visible = false;
                    fse.Show();
                }
                else
                {
                    Notification.notiShow(" ملکی برای قرارداد وجود ندارد","Dashboard , Open Estate Not Bill" ,Notification.msgType.error);
                }
            }
            else
            {
                Notification.notiShow("هیچ گونه ملکی ثبت نشده است", "Dashboard , Open Estate Not Bill", Notification.msgType.error);
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)//information 
        {
            movePanelColor(btnInfo);

            if (Application.OpenForms.OfType<Add>().Count() == 1)
            {
                Application.OpenForms.OfType<Add>().First().Close();
                openInformation();
            }
            else if (Application.OpenForms.OfType<ForSelectEstate>().Count() == 1)
            {
                Application.OpenForms.OfType<ForSelectEstate>().First().Close();
                openInformation();
            }
            else if (Application.OpenForms.OfType<Information>().Count() == 1)
            {
                Application.OpenForms.OfType<Information>().First().Close();
                openInformation();
            }
            else if (Application.OpenForms.OfType<EstateDetail>().Count() == 1)
            {
                Application.OpenForms.OfType<EstateDetail>().First().Close();
                openInformation();
            }
            else if(Application.OpenForms.OfType<RecoveryForm>().Count()== 1)
            {
                Application.OpenForms.OfType<RecoveryForm>().First().Close();
                openInformation();
            }
            else
            {
                openInformation();
            }
        }
        void openInformation()//open information
        {
            Information info = new Information();
            pnl_add.Visible = false;
            panel_List.Visible = false;
            info.Show();
        }

        private void normalState_Click(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void checkBoxSale_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxSale.Checked == true)
            {
                Sale.Enabled = true;
                Sale.Value = 0;
            }
            else
            {
                Sale.Enabled = false;
                Sale.Value = 0;
            }
        }

        private void DGEstates_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32.TryParse(DGEstates.CurrentRow.Cells[0].Value.ToString(), out int curentID);

            var editEstate = estateserv.GetAll().Where(x => x.Id == curentID).SingleOrDefault();

            if(editEstate != null)
            {

                resetTexts();

                txt_search.Enabled = false;
                btnSubmit.Enabled = false;
                btnchangeInfromation.Enabled = false;
                DGEstates.Enabled = false;
                estateDetailGB.Enabled = true;
                adressGB.Enabled = true;
                firstInformationGB.Enabled = true;
                com_AreaType.Enabled = true;
                com_estateType.Enabled = true;
                com_SaleType.Enabled = true;
                btnAddEstate.Enabled = true;
                adressGB.Enabled = true;
                OwnerAndPayGB.Enabled = true;
                estateDetailGB.Enabled = true;

                estateBinding(estateserv.GetAll().Where(x => x.Id == editEstate.Id).ToList());

                showForEdit(editEstate);
            }
        }
        void showForEdit(Estate est)
        {
            txt_id.Text = est.Id.ToString();

            com_AreaType.SelectedText.Equals(est.Area.Name);
            com_SaleType.SelectedText.Equals(est.SaleType.Name);
            com_estateType.SelectedText.Equals(est.EstateType.Name);

            txt_name.Text = est.Owner.Name;
            txt_family.Text = est.Owner.Family;
            txt_phonenumber.Text = est.Owner.Phonenumber;
            ejare.Value = est.EjareDeposit;
            rahn.Value = est.RahnDeposit;
            Sale.Value = est.SaleDeposit;
            keySale.Checked = est.EstateDetail.KeySale;
            ComDirection.SelectedIndex = 0;//chek shavad
            txt_buildyear.Text = est.EstateDetail.BuildYear.ToString();
            txt_thearea.Text = est.EstateDetail.TheArea.ToString();
            txt_mastercount.Text = est.EstateDetail.MasterRoomCount.ToString();
            txt_floor.Text = est.Adress.Floor.ToString();
            txt_roomcount.Text = est.EstateDetail.RoomCount.ToString();
            txt_street.Text = est.Adress.Street;
            txt_alley.Text = est.Adress.Alley;
            txt_postcode.Text = est.Adress.Postcode.ToString();
            txt_estateName.Text = est.Adress.EstateName;
            txt_unit.Text = est.Adress.Unit.ToString();
            txt_estateinfo.Text = est.Info;
            txt_ownerinfo.Text = est.Owner.Info;
            

            switch (est.EstateDetail.Direction)
            {
                case EnumsModel.Direction.Empty:
                    ComDirection.SelectedIndex = 0;
                    break;
                case EnumsModel.Direction.shomal:
                    ComDirection.SelectedIndex = 1;
                    break;
                case EnumsModel.Direction.jonob:
                    ComDirection.SelectedIndex = 2;
                    break;
                case EnumsModel.Direction.shargh:
                    ComDirection.SelectedIndex = 3;
                    break;
                case EnumsModel.Direction.gharb:
                    ComDirection.SelectedIndex = 4;
                    break;
            }
        }

        void resetTexts()
        {
            txt_id.ResetText();
            txt_name.ResetText();
            txt_family.ResetText();
            txt_phonenumber.ResetText();
            ejare.Value = 0;
            rahn.Value = 0;
            Sale.Value = 0;
            keySale.Checked = false;
            checkBoxejare.Checked = false;
            checkBoxrahn.Checked = false;
            checkBoxSale.Checked = false;
            ComDirection.SelectedIndex = 0;
            txt_buildyear.ResetText();
            txt_thearea.ResetText();
            txt_mastercount.ResetText();
            txt_floor.ResetText();
            txt_roomcount.ResetText();
            txt_street.ResetText();
            txt_alley.ResetText();
            txt_postcode.ResetText();
            txt_estateName.ResetText();
            txt_unit.ResetText();
            txt_estateinfo.ResetText();
            txt_ownerinfo.ResetText();
        }
        void comBinding()
        {
            com_AreaType.DataSource = areaServ.GetAll().Where(x => x.ShowType == EnumsModel.showType.show).ToList();
            com_estateType.DataSource = estateTypeServ.GetAll().Where(x => x.ShowType == EnumsModel.showType.show).ToList();
            com_SaleType.DataSource = saleTypeServ.GetAll().Where(x => x.ShowType == EnumsModel.showType.show).ToList();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            comBinding();
            resetTexts();
        }
        void movePanelColor(Button btn)
        {
            PanelColor.Visible = true;
            PanelColor.Height = btn.Height;
            PanelColor.Top = btn.Top;

            CloseColorPnl.Start();
        }

        private void CloseColorPnl_Tick(object sender, EventArgs e)
        {
            PanelColor.Visible = false;
        }

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            List<Estate> list = ((DataPrameter)e.Argument).estateList;
            string fileName = ((DataPrameter)e.Argument).fileName;
            //Microsoft.Office.Interop.Excel.Application exel = new Microsoft.Office.Interop.Excel.Application();
            //Microsoft.Office.Interop.Excel.Workbook wb = exel.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
            //Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)exel.ActiveSheet;
            //exel.Visible = false;
            //int index = 1;
            //int process = list.Count;

            //ws.Cells[1, 1] = "شماره";
            //ws.Cells[1, 2] =  "خیابان";
            //ws.Cells[1, 3] =  "کوچه";
            //ws.Cells[1, 4] =  "نام ساختمان";
            //ws.Cells[1, 5] =  "طبقه";
            //ws.Cells[1, 6] =  "واحد";
            //ws.Cells[1, 7] =  "نوع ملک";
            //ws.Cells[1, 8] =  "نوع پرداخت";
            //ws.Cells[1, 9] =  "منطقه";
            //ws.Cells[1, 10] = "صاحب ملک";
            //ws.Cells[1, 11] = "شماره تلفن";

            //foreach (Estate item in list)
            //{
            //    if(!backgroundWorker.CancellationPending)
            //    {
            //        backgroundWorker.ReportProgress(index++ * 100 / process);
            //        ws.Cells[index, 1] = item.Id.ToString();
            //        ws.Cells[index, 2] = item.Adress.Street;
            //        ws.Cells[index, 3] = item.Adress.Alley;
            //        ws.Cells[index, 4] = item.Adress.EstateName;
            //        ws.Cells[index, 5] = item.Adress.Floor;
            //        ws.Cells[index, 6] = item.Adress.Unit;
            //        ws.Cells[index, 7] = item.EstateType.Name;
            //        ws.Cells[index, 8] = item.SaleType.Name;
            //        ws.Cells[index, 9] = item.Area.Name;
            //        ws.Cells[index, 10] = item.Owner.Name + item.Owner.Family;
            //        ws.Cells[index, 11] = item.Owner.Phonenumber;
            //    }
            //}
            //ws.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
            //exel.Quit();
        }

        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            lblstatus.Text = string.Format("در حال پردازش ... {0}", e.ProgressPercentage);
            progressBar.Update();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if(e.Error == null)
            {
                Thread.Sleep(100);
                lblstatus.Text = "گزارش با موفقیت ذخیره شد";
                ResetProgress.Start();
            }
        }
        struct DataPrameter
        {
            public List<Estate> estateList;
            public string fileName { get; set; }
        }

        DataPrameter _inputPrameter;

        private void PicGB_Enter(object sender, EventArgs e)
        {

        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            estateBinding(estateserv.GetAll().Where(x => x.Sale == EnumsModel.Sele.notsale 
                                                        && x.Show == EnumsModel.showType.show 
                                                        && x.Adress.Street.Contains(txt_search.Text)
                                                        || x.Adress.Alley.Contains(txt_search.Text)
                                                        || x.Adress.EstateName.Contains(txt_search.Text)
                                                        || x.Adress.Floor.ToString().Contains(txt_search.Text)
                                                        || x.Adress.Unit.ToString().Contains(txt_search.Text)
                                                        || x.Adress.Postcode.ToString().Contains(txt_search.Text)).ToList());
        }

        private void ResetProgress_Tick(object sender, EventArgs e)
        {
            lblstatus.Text = "";
            progressBar.Value = 0;
            ResetProgress.Stop();
        }

        private void btn_delestate_Click(object sender, EventArgs e)
        {
            Int32.TryParse(DGEstates.CurrentRow.Cells[0].Value.ToString(), out int curentID);
            MessageBox.Show(curentID.ToString());

            var res =  MessageBox.Show("آیا میخواهید ملک مورد نظر را حذف کنید ؟", "پیام",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question,
               MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);

            if(res == DialogResult.Yes)
            {
                var estForDel = estateserv.GetAll().Where(x => x.Id == curentID).SingleOrDefault();

                if( estForDel != null)
                {

                    var resultDel = estateserv.Delete(estForDel.Id);

                    if(resultDel == true)
                    {
                        Notification.notiShow("ملک مورد نظر با موفقیت حذف شد" , null,Notification.msgType.success);

                        estateBinding(estateserv.GetAll().Where(x => x.Sale == EnumsModel.Sele.notsale && x.Show == EnumsModel.showType.show).ToList());
                    }
                    else
                    {
                        Notification.notiShow("خطا در حذف ملک مورد نظر","Dashboard , btn Delete Estate Click", Notification.msgType.error);
                    }
                }
                else
                {
                    Notification.notiShow("خطا در جستجو ملک مورد نظر", "Dashboard , btn Delete Estate Click", Notification.msgType.error);
                }
            }
        }

        private void btn_recover_Click(object sender, EventArgs e)
        {
            movePanelColor(btn_recover);

            if (Application.OpenForms.OfType<Add>().Count() == 1)
            {
                Application.OpenForms.OfType<Add>().First().Close();
                openRecoverForm();
            }
            else if (Application.OpenForms.OfType<ForSelectEstate>().Count() == 1)
            {
                Application.OpenForms.OfType<ForSelectEstate>().First().Close();
                openRecoverForm();
            }
            else if (Application.OpenForms.OfType<Information>().Count() == 1)
            {
                Application.OpenForms.OfType<Information>().First().Close();
                openRecoverForm();
            }
            else if (Application.OpenForms.OfType<EstateDetail>().Count() == 1)
            {
                Application.OpenForms.OfType<EstateDetail>().First().Close();
                openRecoverForm();
            }
            else if (Application.OpenForms.OfType<RecoveryForm>().Count() == 1)
            {
                Application.OpenForms.OfType<RecoveryForm>().First().Close();
                openRecoverForm();
            }
            else
            {
                openRecoverForm();
            }
        }
        void openRecoverForm()
        {
            if (estateTypeServ.GetAll().Where(x => x.ShowType == EnumsModel.showType.hide).ToList().Count == 0 &&
                areaServ.GetAll().Where(x => x.ShowType == EnumsModel.showType.hide).ToList().Count == 0 &&
                saleTypeServ.GetAll().Where(x => x.ShowType == EnumsModel.showType.hide).ToList().Count == 0 &&
                estateserv.GetAll().Where(x => x.Show == EnumsModel.showType.hide && x.Sale == EnumsModel.Sele.notsale).ToList().Count == 0 &&
                estateserv.GetAll().Where(x => x.Show == EnumsModel.showType.hide && x.Sale == EnumsModel.Sele.sale).ToList().Count == 0)
                {
                    Notification.notiShow("هیچ رکورد حذف شده ای پیدا نشد","Dashboard , Open Recover Form", Notification.msgType.error);
                }
            else
            {
                RecoveryForm rf = new RecoveryForm();

                pnl_add.Visible = false;
                panel_List.Visible = false;
                rf.Show();
            }
        }
        List<Bitmap> images = new List<Bitmap>();
        int imageNumber = 0;
        private void loadNextImage()
        {
            if(imageNumber == 3)
            {
                imageNumber = 0;
            }
            pictureBox2.Image = images[imageNumber];
            imageNumber++;
        }
        private void SlideShow_Tick(object sender, EventArgs e)
        {
            loadNextImage();
        }

        public void Dashbord_Load(object sender, EventArgs e)
        {
            images.Add(RealEstate.Properties.Resources._1);
            images.Add(RealEstate.Properties.Resources._2);
            images.Add(RealEstate.Properties.Resources._3);
            images.Add(RealEstate.Properties.Resources._4);

            SlideShow.Enabled = Properties.Settings.Default.ShowSlide;
            pictureBox2.Visible = Properties.Settings.Default.ShowPic;

        }

        private void Setting_Click(object sender, EventArgs e)//////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            SettingForm setfrm = new SettingForm();
            pnl_add.Visible = false;
            panel_List.Visible = false;
            setfrm.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Add>().Count() == 1)
            {
                Application.OpenForms.OfType<Add>().First().Close();
            }
            else if (Application.OpenForms.OfType<ForSelectEstate>().Count() == 1)
            {
                Application.OpenForms.OfType<ForSelectEstate>().First().Close();
            }
            else if (Application.OpenForms.OfType<Information>().Count() == 1)
            {
                Application.OpenForms.OfType<Information>().First().Close();
            }
            else if (Application.OpenForms.OfType<EstateDetail>().Count() == 1)
            {
                Application.OpenForms.OfType<EstateDetail>().First().Close();
            }
            else if (Application.OpenForms.OfType<RecoveryForm>().Count() == 1)
            {
                Application.OpenForms.OfType<RecoveryForm>().First().Close();
            }
        }
    }
}
