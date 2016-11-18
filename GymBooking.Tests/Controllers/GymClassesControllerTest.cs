using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GymBooking;
using GymBooking.Controllers;

namespace GymBooking.Tests.Controllers {
    [TestClass]
    public class GymClassesControllerTest {
        [TestMethod]
        public void Index() {
            // Arrange
            GymClassesController controller = new GymClassesController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        
    }
}
