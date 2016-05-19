using System;
using FlygoApp.Models;
using FlyGoWebService.Models;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace UnitTestFlygoApp
{
    [TestClass]
    public class FlyopgaveUnitTest
    {
        Flyopgave _flyopgave;

        [TestInitialize]
        public void InitTest()
        {
            _flyopgave = new Flyopgave();
        }

        [TestMethod]
        public void TestFlyopgaveNummer()
        {
            string nummer = "";
            Assert.ThrowsException<ArgumentException>(() => _flyopgave.CheckFlyopgaveNummer(nummer));
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
            Assert.ThrowsException<ArgumentException>(() => _flyopgave.CheckAfgangAnkomst(til,fra));
        }

        [TestMethod]
        public void TestFlyId()
        {
            int id = -1;
            Assert.ThrowsException<IndexOutOfRangeException>(() => _flyopgave.CheckFlyId(id));
        }



        [TestMethod]
        public void TestHangarId()
        {
            int id = -1;
            Assert.ThrowsException<IndexOutOfRangeException>(() => _flyopgave.CheckHangarId(id));
        }
        [TestMethod]
        public void TestFlyopgaveAfgangAnkomstFørNu()
        {
            DateTime fra = DateTime.Now.AddDays(-4);
            DateTime til = DateTime.Now.AddDays(-1);
            Assert.ThrowsException<ArgumentException>(() => _flyopgave.CheckAfgangAnkomst(til, fra));
        }
        

    }
}
