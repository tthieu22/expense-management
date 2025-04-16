using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.System;

namespace ExpenseManagement.Views
{
    public partial class HomeAdmin : Form
    {
        private int _userId;
        private string _fullName;
        private Form activeForm = null;
        public HomeAdmin()
        {
            InitializeComponent();
        }

        public HomeAdmin(int userId, string fullName)
        {
            InitializeComponent();
            this._userId = userId;
            this._fullName = fullName;
        }
        private void LoadChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            plContentTab.Controls.Clear();
            plContentTab.Controls.Add(childForm);
            plContentTab.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            LoadChildForm(new Account(this._userId));
        }

        private void btnReport_Click(object sender, EventArgs e)
        {

        }

        private void btnAPI_Click(object sender, EventArgs e)
        {
            LoadChildForm(new API(this._userId));

        }

        private void btnNotification_Click(object sender, EventArgs e)
        {

        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            LoadChildForm(new Category(_userId,0));

        }

        private void btnCategoryGroup_Click(object sender, EventArgs e)
        {
            LoadChildForm(new GroupCategory(_userId));
        }

        private void btnAvatar_Click(object sender, EventArgs e)
        {

            var homeForm = new Home(_userId);
            homeForm.Show();
        }
    }
}
