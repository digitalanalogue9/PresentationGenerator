using PresentationGenerator.Core.Views.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raven.Client;
using PresentationGenerator.Core.Repositories;
using PresentationGenerator.Core.Views.Models.Input;
using PresentationGenerator.Core.Views.Models.Output;
using StructureMap;

namespace PresentationGenerator.Test
{
    /// <summary>
    ///This is a test class for GetMyPresentationsViewFactoryTest and is intended
    ///to contain all GetMyPresentationsViewFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GetMyPresentationsViewFactoryTest : BaseTestIntegration
    {
        /// <summary>
        ///A test for Load
        ///</summary>
        [TestMethod()]
        public void LoadTest()
        {
            //Arrange
            var documentstore = ObjectFactory.GetInstance<IDocumentStore>();
            var presentationRepository = ObjectFactory.GetInstance<IPresentationRepository>();
            var target = new GetMyPresentationsViewFactory(documentstore, presentationRepository); // TODO: Initialize to an appropriate value
            var input = new PresentationListInputModel {UserId = "users/1"};
            //Arrange
            var actual = target.Load(input);
            //Assert
            Assert.AreEqual(3, actual.Presentations.Count);
        }
    }
}
