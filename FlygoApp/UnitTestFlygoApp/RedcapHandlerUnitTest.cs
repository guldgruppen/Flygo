using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.Exceptions;
using FlygoApp.Models;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace UnitTestFlygoApp
{
    [TestClass]
    public class RedcapHandlerUnittest
    {
        private RedcapHandler handler;

        [TestInitialize]

        public void BeforeEachTest()
        {
            handler = new RedcapHandler();
        }


        [TestMethod]
        public void FlyruteNummerIsNullOrEmpty()
        {
            string Flyrutenummer = "";
            DateTime datetime1 = DateTime.Now;

            Assert.ThrowsException<NullOrEmptyException>((() => handler.SearchForFlyRute(Flyrutenummer, datetime1)));

        }


        [TestMethod]
        public void FlyruteNummerIsNorCorrect()
        {
            string Flyrutenummer = "aa";
            DateTime dato = DateTime.Parse("2016-06-03");

            Assert.ThrowsException<InfoWrongException>((() => handler.SearchForFlyRute(Flyrutenummer, dato)));
        }



        [TestMethod]
        public void FlyruteNummerDatoForkert()
        {
            string FlyruteNummer = "Test";
            DateTime dato = DateTime.Parse("2016-05-01");

            Assert.ThrowsException<DateWrongException>((() => handler.SearchForFlyRute(FlyruteNummer, dato)));
        }
    }

}
