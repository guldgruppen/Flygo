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
        [TestMethod]
        public void TestMethod2()
        {
            DateTime fra = DateTime.Now;
            DateTime til = DateTime.Now;
            Assert.ThrowsException<ArgumentException>(() => flyrute.CheckAfgangAnkomst(til,fra));
        }

        [TestMethod]
        public void TestMethod3()
        {
            int id = -1;
            Assert.ThrowsException<IndexOutOfRangeException>(() => flyrute.CheckFlyId(id));
        }



        [TestMethod]
        public void TestMethod4()
        {
            int id = -1;
            Assert.ThrowsException<IndexOutOfRangeException>(() => flyrute.CheckHangarId(id));
        }
        [TestMethod]
        public void TestMethod5()
        {
            DateTime fra = DateTime.Now.AddDays(-4);
            DateTime til = DateTime.Now.AddDays(-1);
            Assert.ThrowsException<ArgumentException>(() => flyrute.CheckAfgangAnkomst(til, fra));
        }
    }
}
