using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.Models;
using FlyGoWebService.Models;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace UnitTestFlygoApp
{
    [TestClass]
    public class FlyopgaveUnitTest
    {
        Flyopgave Flyopgave;

        [TestInitialize]
        public void InitTest()
        {
            Flyopgave = new Flyopgave();
        }

        [TestMethod]
        public void TestFlyopgaveNummer()
        {
            string nummer = "";
            Assert.ThrowsException<ArgumentException>(() => Flyopgave.CheckFlyopgaveNummer(nummer));
        }

        [TestMethod]
        public void TestFlyOpgaveNummerWrong()
        {
            FlyopgaveHandler h = new FlyopgaveHandler();

            string nummer = "AAA123";
            Assert.ThrowsException<ArgumentException>((() => h.Add(DateTimeOffset.Now.AddDays(1), DateTimeOffset.Now, 2, 2, nummer))); 
        }

        [TestMethod]
        public void TestFlyopgaveAfgangAnkomstDif()
        {
            DateTime fra = DateTime.Now;
            DateTime til = DateTime.Now;
            Assert.ThrowsException<ArgumentException>(() => Flyopgave.CheckAfgangAnkomst(til,fra));
        }

        [TestMethod]
        public void TestFlyId()
        {
            int id = -1;
            Assert.ThrowsException<IndexOutOfRangeException>(() => Flyopgave.CheckFlyId(id));
        }



        [TestMethod]
        public void TestHangarId()
        {
            int id = -1;
            Assert.ThrowsException<IndexOutOfRangeException>(() => Flyopgave.CheckHangarId(id));
        }
        [TestMethod]
        public void TestFlyopgaveAfgangAnkomstFørNu()
        {
            DateTime fra = DateTime.Now.AddDays(-4);
            DateTime til = DateTime.Now.AddDays(-1);
            Assert.ThrowsException<ArgumentException>(() => Flyopgave.CheckAfgangAnkomst(til, fra));
        }
        

    }
}
