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


namespace Bug_Tracker
{

    public partial class Login : Form
    {
        //Declaring variables
        MainMenu Userspops;
        string conString;
        SqlConnection mySqlConnection;


        public Login()
        {
            InitializeComponent();
            textBox1.UseSystemPasswordChar = true;
        }

        /// <summary>
        /// Quits application when exit button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Logs in when submit button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            login();
        }

        /// <summary>
        /// Shows password when the checkbox is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.UseSystemPasswordChar = false;
            }
            else { textBox1.UseSystemPasswordChar = true; }
        }

        /// <summary>
        /// the login method checks the username and password for a match in the database, if a row is retuned then acsses is granted.
        /// else it will show an error message prompting the user to check their login details.
        /// </summary>
        /// <returns></returns>
        public bool login()
        {
            conString = Properties.Settings.Default.EmployeesConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            SqlDataAdapter sdaa = new SqlDataAdapter("Select first_name,Id from tbl_employees Where username='" + textBox2.Text + "' and password='" + textBox1.Text + "'", conn);
            DataTable dt = new System.Data.DataTable();
            sdaa.Fill(dt);



            if (dt.Rows.Count == 1)
            {
                Userspops = new MainMenu(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString());
                Userspops.Show();
                Hide();
                return true;
            }
            else
            {
                MessageBox.Show("Please Check Login Details");
                return false;
            }

        }
        /// <summary>
        ///  this allows the user to press the Enter Key when in the password box, 
        ///  so you do not have to click login to submit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login();
            }

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
