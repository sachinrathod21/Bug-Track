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
    public partial class MainMenu : Form
    {
        //database connections
        DatabaseConnection objConnect;
        DatabaseConnection objConnect1;
        DatabaseConnection objConnect2;

        //declaring datasets
        string conString;
        DataSet d;
        DataSet ds;
        DataSet ds1;
        DataSet ds2;
        Form1 Userspop1;

        //declaring variables
        public int MaxRows;
        int Openbugs;
        int Closedbugs;
        int inc = 0;
        public int projectid = 0;
        string id1;
        public int rowcount;

        /// <summary>
        /// setting username and user id to values
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>

        public MainMenu(string name, string id)
        {
            InitializeComponent();
            label2.Text = name;
            LoadTable();
            textBox2.Text = id;
            textBox3.Text = name;
            id1 = id;
            loadbugsummry();

            //User Admin button only visible to user 1, that is admin
            if (id == "1")
            {
                button1.Visible = true;
            }
        }
        /// <summary>
        /// user table appears in new form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button1_Click(object sender, EventArgs e)
        {
            Userspop1 = new Form1();
            Userspop1.Show();
        }

        /// <summary>
        /// This will load the tables, and display them in a datagrid view
        /// </summary>

        public void LoadTable()
        {
            conString = Properties.Settings.Default.EmployeesConnectionString;
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM projects Inner Join tbl_employees on projects.UserID=tbl_employees.id", conString);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.ClearSelection();
            this.dataGridView1.CurrentCell = this.dataGridView1[2, inc];
            dataGridView1.CurrentCell.Selected = true;
            dataGridView1.BeginEdit(true);
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["userid"].Visible = false;
            dataGridView1.Columns["id1"].Visible = false;
            dataGridView1.Columns["password"].Visible = false;
            dataGridView1.Columns["username"].Visible = false;

            objConnect = new DatabaseConnection();
            objConnect.connection_string = conString;
            objConnect.Sql = "SELECT * FROM projects";
            d = objConnect.GetConnection;

            rowcount = dataTable.Rows.Count;
        }


        /// <summary>
        /// loads the correct bug summary information when the project is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            projectid = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
            loadbugsummry();
        }

        /// <summary>
        /// Loads a summary view of the bug
        /// </summary>
        public void loadbugsummry()
        {

            conString = Properties.Settings.Default.EmployeesConnectionString;

            objConnect = new DatabaseConnection();
            objConnect.connection_string = conString;
            objConnect.Sql = "SELECT * FROM bugs  Where projectid='" + projectid + "'";
            ds = objConnect.GetConnection;
            MaxRows = ds.Tables[0].Rows.Count;

            objConnect1 = new DatabaseConnection();
            objConnect1.connection_string = conString;
            objConnect1.Sql = "SELECT * FROM bugs  Where projectid='" + projectid + "' and status='Open'";
            ds1 = objConnect1.GetConnection;
            Openbugs = ds1.Tables[0].Rows.Count;

            objConnect2 = new DatabaseConnection();
            objConnect2.connection_string = conString;
            objConnect2.Sql = "SELECT * FROM bugs  Where projectid='" + projectid + "' and status='Close'";
            ds2 = objConnect2.GetConnection;
            Closedbugs = ds2.Tables[0].Rows.Count;

            //MessageBox.Show(MaxRows.ToString());
            totalnobug.Text = MaxRows.ToString();
            openbugs.Text = Openbugs.ToString();
            closebugs.Text = Closedbugs.ToString();
        }

        /// <summary>
        /// when a project is double clicked,
        /// it sends the Project id to the Bugs Form and also loads the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int projectid = 0;
            projectid = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
            Bugs Userspops;
            Userspops = new Bugs(projectid);
            Userspops.Show();

        }

        /// <summary>
        ///insert new projects into the Projects Table.
        /// </summary>
        /// <returns></returns>
        public bool insert()
        {
            string userid = textBox2.Text;
            string title = textBox1.Text;
            string type = comboBox1.Text;

            if (title.Length > 0 && type.Length > 0 && userid.Length > 0)
            {
                objConnect = new DatabaseConnection();
                objConnect.connection_string = conString;
                objConnect.Sql = "SELECT * FROM projects";
                d = objConnect.GetConnection;

                DataRow row = d.Tables[0].NewRow();

                row[1] = textBox2.Text;
                row[2] = textBox1.Text;
                row[3] = comboBox1.Text;

                d.Tables[0].Rows.Add(row);

                try
                {
                    objConnect.UpdateDatabase(d);
                    LoadTable();
                    MessageBox.Show("Database updated."); //message box to confirm addition of project
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message); //Error message pop-up         
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// inserts new projects into the Projects Table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            insert();
        }

        /// <summary>
        /// loads all the projects when the view all projects button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            LoadTable();
            projectid = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
            loadbugsummry();
        }

        /// <summary>
        /// loads all the projects the user has cerated ONLY.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            conString = Properties.Settings.Default.EmployeesConnectionString;
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM projects Where userid='" + id1 + "'", conString);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.CurrentCell = this.dataGridView1[2, inc];
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["userid"].Visible = false;
            projectid = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
            loadbugsummry();
        }

        /// <summary>
        /// Quits application when exit button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
