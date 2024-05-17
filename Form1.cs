using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Dapper;
namespace CRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        internal class ConnectionManager
        {
            internal static string ConnectionString = "Server=localhost;Database=crud;Trusted_Connection=True;";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DBShow();
        }
        private void AddBTN_Click(object sender, EventArgs e)
        {
              var addfrm = new frmAdd();
              addfrm.ShowDialog();
            DBShow();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DBShow();
        }
        private void DBShow()
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = User.UserShow();
            string[] columnNames = { "recid", "Name", "Age", "Birthday" };

            for (int i = 0; i < columnNames.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = columnNames[i];
            }
            lblCount.Text = dataGridView1.Rows.Count.ToString();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow clickedRow = dataGridView1.Rows[e.RowIndex];

                foreach (DataGridViewCell cell in clickedRow.Cells)
                {
                    cell.Selected = true;

                    int row = e.RowIndex;
                    txtDeleteID.Text = Convert.ToString(dataGridView1[0, row].Value);
                    DeleteBTN.Enabled = true;

                }
            }
        }
 

        private void DeleteBTN_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtDeleteID.Text);

            var deleteUser = new User
            {
                recid = id
            };


            if (DeleteUser(deleteUser))
            {
                if (MessageBox.Show("Are you sure want to delete recid: " + txtDeleteID.Text, "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    MessageBox.Show("Deleted User Success", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = User.UserShow();
                }




            }
        }

        private static bool DeleteUser(User user)
        {
            var sql = "DELETE FROM dbo.UserTest WHERE recid = @recid";

            using (var connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                connection.Open();
                var rowaffected = connection.Execute(sql, user);
                return rowaffected > 0;
            }

        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            
                frmEdit frmEDIT = new frmEdit();

                DataGridViewRow row = dataGridView1.SelectedRows[0];

                frmEDIT.txtID.Text = Convert.ToString(row.Cells[0].Value);
                frmEDIT.txtName.Text = Convert.ToString(row.Cells[1].Value);
                frmEDIT.txtage.Text = Convert.ToString(row.Cells[2].Value);
                frmEDIT.dateTimePicker1.Value = DateTime.Parse(row.Cells[3].Value.ToString());

                frmEDIT.ShowDialog();
                DBShow();
        }

        private void dataGridView1_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                dataGridView1.Rows[e.RowIndex].Selected = true;


                int row = e.RowIndex;
                txtDeleteID.Text = Convert.ToString(dataGridView1[0, row].Value);
                DeleteBTN.Enabled = true;
                btn_edit.Enabled = true;

            }
        }
    }
}
