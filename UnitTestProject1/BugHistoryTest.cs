using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bug_Tracker;
namespace BugsTests
{
    /// <summary>
    /// Summary description for BugHistoryTest
    /// </summary>
    [TestClass]
    public class BugHistoryTest
    {
        /// <summary>
        ///should pass because there are 4 rows of History data associated with the bug id 3.
        /// </summary>
        [TestMethod]
        public void Bughistory_pull_table_pass()
        {
            buglogs form = new buglogs(15);
            int actule;
            int expected;
            actule = form.rowcount;
            expected = 4;

            Assert.AreEqual(expected, actule);

        }

    }
}
