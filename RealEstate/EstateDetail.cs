using System;
using System.Data;
using System.Drawing;
using System.Linq;
using Service;
using Model;
using System.Windows.Forms;

namespace RealEstate
{

    public partial class EstateDetail : Form
    {
        
        EstateServ estateServ = new EstateServ();
        Estate est;
        public EstateDetail()
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
        
        Estate estateSearch = null;
        public void curentItem(int estateID)
        {
            estateSearch = (from a in estateServ.GetAll()
                          where a.Id == estateID
                          select a).SingleOrDefault();
            est = estateSearch;

            if (estateSearch != null && estateSearch.Sale == EnumsModel.Sele.notsale)
            {
                txt_adress.Text = $"خ {estateSearch.Adress.Street.ToString()} / ک {estateSearch.Adress.Alley.ToString()} / س {estateSearch.Adress.EstateName.ToString()} " +
                $"/ ط {estateSearch.Adress.Floor.ToString()} / و {estateSearch.Adress.Unit.ToString()} / ک {estateSearch.Adress.Postcode.ToString()}";
                txt_area.Text = estateSearch.Area.Name;
                txt_ejare.Text = $"{estateSearch.EjareDeposit}";
                txt_estateType.Text = estateSearch.EstateType.Name;
                Keysale.Checked = estateSearch.EstateDetail.KeySale;
                txt_masterCount.Text = $"{estateSearch.EstateDetail.MasterRoomCount}";
                txt_owner.Text = $"{estateSearch.Owner.Name} {estateSearch.Owner.Family}";
                txt_ownerPhonenumber.Text = $"{estateSearch.Owner.Phonenumber}";
                txt_rahn.Text = $"{estateSearch.RahnDeposit}";
                txt_roomCount.Text = $"{estateSearch.EstateDetail.RoomCount}";
                txt_saleType.Text = estateSearch.SaleType.Name;
                txt_theArea.Text = $"{estateSearch.EstateDetail.TheArea}";
                txtِdirection.Text = $"{estateSearch.EstateDetail.Direction}";
                txt_sale.Text = $"{estateSearch.SaleDeposit}";
                txt_estinfo.Text = estateSearch.Info;
                txt_owinfo.Text = estateSearch.Owner.Info;

                txt_discharge.Text = "-----";
                txt_saleto.Text = "-----";
                txt_salePhonenumber.Text = "-----";
                txt_byinfo.Text = "-----";
            }
            else if(estateSearch != null && estateSearch.Sale == EnumsModel.Sele.sale)
            {
                txt_adress.Text = $"خ {estateSearch.Adress.Street.ToString()} / ک {estateSearch.Adress.Alley.ToString()} / س {estateSearch.Adress.EstateName.ToString()} " +
                $"/ ط {estateSearch.Adress.Floor.ToString()} / و {estateSearch.Adress.Unit.ToString()} / ک {estateSearch.Adress.Postcode.ToString()}";
                txt_area.Text = estateSearch.Area.Name;
                txt_ejare.Text = $"{estateSearch.EjareDeposit}";
                txt_estateType.Text = estateSearch.EstateType.Name;
                Keysale.Checked = estateSearch.EstateDetail.KeySale;
                txt_masterCount.Text = $"{estateSearch.EstateDetail.MasterRoomCount}";
                txt_owner.Text = $"{estateSearch.Owner.Name} {estateSearch.Owner.Family}";
                txt_ownerPhonenumber.Text = $"{estateSearch.Owner.Phonenumber}";
                txt_rahn.Text = $"{estateSearch.RahnDeposit}";
                txt_roomCount.Text = $"{estateSearch.EstateDetail.RoomCount}";
                txt_saleType.Text = estateSearch.SaleType.Name;
                txt_theArea.Text = $"{estateSearch.EstateDetail.TheArea}";
                txtِdirection.Text = $"{estateSearch.EstateDetail.Direction}";
                txt_sale.Text = $"{estateSearch.SaleDeposit}";
                txt_estinfo.Text = estateSearch.Info;
                txt_owinfo.Text = estateSearch.Owner.Info;
                txt_saleto.Text = $"{estateSearch.Buyer.Name} {estateSearch.Buyer.Family}";
                txt_salePhonenumber.Text = estateSearch.Buyer.Phonenumber;
                txt_byinfo.Text = estateSearch.Buyer.info;
                txt_discharge.Text = estateSearch.EstateDetail.DischargeTime;
            }
            else
            {
                MessageBox.Show("error");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void EstateDetail_Load(object sender, EventArgs e)
        {
            
        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void new_ejare_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txt_estateType_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_ownerPhonenumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_theArea_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_rahn_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Click(object sender, EventArgs e)
        {

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

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
                WindowState = FormWindowState.Normal;
            else
                WindowState = FormWindowState.Maximized;
        }

        private void txt_saleType_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_area_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_adress_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_buildyear_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_roomCount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_masterCount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtِdirection_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_ejare_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_sale_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            if(est != null)
            {
                if(est.Sale == EnumsModel.Sele.sale)
                {
                    try
                    {
                        ///
                        //////////////////////////////////////////////////////////
                        ///
                        DataTable time = new DataTable();
                        time.Columns.Add("Time");
                        time.Rows.Add(DateTime.Now);
                        ///
                        ////////////////////////////////////////////////
                        ///

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

                        dtEstate.Rows.Add(est.Id,
                                          est.EstateType.Name,
                                          est.SaleType.Name,
                                          est.Area.Name,
                                          est.Adress.Street,
                                          est.Adress.Alley,
                                          est.Adress.EstateName,
                                          est.Adress.Floor,
                                          est.Adress.Unit,
                                          est.Adress.Postcode,
                                          est.RahnDeposit,
                                          est.SaleDeposit,
                                          est.EjareDeposit,
                                          est.Info,
                                          est.EstateDetail.TheArea,
                                          est.EstateDetail.RoomCount,
                                          est.EstateDetail.MasterRoomCount,
                                          GetDirection(est.EstateDetail.Direction),
                                          est.EstateDetail.BuildYear,
                                          est.EstateDetail.KeySale,
                                          est.EstateDetail.Info);
                        ///
                        ////////////////////////////////////////////
                        ///

                        DataTable ownerAndBuyer = new DataTable();

                        ownerAndBuyer.Columns.Add("Owner Name");
                        ownerAndBuyer.Columns.Add("Owner family");
                        ownerAndBuyer.Columns.Add("Ow Phonenumber");
                        ownerAndBuyer.Columns.Add("Buyer Name");
                        ownerAndBuyer.Columns.Add("Buyer Family");
                        ownerAndBuyer.Columns.Add("Bu Phonenumber");

                        ownerAndBuyer.Rows.Add(est.Owner.Name,
                                                est.Owner.Family,
                                                est.Owner.Phonenumber,
                                                est.Buyer.Name,
                                                est.Buyer.Family,
                                                est.Buyer.Phonenumber);

                        ////////////////////////////////////////////////

                        stiPrint.RegData("PersianTimeTable", time);
                        stiPrint.RegData("Estate", dtEstate);
                        stiPrint.RegData("Owner And Buyer", ownerAndBuyer);

                        stiPrint.Load(Properties.Resources.SingleEstateReport);
                        stiPrint.Show();
                    }
                    catch (Exception ext)
                    {
                        MessageBox.Show(ext.Message);
                    }
                }
                else
                {
                    try
                    {
                        ///
                        //////////////////////////////////////////////////////////
                        ///
                        DataTable time = new DataTable();
                        time.Columns.Add("Time");
                        time.Rows.Add(DateTime.Now);
                        ///
                        ////////////////////////////////////////////////
                        ///

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

                        dtEstate.Rows.Add(est.Id,
                                          est.EstateType.Name,
                                          est.SaleType.Name,
                                          est.Area.Name,
                                          est.Adress.Street,
                                          est.Adress.Alley,
                                          est.Adress.EstateName,
                                          est.Adress.Floor,
                                          est.Adress.Unit,
                                          est.Adress.Postcode,
                                          est.RahnDeposit,
                                          est.SaleDeposit,
                                          est.EjareDeposit,
                                          est.Info,
                                          est.EstateDetail.TheArea,
                                          est.EstateDetail.RoomCount,
                                          est.EstateDetail.MasterRoomCount,
                                          GetDirection(est.EstateDetail.Direction),
                                          est.EstateDetail.BuildYear,
                                          est.EstateDetail.KeySale,
                                          est.EstateDetail.Info);
                        ///
                        ////////////////////////////////////////////
                        ///

                        DataTable ownerAndBuyer = new DataTable();

                        ownerAndBuyer.Columns.Add("Owner Name");
                        ownerAndBuyer.Columns.Add("Owner family");
                        ownerAndBuyer.Columns.Add("Ow Phonenumber");
                        ownerAndBuyer.Columns.Add("Buyer Name");
                        ownerAndBuyer.Columns.Add("Buyer Family");
                        ownerAndBuyer.Columns.Add("Bu Phonenumber");

                        ownerAndBuyer.Rows.Add(est.Owner.Name,
                                                est.Owner.Family,
                                                est.Owner.Phonenumber,
                                                "------",
                                                "------",
                                                "------");

                        ////////////////////////////////////////////////

                        stiPrint.RegData("PersianTimeTable", time);
                        stiPrint.RegData("Estate", dtEstate);
                        stiPrint.RegData("Owner And Buyer", ownerAndBuyer);

                        stiPrint.Load(Properties.Resources.SingleEstateReport);
                        stiPrint.Show();
                    }
                    catch (Exception ext)
                    {
                        MessageBox.Show(ext.Message);
                    }
                }
                
                
            }
            

        }
        string GetDirection(EnumsModel.Direction di)
        {
            switch (di)
            {
                case EnumsModel.Direction.Empty:
                    return "----";
                case EnumsModel.Direction.shomal:
                    return "شمال";
                case EnumsModel.Direction.jonob:
                    return "جنوب";
                case EnumsModel.Direction.shargh:
                    return "شرق";
                case EnumsModel.Direction.gharb:
                    return "غرب";
            }
            return "-----";
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void txt_owner_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_phoneNumber_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void txt_saleto_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_byinfo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
