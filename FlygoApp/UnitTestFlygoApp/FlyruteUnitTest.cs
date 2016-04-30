using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyGoWebService.Models;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace UnitTestFlygoApp
{
    [TestClass]
    public class FlyruteUnitTest
    {
        FlyRute flyrute;

        [TestInitialize]
        public void InitTest()
        {
            flyrute = new FlyRute();

        }

        [TestMethod]
        public void TestMethod1()
        {
            string nummer = "";
            Assert.ThrowsException<ArgumentException>(() => flyrute.CheckFlyruteNummer(nummer));
        }
    }
}
