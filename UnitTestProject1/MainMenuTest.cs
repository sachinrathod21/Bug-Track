using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bug_Tracker;

namespace BugsTests
{
    /// <summary>
    /// Summary description for MainMenuTest
    /// </summary>
    [TestClass]
    public class MainMenuTest
    {

        /// <summary>
        /// should pass because the number of bugs in project id 9 is 2.
        /// </summary>
        [TestMethod]
        public void Project_bugsummry_pass()
        {
            MainMenu form = new MainMenu("Sachin", "2");
            int actual;
            int expected;

            form.projectid = 9;
            form.loadbugsummry();
            actual = form.MaxRows;
            expected = 2;

            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///should pass the fail test because the actual number of bugs in project id 2 is 2.
        /// </summary>
        [TestMethod]
        public void Project_bugsummry_fail()
        {
            MainMenu form = new MainMenu("Sachin", "2");
            int actual;
            int expected;

            form.projectid = 2;
            form.loadbugsummry();
            actual = form.MaxRows;
            expected = 1;

            Assert.AreNotEqual(expected, actual);

        }

        /// <summary>
        ///should pass because the number of projects in the table is 21 at the moment
        /// </summary>
        [TestMethod]
        public void MainMenu_table_load()
        {
            MainMenu form = new MainMenu("Sachin", "2");
            int actual;
            int expected;

            form.LoadTable();
            actual = form.rowcount;
            expected = 21;

            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// should pass the fail test because the first field is empty.
        /// </summary>
        [TestMethod]
        public void Project_insert_fail()
        {
            MainMenu form = new MainMenu("Sachin", "2");
            bool actual;
            bool expected;
            form.textBox2.Text = "";
            form.textBox1.Text = "TEST";
            form.comboBox1.Text = "TEST";

            actual = form.insert();
            expected = false;

            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// should pass the test because the field values are in accordance with the code and database
        /// </summary>
        [TestMethod]
        public void Project_insert_pass()
        {
            MainMenu form = new MainMenu("Sachin", "2");
            bool actual;
            bool expected;
            form.textBox2.Text = "2";
            form.textBox1.Text = "test";
            form.comboBox1.Text = "test";

            actual = form.insert();
            expected = true;

            Assert.AreEqual(expected, actual);

        }

    }
}
