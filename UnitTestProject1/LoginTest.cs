using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bug_Tracker;
namespace BugsTests
{
    [TestClass]
    public class LoginTest
    {
        /// <summary>
        ///should pass because to there are login details with the username dp, and Password dp
        /// </summary>
        [TestMethod]
        public void login_test_pass()
        {
            Login form = new Login();
            bool actual;
            bool expected;

            form.textBox2.Text = "dp";
            form.textBox1.Text = "dp";
            actual = form.login();
            expected = true;

            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// should pass the fail test because the login details are incorrect            
        /// </summary>
        [TestMethod]
        public void login_test_fail()
        {
            Login form = new Login();
            bool actual;
            bool expected;
            form.textBox2.Text = "admin";
            form.textBox1.Text = "password";

            actual = form.login();
            expected = false;

            Assert.AreEqual(expected, actual);

        }
    }
}
