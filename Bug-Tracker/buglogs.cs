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
    public partial class buglogs : Form
    {
        string conString;
        int bugsid; //used to pass bug id in to the SQL statement
        public int rowcount;

        public buglogs(int bugid)
        {
            bugsid = bugid;
            InitializeComponent();
            loadtable();
        }

        /// <summary>
        /// Loads the tables and display them in a datagrid view
        /// </summary>
        private void loadtable()
        {
            conString = Properties.Settings.Default.EmployeesConnectionString;
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM bughistory  Where bugid = '" + bugsid + "'", conString);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            label2.Text = dataTable.Rows.Count.ToString();
            rowcount = dataTable.Rows.Count;
        }
    }


}
