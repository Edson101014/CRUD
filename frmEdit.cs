﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class frmEdit : Form
    {
        public frmEdit()
        {
            InitializeComponent();
        }

        private void CancelBTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditBTN_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "" || txtage.Text.Trim() == "")
            {
                MessageBox.Show("Input cannot be blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime birthdate = dateTimePicker1.Value;
            DateTime currentDate = DateTime.Today;
            int calculatedAge = currentDate.Year - birthdate.Year;
            if (birthdate > currentDate.AddYears(-calculatedAge))
            {
                calculatedAge--;
            }

            if (int.Parse(txtage.Text) != calculatedAge)
            {
                MessageBox.Show("Age does not match the birthdate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var editUser = new User
            {
                recid = int.Parse(txtID.Text),
                Name = txtName.Text,
                Age = int.Parse(txtage.Text),
                Birth = dateTimePicker1.Value,
            };
            if (User.EditUser(editUser))
            {
                MessageBox.Show("EDIT User Success", "EDIT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Clear();
                txtage.Clear();
                dateTimePicker1.Value = DateTime.Now;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error adding User");
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
        }

        private void txtage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
