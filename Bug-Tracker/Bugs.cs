using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Bug_Tracker
{
    public partial class Bugs : Form
    {
        DatabaseConnection objConnect;
        string conString;
        DataSet ds1;
        DataSet ds2;
        DataSet ds;

        int MaxRows;
        int inc;
        int inc1; int inc2;

        int project_id;
        public int codecount;
        public int bugscount;
        public int linecounter1;

        String date = DateTime.Now.ToString("dd.MM.yyyy"); //Date format

        public Bugs(int projectid)
        {
            InitializeComponent();
            project_id = projectid;
            LoadTable();
        }

        /// <summary>
        /// Loads the table and data, when this method is called, it refreshes the table
        /// </summary>
        private void LoadTable()
        {
            conString = Properties.Settings.Default.EmployeesConnectionString;
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM bugs  Where projectid = '" + project_id + "'", conString);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

            SqlDataAdapter dataAdapter1 = new SqlDataAdapter("SELECT * FROM codetb  Where projectid = '" + project_id + "'", conString);
            DataTable dataTable1 = new DataTable();
            dataAdapter1.Fill(dataTable1);
            dataGridView2.DataSource = dataTable1;
            codecount = dataTable1.Rows.Count;
            bugscount = dataTable.Rows.Count;

            comboBox2.Items.Clear();
            for (int i = 0; i < dataTable1.Rows.Count; i++)
                comboBox2.Items.Add(dataTable1.Rows[i][2].ToString());

            dataGridView2.Columns["code"].Visible = false;
            dataGridView2.Columns["id"].Visible = false;
            dataGridView2.Columns["projectid"].Visible = false;

            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["projectid"].Visible = false;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string code;
            code = Convert.ToString(dataGridView2.CurrentRow.Cells["code"].Value);
            richTextBox1.Text = code;
            linecount();
        }

        /// <summary>
        /// when the save button (button 3) is clicked it saves the code page to the code table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            objConnect = new DatabaseConnection();
            conString = Properties.Settings.Default.EmployeesConnectionString;

            objConnect.connection_string = conString;
            objConnect.Sql = "SELECT * FROM codetb";

            ds1 = objConnect.GetConnection;

            DataRow row = ds1.Tables[0].NewRow();
            row[1] = richTextBox1.Text.ToString();
            row[2] = textBox1.Text;
            row[3] = project_id.ToString();

            ds1.Tables[0].Rows.Add(row);

            try
            {
                MaxRows = ds1.Tables[0].Rows.Count;
                objConnect.UpdateDatabase(ds1);
                MaxRows = MaxRows + 1;
                inc = MaxRows - 2;
                LoadTable();
                MessageBox.Show("Database updated."); //confirmation message
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message); //error message
            }

        }

        /// <summary>
        /// when the save button (button 6) is clicked it saves the bug to the bug table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            objConnect = new DatabaseConnection();
            conString = Properties.Settings.Default.EmployeesConnectionString;

            objConnect.connection_string = conString;
            objConnect.Sql = "SELECT * FROM bugs";

            ds1 = objConnect.GetConnection;

            DataRow row = ds1.Tables[0].NewRow();
            row[1] = project_id.ToString();
            row[2] = comboBox2.Text.ToString();
            row[3] = comboBox1.Text.ToString();
            row[4] = textBox2.Text.ToString();
            row[5] = textBox3.Text.ToString();
            row[6] = richTextBox2.Text.ToString();

            ds1.Tables[0].Rows.Add(row);

            try
            {
                MaxRows = ds1.Tables[0].Rows.Count;
                objConnect.UpdateDatabase(ds1);
                MaxRows = MaxRows + 1;
                inc1 = MaxRows - 2;
                LoadTable();
                MessageBox.Show("Database updated."); //confirmation message

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message); //error message
            }
        }

        /// <summary>
        /// when the data in datagrid view is clicked it populates the correct fields
        /// and also populates the code into the richtext box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            richTextBox1.SelectionColor = System.Drawing.Color.Black;
            richTextBox1.SelectionBackColor = System.Drawing.Color.White;

            DataRow dRow;
            objConnect = new DatabaseConnection();
            conString = Properties.Settings.Default.EmployeesConnectionString;

            objConnect.connection_string = conString;
            objConnect.Sql = "SELECT * FROM bugs  Where projectid = '" + project_id + "'";

            ds1 = objConnect.GetConnection;

            inc2 = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);
            inc = Convert.ToInt32(dataGridView1.CurrentRow.Index.ToString());
            dRow = ds1.Tables[0].Rows[inc];
            comboBox2.Text = dRow.ItemArray.GetValue(2).ToString();
            comboBox1.Text = dRow.ItemArray.GetValue(3).ToString();
            textBox2.Text = dRow.ItemArray.GetValue(4).ToString();
            textBox3.Text = dRow.ItemArray.GetValue(5).ToString();
            richTextBox2.Text = dRow.ItemArray.GetValue(6).ToString();

            int Rows;
            Rows = dataGridView2.Rows.Count;

            string page;
            page = Convert.ToString(dataGridView1.CurrentRow.Cells["codepgid"].Value);

            string test;
            for (int i = 0; i < Rows; i++)
            {
                dataGridView2.Rows[i].Cells[2].Selected = false;
                test = dataGridView2.Rows[i].Cells[2].Value.ToString();
                if (page == test)
                {

                    dataGridView2.Rows[i].Cells[2].Selected = true;
                    string code;
                    code = Convert.ToString(dataGridView2.Rows[i].Cells["code"].Value);
                    richTextBox1.Text = code;
                    linecount();
                }
            }
           
             richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine((Convert.ToInt32(dRow.ItemArray.GetValue(4)) - 1)), (richTextBox1.GetFirstCharIndexFromLine(Convert.ToInt32(dRow.ItemArray.GetValue(5)))) - (richTextBox1.GetFirstCharIndexFromLine((Convert.ToInt32(dRow.ItemArray.GetValue(4)) - 1))));
           
            //Sets the selected text fore and background color
            richTextBox1.SelectionColor = System.Drawing.Color.White;
            richTextBox1.SelectionBackColor = System.Drawing.Color.Blue;
        }


        /// <summary>
        /// when a cell is dobble clicked it opens up the Bug History form, 
        ///and passes thew the Bug id so it can be used in a SQL statement in that form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int bugid;
            bugid = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
            buglogs Userspops;
            Userspops = new buglogs(bugid);
            Userspops.Show();
        }

        /// <summary>
        /// Updates the data in the bugs table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            objConnect = new DatabaseConnection();
            conString = Properties.Settings.Default.EmployeesConnectionString;

            objConnect.connection_string = conString;
            objConnect.Sql = "SELECT * FROM bugs  Where Id='" + inc2.ToString() + "'";

            ds = objConnect.GetConnection;

            DataRow Row = ds.Tables[0].Rows[0];
            Row[3] = comboBox1.Text;
            Row[4] = textBox2.Text;
            Row[5] = textBox3.Text;
            Row[6] = richTextBox2.Text;

            try
            {
                objConnect.UpdateDatabase(ds);

                objConnect = new DatabaseConnection();
                conString = Properties.Settings.Default.EmployeesConnectionString;

                objConnect.connection_string = conString;
                objConnect.Sql = "SELECT * FROM bughistory";

                ds2 = objConnect.GetConnection;

                DataRow row = ds2.Tables[0].NewRow();
                row[1] = inc2;
                row[2] = date;
                row[3] = comboBox1.Text;
                row[4] = richTextBox2.Text;

                ds2.Tables[0].Rows.Add(row);
                objConnect.UpdateDatabase(ds2);

                MessageBox.Show("Bug Details Updated."); //confirmation message

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message); //error message
            }
            LoadTable();
        }

        /// <summary>
        ///this method counts the lines displayed in richtextbox1 
        ///and then adds the line count to the RichTextlineCount Box
        /// </summary>
        public void linecount()
        {
            int linecounter;

            linecounter = richTextBox1.Lines.Count();
            richTextlinecount.Text = "";
            for (int i = 1; i <= linecounter; i++)
            {
                richTextlinecount.Text += i.ToString() + "\n";
            }

            linecounter1 = linecounter;
        }

        /// <summary>
        /// This method helps sync the scrolls bars on richtextbox1 and linecount richtextbox
        ///it works by getting the position and vaules of richtextbox1 and puts
        ///the same vaules on the Linecount Richtextbox
        /// </summary>
        public enum ScrollBarType : uint
        {
            SbHorz = 0,
            SbVert = 1,
            SbCtl = 2,
            SbBoth = 3
        }

        public enum Message : uint
        {
            WM_VSCROLL = 0x0115
        }

        public enum ScrollBarCommands : uint
        {
            SB_THUMBPOSITION = 4
        }

        [DllImport("User32.dll")]
        public extern static int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        private void richTextBox1_VScroll_1(object sender, EventArgs e)
        {
            int nPos = GetScrollPos(richTextBox1.Handle, (int)ScrollBarType.SbVert);
            nPos <<= 16;
            uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;
            SendMessage(richTextlinecount.Handle, (int)Message.WM_VSCROLL, new IntPtr(wParam), new IntPtr(0));
        }

        private void Bugs_Load(object sender, EventArgs e)
        {

        }
    }
}