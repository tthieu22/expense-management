using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseManagement.Views
{
    public partial class Setting : Form
    {
        private int _userID;
        public Setting(int userId)
        {
            InitializeComponent();
            _userID = userId;
        }

        private void tAccount_Click(object sender, EventArgs e)
        {

            int budgetId = 0;
            Home homeForm = Home.GetInstance(_userID);
            if (homeForm != null)
            {
                homeForm.ProcessToastAction($"openAccount|{budgetId}");
                this.Close();
            }
        }

        private void tReport_Click(object sender, EventArgs e)
        {

        }

        private void tNoti_Click(object sender, EventArgs e)
        {

            int budgetId = 0;
            Home homeForm = Home.GetInstance(_userID);
            if (homeForm != null)
            {
                homeForm.ProcessToastAction($"openAddNotifications|{budgetId}");
                this.Close();
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();

        }
    }
}
