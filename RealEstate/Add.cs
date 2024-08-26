using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Service;
using Model;

namespace RealEstate
{
    public partial class Add : Form
    {
        AreaServ areaServ = new AreaServ();
        EstateTypeServ estServ = new EstateTypeServ();
        SaleTypeServ saleServ = new SaleTypeServ();

        AddType saveType;

        Area areaFound = null;
        EstateType estFound = null;
        SaleType saleFound = null;

        public Add(AddType type)
        {
            InitializeComponent();

            saveType = type;

            switch (saveType)
            {
                case AddType.AreaType:
                    lblName.Text = "نام منطقه";
                    break;
                case AddType.EstateType:
                    lblName.Text = "نوع ملک";
                    break;
                case AddType.SaleType:
                    lblName.Text = "نوع پرداخت";
                    break;
            }
            Binding(type);
        }

        /// //////////////////////////////////////////////////////////////////////////////////////
        /// 
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
        /// //////////////////////////////////////////////////////////////////////////////////////

        void Binding(AddType type)
        {
            switch (type)
            {
                case AddType.AreaType:

                    dg_estateType.Rows.Clear();
                    var areaItems = areaServ.GetAll().Where(x => x.ShowType == EnumsModel.showType.show);
                    foreach (var item in areaItems)
                    {
                        dg_estateType.Rows.Add(item.Id, item.Name, item.ShowType);
                    }

                    break;
                case AddType.EstateType:

                    dg_estateType.Rows.Clear();
                    var estateItems = estServ.GetAll().Where(x => x.ShowType == EnumsModel.showType.show);
                    foreach (var item in estateItems)
                    {
                        
                        dg_estateType.Rows.Add(item.Id, item.Name, item.ShowType);
                    }

                    break;
                case AddType.SaleType:

                    dg_estateType.Rows.Clear();
                    var saleItems = saleServ.GetAll().Where(x => x.ShowType == EnumsModel.showType.show);
                    foreach (var item in saleItems)
                    {
                        dg_estateType.Rows.Add(item.Id, item.Name, item.ShowType);
                    }

                    break;
            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {

            
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Name.Text))
            {
                MessageBox.Show("لطفا فیلد را تکمیل کنید . ", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
            }
            else
            {
                switch (saveType)
                {
                    case AddType.AreaType:

                        Area area = new Area()
                        {
                            Name = txt_Name.Text,
                            ShowType = EnumsModel.showType.show
                        };

                        var checkArea = areaServ.Add(area);
                        if (checkArea == true)
                        {
                            Notification.notiShow("با موفقیت ذخیره شد",null, Notification.msgType.success);
                            txt_Name.ResetText();
                            Binding(saveType);
                        }
                        else
                        {
                            txt_Name.ResetText();
                            Notification.notiShow("خطا در ثبت منطقه","AddForm,areatype", Notification.msgType.error);
                        }

                        break;
                    case AddType.EstateType:

                        EstateType est = new EstateType()
                        {
                            Name = txt_Name.Text,
                            ShowType = EnumsModel.showType.show,
                        };
                        var chekEstateType = estServ.Add(est);
                        if (chekEstateType == true)
                        {
                            Notification.notiShow("با موفقیت ذخیره شد",null, Notification.msgType.success);
                            txt_Name.ResetText();
                            Binding(saveType);
                        }
                        else
                        {
                            txt_Name.ResetText();
                            Notification.notiShow("خطا در ثبت نوع ملک","AddForm,EstateType", Notification.msgType.error);
                        }

                        break;
                    case AddType.SaleType:

                        SaleType st = new SaleType()
                        {
                            Name = txt_Name.Text,
                            ShowType = EnumsModel.showType.show,
                        };
                        var checkSaleType = saleServ.Add(st);

                        if (checkSaleType == true)
                        {
                            Notification.notiShow("با موفقیت ذخیره شد",null, Notification.msgType.success);
                            txt_Name.ResetText();
                            Binding(saveType);
                        }
                        else
                        {
                            txt_Name.ResetText();
                            Notification.notiShow("خطا در ثبت نوع پرداخت","AddForm,SaleType",Notification.msgType.error);
                        }

                        break;
                }
                
            }
        }
        int curentId;
        private void dg_estateType_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int.TryParse(dg_estateType.CurrentRow.Cells[0].Value.ToString(), out curentId);

            switch (saveType)
            {
                case AddType.AreaType:

                    areaFound = areaServ.GetAll().Where(item => item.Id == curentId).SingleOrDefault();

                    if (areaFound != null)
                    {
                        txt_Name.Text = areaFound.Name;
                        btn_save.Visible = false;
                        btn_edit.Visible = true;
                        btnDel.Visible = true;
                    }

                    break;
                case AddType.EstateType:

                    estFound = estServ.GetAll().Where(item => item.Id == curentId).SingleOrDefault();

                    if (estFound != null)
                    {
                        txt_Name.Text = estFound.Name;
                        btn_save.Visible = false;
                        btn_edit.Visible = true;
                        btnDel.Visible = true;
                    }

                    break;
                case AddType.SaleType:

                    saleFound = saleServ.GetAll().Where(item => item.Id == curentId).SingleOrDefault();

                    if (saleFound != null)
                    {
                        txt_Name.Text = saleFound.Name;
                        btn_save.Visible = false;
                        btn_edit.Visible = true;
                        btnDel.Visible = true;
                    }
                    break;
            }


            
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            switch (saveType)
            {
                case AddType.AreaType:


                    if (areaFound != null && !string.IsNullOrEmpty(txt_Name.Text))
                    {
                        Area area = new Area()
                        {
                            Id = areaFound.Id,
                            Name = txt_Name.Text,
                            ShowType = EnumsModel.showType.show
                        };

                        var res = areaServ.Edit(area);

                        if (res == true)
                        {
                            Notification.notiShow("ویرایش با موفقیت انجام شد",null, Notification.msgType.success);
                            txt_Name.ResetText();
                            btn_save.Visible = true;
                            btn_edit.Visible = false;
                            btnDel.Visible = false;
                            Binding(saveType);
                        }
                        else
                        {
                            Notification.notiShow("خطا در ویرایش منطقه","AddForm,EditArea", Notification.msgType.error);
                            txt_Name.ResetText();
                            btn_save.Visible = true;
                            btn_edit.Visible = false;
                            btnDel.Visible = false;
                        }
                    }
                    else
                    {
                        Notification.notiShow(" خطا در جستجو یا فبلد ویرایش خالی است", "AddForm,EditArea", Notification.msgType.error);
                        txt_Name.ResetText();
                        btn_save.Visible = true;
                        btn_edit.Visible = false;
                        btnDel.Visible = false;
                    }

                    break;
                case AddType.EstateType:


                    if (estFound != null && !string.IsNullOrEmpty(txt_Name.Text))
                    {
                        EstateType est = new EstateType()
                        {
                            Id = estFound.Id,
                            Name = txt_Name.Text,
                            ShowType = EnumsModel.showType.show
                        };

                        var res = estServ.Edit(est);

                        if (res == true)
                        {
                            Notification.notiShow("ویرایش با موفقیت انجام شد",null, Notification.msgType.success);
                            txt_Name.ResetText();
                            btn_save.Visible = true;
                            btn_edit.Visible = false;
                            btnDel.Visible = false;
                            Binding(saveType);
                        }
                        else
                        {
                            Notification.notiShow("خطا در ویرایش نوع ملک","Add Form,Edit Estate Type", Notification.msgType.error);
                            txt_Name.ResetText();
                            btn_save.Visible = true;
                            btn_edit.Visible = false;
                            btnDel.Visible = false;
                        }
                    }
                    else
                    {
                        Notification.notiShow(" خطا در جستجو یا فبلد ویرایش خالی است", "Add Form , Edit Estate Type", Notification.msgType.error);
                        txt_Name.ResetText();
                        btn_save.Visible = true;
                        btn_edit.Visible = false;
                        btnDel.Visible = false;
                    }

                    break;
                case AddType.SaleType:


                    if (saleFound != null && !string.IsNullOrEmpty(txt_Name.Text))
                    {
                        SaleType st = new SaleType()
                        {
                            Id = saleFound.Id,
                            Name = txt_Name.Text,
                            ShowType = EnumsModel.showType.show
                        };

                        var res = saleServ.Edit(st);

                        if (res == true)
                        {
                            Notification.notiShow("ویرایش با موفقیت انجام شد", null, Notification.msgType.success);

                            txt_Name.ResetText();
                            btn_save.Visible = true;
                            btn_edit.Visible = false;
                            btnDel.Visible = false;
                            Binding(saveType);
                        }
                        else
                        {
                            Notification.notiShow("خطا در ویرایش نوع منطقه","Add Form , Edit Sale Type", Notification.msgType.error);

                            txt_Name.ResetText();
                            btn_save.Visible = true;
                            btn_edit.Visible = false;
                            btnDel.Visible = false;
                        }
                    }
                    else
                    {
                        Notification.notiShow(" خطا در جستجو یا فبلد ویرایش خالی است","Add Form , Edit Sale Type", Notification.msgType.error); 

                        txt_Name.ResetText();
                        btn_save.Visible = true;
                        btn_edit.Visible = false;
                    }

                    break;
            }

        }
        public enum AddType
        {
            AreaType,
            EstateType,
            SaleType
        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            switch (saveType)
            {
                case AddType.AreaType:
                    var resArea = MessageBox.Show("آیا میخواهید رکورد مورد نظر حذف شود ؟", "پیام", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
                    if (resArea == DialogResult.Yes)
                    {
                        var areafound = areaServ.GetAll().Where(x => x.Id == curentId).SingleOrDefault();
                        var Check = areaServ.Delete(areafound);
                        if(Check == true)
                        {
                            Notification.notiShow("با موفقیت حذف شد",null, Notification.msgType.success);
                            txt_Name.ResetText();
                            btnDel.Visible = false;
                            btn_edit.Visible = false;
                            btn_save.Visible = true;
                            Binding(saveType);
                        }
                        else
                        {
                            Notification.notiShow("خطا در حذف منطقه مورد نطر","Add Form , Delete Area", Notification.msgType.error);
                        }
                    }
                    break;
                case AddType.EstateType:
                    var resEst = MessageBox.Show("آیا میخواهید رکورد مورد نظر حذف شود ؟", "پیام", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
                    if(resEst == DialogResult.Yes)
                    {
                        var found = estServ.GetAll().Where(x => x.Id == curentId).SingleOrDefault();
                        var Check = estServ.Delete(found);
                        if (Check == true)
                        {
                            Notification.notiShow("با موفقیت حذف شد",null, Notification.msgType.success);
                            btnDel.Visible = false;
                            btn_edit.Visible = false;
                            btn_save.Visible = true;
                            Binding(saveType);
                        }
                        else
                        {
                            Notification.notiShow("خطا در حذف نوع ملک مورد نطر","Add Form , Delete Estate Type", Notification.msgType.error);
                        }
                    }
                    break;
                case AddType.SaleType:
                    var resSale = MessageBox.Show("آیا میخواهید رکورد مورد نظر حذف شود ؟", "پیام", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
                    if(resSale == DialogResult.Yes)
                    {
                        var found = saleServ.GetAll().Where(x => x.Id == curentId).SingleOrDefault();
                        var Check = saleServ.Delete(found);
                        if (Check == true)
                        {
                            Notification.notiShow("با موفقیت حذف شد",null, Notification.msgType.success);
                            btnDel.Visible = false;
                            btn_edit.Visible = false;
                            btn_save.Visible = true;
                            Binding(saveType);
                        }
                        else
                        {
                            Notification.notiShow("خطا در حذف نوع پرداخت مورد نطر","Add Form , Delete Sale Type", Notification.msgType.error);
                        }
                    }
                    break;
            }
        }
    }
}
