using System;
using System.Data;
using System.Windows.Forms;
using ExpenseManagement.Controller;

namespace ExpenseManagement.Views
{
    public partial class API : Form
    {
        private readonly APIKeyController apiKeyController = new APIKeyController();
        private int _userID = 0;
        public API(int userId)
        {
            this._userID = userId;
            InitializeComponent();
            LoadAPIKeys();
        }

        private void LoadAPIKeys()
        {
            dgvAPI.DataSource = apiKeyController.GetAPIKeys();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string serviceName = tbServiceName.Text;
                string apiKey = tbApiKey.Text;

                if (apiKeyController.CreateAPIKey(this._userID, serviceName, apiKey))
                {
                    MessageBox.Show("API Key added successfully.");
                    LoadAPIKeys();
                }
                else
                {
                    MessageBox.Show("Failed to add API Key.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int apiKeyId = Convert.ToInt32(tbApiKeyId.Text);
                string serviceName = tbServiceName.Text;
                string apiKey = tbApiKey.Text;

                if (apiKeyController.UpdateAPIKey(apiKeyId, serviceName, apiKey))
                {
                    MessageBox.Show("API Key updated successfully.");
                    LoadAPIKeys();
                }
                else
                {
                    MessageBox.Show("Failed to update API Key.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int apiKeyId = Convert.ToInt32(tbApiKeyId.Text);
                if (apiKeyController.DeleteAPIKey(apiKeyId))
                {
                    MessageBox.Show("API Key deleted successfully.");
                    LoadAPIKeys();
                }
                else
                {
                    MessageBox.Show("Failed to delete API Key.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = apiKeyController.GetAPIKeys();
            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Format("service_name LIKE '%{0}%'", tbSearch.Text);
            dgvAPI.DataSource = dv.ToTable();
        }

        private void dgvAPI_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvAPI_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvAPI.Rows[e.RowIndex];

                tbApiKeyId.Text = row.Cells["api_key_id"].Value.ToString();
                tbServiceName.Text = row.Cells["service_name"].Value.ToString();
                tbApiKey.Text = row.Cells["api_key"].Value.ToString();
            }
        }

    }
}
