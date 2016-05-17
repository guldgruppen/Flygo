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
        private SøgFlyOpgaveHandler handler;

        [TestInitialize]

        public void BeforeEachTest()
        {
            handler = new SøgFlyOpgaveHandler();
        }


        [TestMethod]
        public void FlyopgaveNummerIsNullOrEmpty()
        {
            string Flyopgavenummer = "";
            DateTime datetime1 = DateTime.Now;

            Assert.ThrowsException<NullOrEmptyException>((() => handler.SearchForFlyopgave(Flyopgavenummer, datetime1)));

        }


        [TestMethod]
        public void FlyopgaveNummerIsNorCorrect()
        {
            string Flyopgavenummer = "aa";
            DateTime dato = DateTime.Parse("2016-06-03");

            Assert.ThrowsException<InfoWrongException>((() => handler.SearchForFlyopgave(Flyopgavenummer, dato)));
        }



        [TestMethod]
        public void FlyopgaveNummerDatoForkert()
        {
            string FlyopgaveNummer = "Test";
            DateTime dato = DateTime.Parse("2016-05-01");

            Assert.ThrowsException<DateWrongException>((() => handler.SearchForFlyopgave(FlyopgaveNummer, dato)));
        }
    }

}
