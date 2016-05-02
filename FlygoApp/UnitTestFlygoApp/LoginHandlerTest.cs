using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.Exceptions;
using FlygoApp.Models;
using FlygoApp.Views;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace UnitTestFlygoApp
{
    [TestClass]
    public class LoginHandlerTest
    {
        private LoginHandler handler;

        [TestInitialize]
        public void BeforeEachTest()
        {
            handler = new LoginHandler();
        }

        [TestMethod]
        public void CheckLoginInfo_No_Name_Test()
        {
           
            string brugernavn = "";
            string kodeord = "1234";


            Assert.ThrowsException<NullOrEmptyException>((() => handler.CheckLoginInfo(brugernavn, kodeord))); 
        }

        [TestMethod]
        public void CheckLoginInfo_No_Password_Test()
        {
           
            string brugernavn = "Ole Henriksen";
            string kodeord = "";

            Assert.ThrowsException<NullOrEmptyException>((() => handler.CheckLoginInfo(brugernavn, kodeord))); 
        }

        [TestMethod]
        public void CheckLoginInfo_No_Pass_And_Name_Test()
        {
            string brugernavn = "";
            string kodeord = "";

            Assert.ThrowsException<NullOrEmptyException>((() => handler.CheckLoginInfo(brugernavn, kodeord))); 

        }

        [TestMethod]
        public void CheckLoginInfo_Wrong_Pass_Or_Name_Test()
        {
            string brugernavn = "Ole Lukøje";
            string kodeord = "123";

            Assert.ThrowsException<LoginInfoWrongException>((() => handler.CheckLoginInfo(brugernavn, kodeord))); 
        }

       
    }
}
