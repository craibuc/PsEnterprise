using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security;
//using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

using PsEnterprise;
using CrystalDecisions.Enterprise;

namespace PsEnterpriseTests
{
    [TestClass]
    public class RepositoryTests
    {
        // values from settings
        private string server = Properties.Settings.Default.Server;
        private string authentication = Properties.Settings.Default.Authentication;
        private string username = Properties.Settings.Default.Username;
        private SecureString securePassword = Properties.Settings.Default.SecurePassword.DecryptString();

        private string token = null;

        // constructor
        public RepositoryTests()
        {
        }

        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            token = Repository.GetToken(username, securePassword.ToInsecureString(), server, authentication);
        }
        
        [TestMethod]
        public void GetToken_Should_Return_A_Token()
        {

            // arrange
            string actual = null;

            // act
            actual = token;

            // assert
            Assert.IsNotNull(actual);

        }

        [TestMethod]
        public void GetSession_Should_Return_A_Session()
        {
            // arrange
            EnterpriseSession actual = null;

            // act
            actual = Repository.GetSession(token);

            // assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(EnterpriseSession));

        }

        [TestMethod]
        public void GetInfoObjects_Should_Return_A_Collection()
        {

            // arrange
            EnterpriseSession session = Repository.GetSession(token);
            InfoObjects actual = null;
            string query = "SELECT TOP 5 si_id, si_name FROM ci_infoobjects WHERE si_kind='CrystalReport'";

            // act
            actual = Repository.GetInfoObjects(session, query);

            // assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(InfoObjects));
        }

        [TestMethod]
        public void ResolveUriQuery_Should_Return_An_Array_Of_Queries()
        {

            // arrange
            EnterpriseSession session = Repository.GetSession(token);
            string uri = "path://InfoObjects/Root Folder";
            string[] actual = null;
            string expected = "SELECT TOP 200 static,SI_CUID,SI_PARENT_CUID FROM CI_INFOOBJECTS WHERE (SI_PARENTID IN (4) AND SI_NAME='Root Folder') AND ((SI_ID>='23')) ORDER BY SI_ID";
            
            // act
            actual = Repository.ResolveUriQuery(session, uri);

            // assert

            // one resolved path was created
            Assert.AreEqual(1, actual.ToArray().Length);
            // text matches expected value
            Assert.AreEqual(expected, actual.ToArray()[0]);

        }

    } // class

} //namespace
