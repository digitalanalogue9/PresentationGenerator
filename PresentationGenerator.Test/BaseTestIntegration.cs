using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresentationGenerator.Core.DependencyResolution;
using PresentationGenerator.Core.Documents;
using PresentationGenerator.Core.Repositories;
using RavenGallery.Core.Documents;
using StructureMap;

namespace PresentationGenerator.Test
{
    [TestClass]
    public class BaseTestIntegration
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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            Bootstrapper.InMemoryStartup();
            var userrepository = ObjectFactory.GetInstance<IUserRepository>();
            var presentationrepository = ObjectFactory.GetInstance<IPresentationRepository>();
            userrepository.Add(userrepository.Create(new UserDocument { Id = "users/1", PasswordHash = "test", Username = "testuser1" }));
            userrepository.Add(userrepository.Create(new UserDocument { Id = "users/2", PasswordHash = "test", Username = "testuser2" }));

            presentationrepository.Add(presentationrepository.Create(GetTestPresentation("presentations/1", "users/1")));
            presentationrepository.Add(presentationrepository.Create(GetTestPresentation("presentations/2", "users/1")));
            presentationrepository.Add(presentationrepository.Create(GetTestPresentation("presentations/3", "users/1")));

            presentationrepository.Add(presentationrepository.Create(GetTestPresentation("presentations/4", "users/2")));
            presentationrepository.Add(presentationrepository.Create(GetTestPresentation("presentations/5", "users/2")));
            presentationrepository.Add(presentationrepository.Create(GetTestPresentation("presentations/6", "users/2")));

        }

        private PresentationDocument GetTestPresentation(string id, string userid)
        {
            var presentationdoc = new PresentationDocument
                                      {
                                          Id = id,
                                          LastModified = new DateTime(2012, 1, 1, 11, 0, 0),
                                          Title = "Presentation 1",
                                          UserId = userid,
                                          Pages = new List<Page>
                                                      {
                                                          new Page
                                                              {
                                                                  Id = "1",
                                                                  Content = "<div>Content</div>",
                                                                  CSS = "step",
                                                                  Rotate = "0",
                                                                  Scale = "0",
                                                                  X = "0",
                                                                  Y = "0",
                                                                  Z = "0"
                                                              }
                                                      }
                                      };
            return presentationdoc;
        }

        private void AddPresentations()
        {
            throw new NotImplementedException();
        }

        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            Bootstrapper.Shutdown(false);
        }
        //
        #endregion
        
    }
}