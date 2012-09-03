using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PresentationGenerator.Core.CommandHandlers;
using PresentationGenerator.Core.Commands;
using PresentationGenerator.Core.Documents;
using PresentationGenerator.Core.Entities;
using PresentationGenerator.Core.Repositories;
using PresentationGenerator.Core.Utility;

namespace PresentationGenerator.Test.CommandHandlers
{
    /// <summary>
    /// Summary description for RegisterNewUserCommandHandlerTests
    /// </summary>
    [TestClass]
    public class MakePresentationCommandHandlerTests
    {
        public MakePresentationCommandHandlerTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void WhenCommandIsReceived_PresentationIsAddedToRepository()
        {
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            var logger = new Mock<ILogger>();
            var handler = new MakePresentationCommandHandler(logger.Object, presentationRepositoryMock.Object);
            handler.Handle(new MakePresentationCommand("testuser","presentations/1", new List<Page>(), "Test title", DateTime.Now ));
            presentationRepositoryMock.Verify(x => x.Add(It.IsAny<Presentation>()), Times.Once());
        }
    }
}