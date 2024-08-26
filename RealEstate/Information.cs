using System;
using System.Data;
using System.Drawing;
using Service;
using Model;
using System.Linq;
using System.Windows.Forms;

namespace RealEstate
{
    public partial class Information : Form
    {
        AccountServ accServ = new AccountServ();
        public Information()
        {
            InitializeComponent();
        }
        /////////////////////////////////////////////////////////

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
        /////////////////////////////////////////////////////////
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Information_Load(object sender, EventArgs e)
        {

            var acc = new LicenceServ().GetAll().ToList();
            var licence = (from a in acc
                           where a.licenceUsable == EnumsModel.LicenceUsable.use
                           select a).SingleOrDefault();

            //SKGL.Validate validate = new SKGL.Validate();
            //validate.secretPhase = new EncriptPassword().secretPhase();
            //validate.Key = licence.LicenceCode;
            //#endregion

            //lbl_st.Text = validate.CreationDate.ToFa();
            //lbl_fin.Text = validate.ExpireDate.ToFa();
            //lbl_dayleft.Text = validate.DaysLeft.ToString();
        }
    }
}
