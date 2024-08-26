using System;
using System.Data;
using System.Linq;
using Model;
using Service;
using System.Windows.Forms;

namespace RealEstate
{
    public partial class RecoveryForm : Form
    {
        private readonly AreaServ areaserv = new AreaServ();
        private readonly EstateServ estateServ = new EstateServ();
        private readonly SaleTypeServ saleTypeServ = new SaleTypeServ();
        private readonly EstateTypeServ estateTypeServ = new EstateTypeServ();

        enum recType
        {
            area,
            estatetype,
            saletype,
            estate,
            billestate
        }
        recType type;

        public RecoveryForm()
        {
            InitializeComponent();
        }
        void ShowArea()
        {
            RecoverList.Rows.Clear();
            
            var areaRec = areaserv.GetAll().Where(x => x.ShowType == EnumsModel.showType.hide).ToList();
            foreach (var item in areaRec)
            {
                RecoverList.Rows.Add(null,$"{item.Id}",$"{item.Name}");
            }
        }
        void ShowEstateType()
        {
            RecoverList.Rows.Clear();

            var estateType = estateTypeServ.GetAll().Where(x => x.ShowType == EnumsModel.showType.hide).ToList();
            foreach (var item in estateType)
            {
                RecoverList.Rows.Add(null, $"{item.Id}", $"{item.Name}");
            }
        }
        void ShowSaleType()
        {
            RecoverList.Rows.Clear();

            var saleType = saleTypeServ.GetAll().Where(x => x.ShowType == EnumsModel.showType.hide).ToList();
            foreach (var item in saleType)
            {
                RecoverList.Rows.Add(null, $"{item.Id}", $"{item.Name}");
            }
        }
        void ShowEstate()
        {
            RecoverList.Rows.Clear();

            var estates = estateServ.GetAll().Where(x => x.Show == EnumsModel.showType.hide && x.Sale == EnumsModel.Sele.notsale).ToList();

            foreach (var item in estates)
            {
                RecoverList.Rows.Add(null, $"{item.Id}",$"منطقه {item.Area.Name}"+
                                                                $"نوع ملک {item.EstateType.Name}" +
                                                                $"نوع پرداخت {item.SaleType.Name}" +
                                                                $"خیابان {item.Adress.Street.ToString()} " +
                                                                $"کوچه {item.Adress.Alley.ToString()} " +
                                                                $"ساختمان {item.Adress.EstateName.ToString()} " +
                                                                $"طبقه {item.Adress.Floor.ToString()} " +
                                                                $"واحد {item.Adress.Unit.ToString()} " +
                                                                $"کدپستی {item.Adress.Postcode.ToString()}");
            }
        }
        void ShowSaleEstate()
        {
            RecoverList.Rows.Clear();

            var estates = estateServ.GetAll().Where(x => x.Show == EnumsModel.showType.hide && x.Sale == EnumsModel.Sele.sale).ToList();

            foreach (var item in estates)
            {
                RecoverList.Rows.Add(null, $"{item.Id}",$"منطقه {item.Area.Name}" +
                                                                $"نوع ملک {item.EstateType.Name}" +
                                                                $"نوع پرداخت {item.SaleType.Name}" +
                                                                $"خیابان {item.Adress.Street.ToString()} " +
                                                                $"کوچه {item.Adress.Alley.ToString()} " +
                                                                $"ساختمان {item.Adress.EstateName.ToString()} " +
                                                                $"طبقه {item.Adress.Floor.ToString()} " +
                                                                $"واحد {item.Adress.Unit.ToString()} " +
                                                                $"کدپستی {item.Adress.Postcode.ToString()}");
            }
        }

        private void RecoveryForm_Load(object sender, EventArgs e)
        {
            RecoverList.Rows.Clear();
            if (areaserv.GetAll().Where(x => x.ShowType == EnumsModel.showType.hide).ToList().Count <= 0)
            {
                btn_area.Enabled = false;
            }
            if(estateServ.GetAll().Where(x => x.Show == EnumsModel.showType.hide).ToList().Count <= 0)
            {
                btn_estate.Enabled = false;
            }
            if(estateTypeServ.GetAll().Where(x => x.ShowType == EnumsModel.showType.hide).ToList().Count <= 0)
            {
                btn_estaeType.Enabled = false;
            }
            if(saleTypeServ.GetAll().Where(x => x.ShowType == EnumsModel.showType.hide).ToList().Count <= 0)
            {
                btn_saleType.Enabled = false;
            }
            if(estateServ.GetAll().Where(x => x.Show == EnumsModel.showType.hide &&  x.Sale == EnumsModel.Sele.sale).ToList().Count <= 0)
            {
                btn_estateBill.Enabled = false;
            }
            
        }

        private void btn_area_Click(object sender, EventArgs e)
        {
            ShowArea();
            type = recType.area;
        }

        private void btn_estaeType_Click(object sender, EventArgs e)
        {
            ShowEstateType();
            type = recType.estatetype;
        }

        private void btn_saleType_Click(object sender, EventArgs e)
        {
            ShowSaleType();
            type = recType.saletype;
        }

        private void btn_estate_Click(object sender, EventArgs e)
        {
            ShowEstate();
            type = recType.estate;
        }

        private void btn_estateBill_Click(object sender, EventArgs e)
        {
            ShowSaleEstate();
            type = recType.billestate;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(RecoverList.Rows.Count > 0)
            {
                switch (type)
                {
                    case recType.area:
                        int area = 0;
                        for (int i = 0; i < RecoverList.Rows.Count; i++)
                        {
                            bool checkin = false;
                            try
                            {
                                checkin = (bool)RecoverList.Rows[i].Cells[0].Value;
                            }
                            catch
                            {

                            }
                            if (checkin == true)
                            {
                                int.TryParse(RecoverList.Rows[i].Cells[1].Value.ToString(), out int id);
                                areaserv.Recovery(id);
                                area++;
                            }
                        }

                        MessageBox.Show($"تعداد {area} رکورد بازگردانی شد.", "پیام", MessageBoxButtons.OK,
                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);

                        RecoveryForm_Load(sender, e);
                        break;
                    case recType.estatetype:
                        int estattype = 0;
                        for (int i = 0; i < RecoverList.Rows.Count; i++)
                        {
                            bool checkin = false;
                            try
                            {
                                checkin = (bool)RecoverList.Rows[i].Cells[0].Value;
                            }
                            catch
                            {

                            }
                            if (checkin == true)
                            {
                                int.TryParse(RecoverList.Rows[i].Cells[1].Value.ToString(), out int id);
                                estateTypeServ.Recovery(id);
                                estattype++;
                            }
                        }

                        MessageBox.Show($"تعداد {estattype} رکورد بازگردانی شد.", "پیام", MessageBoxButtons.OK,
                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);

                        RecoveryForm_Load(sender, e);
                        break;
                    case recType.saletype:
                        int saletype = 0;
                        for (int i = 0; i < RecoverList.Rows.Count; i++)
                        {
                            bool checkin = false;
                            try
                            {
                                checkin = (bool)RecoverList.Rows[i].Cells[0].Value;
                            }
                            catch
                            {

                            }
                            if (checkin == true)
                            {
                                int.TryParse(RecoverList.Rows[i].Cells[1].Value.ToString(), out int id);
                                saleTypeServ.Recovery(id);
                                saletype++;
                            }
                        }

                        MessageBox.Show($"تعداد {saletype} رکورد بازگردانی شد.", "پیام", MessageBoxButtons.OK,
                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        RecoveryForm_Load(sender, e);
                        break;
                    case recType.estate:
                        int estate = 0;
                        for (int i = 0; i < RecoverList.Rows.Count; i++)
                        {
                            bool checkin = false;
                            try
                            {
                                checkin = (bool)RecoverList.Rows[i].Cells[0].Value;
                            }
                            catch
                            {

                            }
                            if (checkin == true)
                            {
                                int.TryParse(RecoverList.Rows[i].Cells[1].Value.ToString(), out int id);
                                estateServ.Recovery(id);
                                estate++;
                            }
                        }

                        MessageBox.Show($"تعداد {estate} رکورد بازگردانی شد.", "پیام", MessageBoxButtons.OK,
                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);

                        RecoveryForm_Load(sender, e);
                        break;
                    case recType.billestate:
                        int billestate = 0;
                        for (int i = 0; i < RecoverList.Rows.Count; i++)
                        {
                            bool checkin = false;
                            try
                            {
                                checkin = (bool)RecoverList.Rows[i].Cells[0].Value;
                            }
                            catch
                            {

                            }
                            if (checkin == true)
                            {
                                int.TryParse(RecoverList.Rows[i].Cells[1].Value.ToString(), out int id);
                                estateServ.Recovery(id);
                                billestate++;
                            }
                        }
                        MessageBox.Show($"تعداد {billestate} رکورد بازگردانی شد.", "پیام", MessageBoxButtons.OK,
                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);

                        RecoveryForm_Load(sender, e);
                        break;
                }
            }
            else
            {
                Notification.notiShow("لیست خالی است" ,"Recovery Form , Button 1", Notification.msgType.error);
            }
        }
    }
                                        
}
