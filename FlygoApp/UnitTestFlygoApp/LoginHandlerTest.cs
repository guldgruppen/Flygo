using FlygoApp.Exceptions;
using FlygoApp.Models;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace UnitTestFlygoApp
{
    [TestClass]
    public class LoginHandlerTest
    {
        private LoginHandler _handler;

        [TestInitialize]
        public void BeforeEachTest()
        {
            _handler = new LoginHandler();
        }

        [TestMethod]
        public void CheckLoginInfo_No_Name_Test()
        {
           
            string brugernavn = "";
            string kodeord = "1234";


            Assert.ThrowsException<NullOrEmptyException>((() => _handler.CheckLoginInfo(brugernavn, kodeord))); 
        }

        [TestMethod]
        public void CheckLoginInfo_No_Password_Test()
        {
           
            string brugernavn = "Ole Henriksen";
            string kodeord = "";

            Assert.ThrowsException<NullOrEmptyException>((() => _handler.CheckLoginInfo(brugernavn, kodeord))); 
        }

        [TestMethod]
        public void CheckLoginInfo_No_Pass_And_Name_Test()
        {
            string brugernavn = "";
            string kodeord = "";

            Assert.ThrowsException<NullOrEmptyException>((() => _handler.CheckLoginInfo(brugernavn, kodeord))); 

        }

        [TestMethod]
        public void CheckLoginInfo_Wrong_Pass_Or_Name_Test()
        {
            string brugernavn = "Ole Lukøje";
            string kodeord = "123";

            Assert.ThrowsException<InfoWrongException>((() => _handler.CheckLoginInfo(brugernavn, kodeord))); 
        }

       
    }
}
