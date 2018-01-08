using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Bug_Tracker
{
    public partial class Form1 : Form
    {
        DatabaseConnection objConnect;
        string conString;
        DataSet ds;
        DataRow dRow;

        int MaxRows;
        int inc = 0;

        public Form1()
        {
            InitializeComponent();
            LoadTable();
        }

        /// <summary>
        /// this method loads the data in the table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                 objConnect = new DatabaseConnection();
                 conString = Properties.Settings.Default.EmployeesConnectionString;

                 objConnect.connection_string = conString;
                 objConnect.Sql = Properties.Settings.Default.SQL;
                                
                ds = objConnect.GetConnection;
                MaxRows = ds.Tables[0].Rows.Count;

                NavigateRecords();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        /// <summary>
        /// used to populate the text boxes with the correct information.
        /// </summary>
        private void NavigateRecords()
        {
            dRow = ds.Tables[0].Rows[inc];
            txtFirstName.Text = dRow.ItemArray.GetValue(1).ToString();
            txtSurname.Text = dRow.ItemArray.GetValue(2).ToString();
            txtJobTitle.Text = dRow.ItemArray.GetValue(3).ToString();
            txtDepartment.Text = dRow.ItemArray.GetValue(4).ToString();
            txtUsername.Text = dRow.ItemArray.GetValue(5).ToString();
            txtPassword.Text = dRow.ItemArray.GetValue(6).ToString();
        }

        /// <summary>
        /// pulls the latest data from my Users database, and populates the data grid view.
        /// </summary>
        private void LoadTable()
        {
            conString = Properties.Settings.Default.EmployeesConnectionString;
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM tbl_employees", conString);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.CurrentCell = this.dataGridView1[0, inc];
        }

        /// <summary>
        /// clears the text Boxes, and shows the save button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNew_Click_1(object sender, EventArgs e)
        {
            txtFirstName.Clear();
            txtSurname.Clear();
            txtJobTitle.Clear();
            txtDepartment.Clear();
            txtUsername.Clear();
            txtPassword.Clear();

            btnAddNew.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            groupBox1.Visible = true;
            btnAddNew.Visible = false;
        }

        /// <summary>
        ///  will enable the buttons back to the correct view, if a insert new record is cancelled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigateRecords();
            btnCancel.Enabled = false;
            btnSave.Enabled = false;
            btnAddNew.Enabled = true;
            groupBox1.Visible = false;
            btnAddNew.Visible = true;
        }

        /// <summary>
        ///  used to insert the text box vaules in to a new row in the users table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables[0].NewRow();
            row[1] = txtFirstName.Text;
            row[2] = txtSurname.Text;
            row[3] = txtJobTitle.Text;
            row[4] = txtDepartment.Text;
            row[5] = txtUsername.Text;
            row[6] = txtPassword.Text;

            ds.Tables[0].Rows.Add(row);

            try
            {
                objConnect.UpdateDatabase(ds);
                MaxRows = MaxRows + 1;
                inc = MaxRows - 1;
                LoadTable();
                MessageBox.Show("Database updated.");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            btnCancel.Enabled = false;
            btnSave.Enabled = false;
            btnAddNew.Enabled = true;
            groupBox1.Visible = false;
            btnAddNew.Visible = true;

        }

        /// <summary>
        /// populates the Navigates records metheod with the correct variable information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            inc = 0;
            inc = Convert.ToInt32(dataGridView1.CurrentRow.Index.ToString());
            NavigateRecords();
        }

        /// <summary>
        /// Updates selected User Info with updated information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUdate_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables[0].Rows[inc];
            row[1] = txtFirstName.Text;
            row[2] = txtSurname.Text;
            row[3] = txtJobTitle.Text;
            row[4] = txtDepartment.Text;
            row[5] = txtUsername.Text;
            row[6] = txtPassword.Text;
            try
            {
                objConnect.UpdateDatabase(ds);
                MessageBox.Show("User Details Updated.");

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            LoadTable();
        }

        /// <summary>
        /// deletes a user in the user table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds.Tables[0].Rows[inc].Delete();
                objConnect.UpdateDatabase(ds);
                MaxRows = ds.Tables[0].Rows.Count;
                LoadTable();
                NavigateRecords();
                MessageBox.Show("User Deleted.");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
