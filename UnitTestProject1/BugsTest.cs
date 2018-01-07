using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bug_Tracker;
namespace BugsTests
{
    /// <summary>
    /// Summary description for BugsTest
    /// </summary>
    [TestClass]
    public class BugsTest
    {

        /// <summary>
        /// should pass because there are two rows of code/file data associated with project id 9.
        /// </summary>
        [TestMethod]
        public void Project_Pullcode_table_pass()
        {
            Bugs form = new Bugs(9);
            int actual;
            int expected;

            actual = form.codecount;
            expected = 2;

            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// should pass because there are two rows of bug data associated with project id 9.
        /// </summary>
        [TestMethod]
        public void Project_bugs_table_pass()
        {
            Bugs form = new Bugs(9);
            int actual;
            int expected;

            actual = form.bugscount;
            expected = 2;

            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// should pass because there are 3 rows of text in the linecounter method.
        /// </summary>
        [TestMethod]
        public void Project_bugs_linecounter()
        {
            Bugs form = new Bugs(2);
            int actual;
            int expected;

            form.richTextBox1.Text = "Test" + "\n" + "\n";
            form.linecount();
            actual = form.linecounter1;
            expected = 3;

            Assert.AreEqual(expected, actual);

        }
    }
}
