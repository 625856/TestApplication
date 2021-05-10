using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string strDisplay { get; set; }

        public double totalHours { get; set; }

        public decimal speed { get; set; }
        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/dd/yyyy hh:mm:ss";

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "MM/dd/yyyy hh:mm:ss";
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Driver name should not be empty!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
                return;
            }

            ddlDrivers.Text = txtName.Text;
            ddlDrivers.Items.Add(txtName.Text);
        }

        private void txtdistance_Leave(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("End Date must be greater than Start Date!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateTimePicker1.Focus();
                return;
            }

            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                MessageBox.Show("End Date must be greater than Start Date!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateTimePicker2.Focus();
                return;
            }


            if (ddlDrivers.Text == string.Empty || ddlDrivers.Text == null)
            {
                MessageBox.Show("No Driver assigned for this Trip!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtdistance.Text == string.Empty)
                txtdistance.Text = "0";


            totalHours = (dateTimePicker1.Value - dateTimePicker2.Value).Duration().TotalHours;

            if (totalHours > 0)
                speed = Math.Round(Convert.ToDecimal(Convert.ToDouble(txtdistance.Text) / totalHours), 2);
            else
                speed = 0;

            if (txtName.Text != string.Empty)
            {
                DialogResult dialogResult = MessageBox.Show("Would you like to continue?", "Confirmation", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    strDisplay = ddlDrivers.Text + ":" + " " + txtdistance.Text + " Miles" + " @ " + speed.ToString() + " mph";

                    if (txtDisplay.Text == string.Empty)
                        txtDisplay.Text = strDisplay;
                    else
                        txtDisplay.Text = txtDisplay.Text + Environment.NewLine + strDisplay;
                }
               
            }

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "*.txt|*.txt";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(dlg.FileName, txtDisplay.Text);

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtdistance.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
    }
}
