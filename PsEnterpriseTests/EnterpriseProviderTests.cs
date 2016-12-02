using PsEnterprise;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Management.Automation;

namespace PsEnterpriseTests
{
    
    
    /// <summary>
    ///This is a test class for EnterpriseProviderTest and is intended
    ///to contain all EnterpriseProviderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EnterpriseProviderTests
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        /// A valid path should return true
        ///</summary>
        [TestMethod()]
        [DeploymentItem("PsEnterprise.dll")]
        public void IsValidPath_valid_path_supplied()
        {
            //
            // arrange
            //

            string path = @"BOEDEV:\\InfoObjects\Root Folder";
            bool expected = true;
            bool actual;

            //
            // act
            //

            EnterpriseProvider_Accessor target = new EnterpriseProvider_Accessor();
            actual = target.IsValidPath(path);

            //
            // assert
            //

            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// An invalid path should return false
        ///</summary>
        [TestMethod()]
        [DeploymentItem("PsEnterprise.dll")]
        public void IsValidPath_invalid_path_supplied()
        {

            //
            // arrange
            //

            string path = @"BOEDEV:\\\\";
            bool expected = false;
            bool actual;

            //
            // act
            //
            EnterpriseProvider_Accessor target = new EnterpriseProvider_Accessor();
            actual = target.IsValidPath(path);

            //
            // assert
            //

            Assert.AreEqual(expected, actual);
        
        }

        /// <summary>
        ///A test for NewDrive
        ///</summary>
        [TestMethod()]
        [DeploymentItem("PsEnterprise.dll")]
        public void NewDriveTest()
        {

            //
            // arrange
            //

            EnterpriseProvider_Accessor target = new EnterpriseProvider_Accessor(); // TODO: Initialize to an appropriate value
            //EnterpriseProvider target = new EnterpriseProvider();
            ProviderInfo providerInfo = null;
            string name = "BOEDEV";
            string description = "BOE Enterprise";
            string root = "BOEDEV:\\";
            PSCredential credential = null;

            PSDriveInfo drive = new PSDriveInfo(name, providerInfo, root, description, credential);
            PSDriveInfo actual;

            //
            // act
            //

            actual = target.NewDrive(drive);

            //
            // assert
            //

            Assert.AreEqual(name, actual.Name);
            Assert.AreEqual(description, actual.Description);
            Assert.AreEqual(root, actual.Root);
            Assert.AreEqual(credential, actual.Credential);

            Assert.IsInstanceOfType(actual,typeof(EnterpriseDriveInfo));

        }

        /// <summary>
        ///A test for RemoveDrive
        ///</summary>
        [TestMethod()]
        [DeploymentItem("PsEnterprise.dll")]
        public void RemoveDriveTest()
        {

            //
            // arrange
            //

            EnterpriseProvider_Accessor target = new EnterpriseProvider_Accessor(); // TODO: Initialize to an appropriate value
            PSDriveInfo drive = null;
            PSDriveInfo expected = null;
            PSDriveInfo actual;

            //
            // act
            //

            actual = target.RemoveDrive(drive);

            //
            // assert
            //

            Assert.AreEqual(expected, actual);

        }
    }
}
