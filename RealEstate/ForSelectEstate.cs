using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using Model;
using Service;
using System.Windows.Forms;

namespace RealEstate
{
    public partial class ForSelectEstate : Form
    {
        static EstateServ estServ = new EstateServ();
        public ForSelectEstate()
        {
            InitializeComponent();
            binding(estServ.GetAll().Where(item => item.Sale == EnumsModel.Sele.notsale).ToList());

            try
            {
                //stiReport1.Load("..//BillReport.mrt");
                
            }
            catch (Exception ext)
            {
                MessageBox.Show(ext.Message);
            }

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
        void binding(List<Estate> es)
        {
            SearchDGV.Rows.Clear();
            foreach (var item in es)
            {
                try
                {
                    SearchDGV.Rows.Add(item.Id,
                                        $"خیابان {item.Adress.Street.ToString()} " +
                                        $"/ کوچه {item.Adress.Alley.ToString()} " +
                                        $"/ ساختمان {item.Adress.EstateName.ToString()} " +
                                        $"/ طبقه {item.Adress.Floor.ToString()} " +
                                        $"/ واحد {item.Adress.Unit.ToString()} " +
                                        $"/ کدپستی {item.Adress.Postcode.ToString()}",
                                        item.SaleType.Name,
                                        item.EstateType.Name ,
                                        item.Area.Name,
                                        $"{item.Owner.Name} {item.Owner.Family}" ,
                                        item.Owner.Phonenumber);

                }
                catch (Exception ext)
                {

                    Notification.notiShow(ext.Message,"For Select Estate , Binding", Notification.msgType.error);
                }
                
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            binding(estServ.GetAll().Where(x => x.Sale == EnumsModel.Sele.notsale && x.Adress.Street.Contains(txtSearch.Text) 
                                                        || x.Adress.Alley.Contains(txtSearch.Text) 
                                                        || x.Adress.EstateName.Contains(txtSearch.Text)
                                                        || x.Adress.Floor.ToString().Contains(txtSearch.Text)
                                                        || x.Adress.Unit.ToString().Contains(txtSearch.Text)
                                                        || x.Adress.Postcode.ToString().Contains(txtSearch.Text)).ToList()); 
        }

        List<Estate> selecteEstate = new List<Estate>();

        private void SearchDGV_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32.TryParse(SearchDGV.CurrentRow.Cells[0].Value.ToString(), out int selectID);

            selectBinging(selectID);

        }
        void selectBinging(int es)
        {
            try
            {
                var search = estServ.GetAll().Where(x => x.Id == es && x.Sale == EnumsModel.Sele.notsale).SingleOrDefault();
                if(search != null)
                {
                    SelectedEstatesList.Rows.Add(search.Id,
                                                 search.EstateType.Name,
                                                 search.SaleType.Name,
                                                 search.Area.Name,
                                                 search.Adress.Street,
                                                 search.Adress.Alley,
                                                 search.Adress.EstateName,
                                                 search.Adress.Floor,
                                                 search.Adress.Unit,
                                                 search.Adress.Postcode,
                                                 search.RahnDeposit,
                                                 search.SaleDeposit,
                                                 search.EjareDeposit,
                                                 search.Info,
                                                 search.EstateDetail.TheArea,
                                                 search.EstateDetail.RoomCount,
                                                 search.EstateDetail.MasterRoomCount,
                                                 sendDirectionFarsi(search.EstateDetail.Direction),
                                                 search.EstateDetail.BuildYear,
                                                 search.EstateDetail.KeySale,
                                                 search.EstateDetail.Info);

                }
            }
            catch (Exception ext)
            {
                MessageBox.Show(ext.InnerException + " " + ext.ToString());
            }
        }
        string sendDirectionFarsi(EnumsModel.Direction di)
        {
            switch (di)
            {
                case EnumsModel.Direction.Empty:
                    return "";
                case EnumsModel.Direction.shomal:
                    return "شمال";
                case EnumsModel.Direction.jonob:
                    return "جنوب";
                case EnumsModel.Direction.shargh:
                    return "شرق";
                case EnumsModel.Direction.gharb:
                    return "غرب";
            }
            return "----";
        }

        private void txtOwnerFamily_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtinfo_TextChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            SaleGB.Enabled = false;
            SearchDGV.Enabled = true;

            txtbuyerFamily.ResetText();
            txtbuyerinfo.ResetText();
            txtbuyerName.ResetText();
            txtbuyerPhonenumber.ResetText();
        }
        private void saveSale_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtbuyerFamily.Text) || !string.IsNullOrEmpty(txtbuyerName.Text) ||
                    !string.IsNullOrEmpty(txtbuyerPhonenumber.Text))
            {
                List<Estate> selectedEs = new List<Estate>();

                for (int i = 0; i < SelectedEstatesList.Rows.Count; i++)
                {
                    int.TryParse(SelectedEstatesList.Rows[i].Cells[0].Value.ToString(), out int seId);
                    selectedEs.Add(estServ.GetAll().Where(x => x.Id.Equals(seId)).SingleOrDefault());
                }

                Buyer buyer = new Buyer()
                {
                    Name = txtbuyerName.Text,
                    Family = txtbuyerFamily.Text,
                    Phonenumber = txtbuyerPhonenumber.Text,
                    info = txtbuyerinfo.Text
                };

                Model.EstateDetail ed = new Model.EstateDetail()
                {
                    DischargeTime = "test",
                };

                int cont = 0;

                foreach (var item in selectedEs)
                {
                    Estate estSale = new Estate()
                    {
                        Id = item.Id,
                        Buyer = buyer,
                        EstateDetail = ed,
                        Sale = EnumsModel.Sele.sale

                    };

                    var finishSale = estServ.ChangeSale(estSale);

                    if(finishSale)
                    {
                        cont++;
                    }

                }
                if(cont == selectedEs.Count)
                {
                    var res = MessageBox.Show("آیا میخواهید رسید قرار داد چاپ شود ؟ ", "پیام" , MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            DataTable dtEstate = new DataTable();

                            dtEstate.Columns.Add("ID");
                            dtEstate.Columns.Add("Estate Type");
                            dtEstate.Columns.Add("Sale Type");
                            dtEstate.Columns.Add("Area Type");
                            dtEstate.Columns.Add("Street");
                            dtEstate.Columns.Add("Alley");
                            dtEstate.Columns.Add("Estate Name");
                            dtEstate.Columns.Add("Floor");
                            dtEstate.Columns.Add("Unit");
                            dtEstate.Columns.Add("Post Code");
                            dtEstate.Columns.Add("Rahn Deposit");
                            dtEstate.Columns.Add("Sale Deposit");
                            dtEstate.Columns.Add("Rent Deposit");
                            dtEstate.Columns.Add("Estate Info");
                            dtEstate.Columns.Add("The Area");
                            dtEstate.Columns.Add("Room Count");
                            dtEstate.Columns.Add("Master Room Count");
                            dtEstate.Columns.Add("Direction");
                            dtEstate.Columns.Add("Build Year");
                            dtEstate.Columns.Add("Key Sale");
                            dtEstate.Columns.Add("Estate Detail Info");

                            foreach (DataGridViewRow item in SelectedEstatesList.Rows)
                            {
                                dtEstate.Rows.Add(item.Cells[0].Value.ToString(),
                                                  item.Cells[1].Value.ToString(),
                                                  item.Cells[2].Value.ToString(),
                                                  item.Cells[3].Value.ToString(),
                                                  item.Cells[4].Value.ToString(),
                                                  item.Cells[5].Value.ToString(),
                                                  item.Cells[6].Value.ToString(),
                                                  item.Cells[7].Value.ToString(),
                                                  item.Cells[8].Value.ToString(),
                                                  item.Cells[9].Value.ToString(),
                                                  item.Cells[10].Value.ToString(),
                                                  item.Cells[11].Value.ToString(),
                                                  item.Cells[12].Value.ToString(),
                                                  item.Cells[13].Value.ToString(),
                                                  item.Cells[14].Value.ToString(),
                                                  item.Cells[15].Value.ToString(),
                                                  item.Cells[16].Value.ToString(),
                                                  item.Cells[17].Value.ToString(),
                                                  item.Cells[18].Value.ToString(),
                                                  item.Cells[19].Value.ToString());
                            }//to do get report

                            dt.Columns.Add("Date");
                            dt.Rows.Add(DateTime.Now);

                            stiReport1.RegData("Data Model", dt);
                            stiReport1.RegData("Table", dtEstate);

                            stiReport1.Load(Properties.Resources.BillFullReport);

                            stiReport1.Show();

                            SaleGB.Enabled = false;
                            SearchDGV.Enabled = true;

                            txtbuyerFamily.ResetText();
                            txtbuyerinfo.ResetText();
                            txtbuyerName.ResetText();
                            txtbuyerPhonenumber.ResetText();

                            binding(estServ.GetAll().Where(x => x.Sale == EnumsModel.Sele.notsale).ToList());
                        }
                        catch (Exception ext)
                        {
                            MessageBox.Show(ext.Message);
                        }
                       
                            
                    }
                    else
                    {
                        SaleGB.Enabled = false;
                        SearchDGV.Enabled = true;

                        txtbuyerFamily.ResetText();
                        txtbuyerinfo.ResetText();
                        txtbuyerName.ResetText();
                        txtbuyerPhonenumber.ResetText();

                        binding(estServ.GetAll().Where(x => x.Sale == EnumsModel.Sele.notsale).ToList());
                    }
                    
                }
                else
                {
                    Notification.notiShow("خطا در ثبت قرار داد جدید","For Select Estate , Add New Bill" ,Notification.msgType.error);
                }
            }
            else
            {
                Notification.notiShow("لطفا نام و نام خانوادگی و شماره تلفن را وارد کنید","For Select Estate , Add New Bill", Notification.msgType.Information);
            }
        }
        private new void KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (ch == 46)
            {
                e.Handled = true;
                return;
            }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
                WindowState = FormWindowState.Normal;
            else
                WindowState = FormWindowState.Maximized;
        }
        DataTable dataTableAdaptor(int id)
        {
            DataTable dt = null;
            var search = estServ.GetAll().Where(x => x.Id.Equals(id)).SingleOrDefault();

            if(search != null)
            {
                dt.Columns.Add("Id",typeof(int));
                dt.Columns.Add("Adress", typeof(string));
                dt.Columns.Add("BuyerName", typeof(string));
                dt.Rows.Add(search.Id, search.Adress.Street, search.Buyer.Name + " " + search.Buyer.Family);
            }
            return dt;
        }

        private void SelectedEstatesList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Delete)
            {
                SelectedEstatesList.Rows.RemoveAt(SelectedEstatesList.CurrentRow.Index);
            }
        }
    }
}
