using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RailwayReservation.DataAccessClass;
using NUnit.Framework.Legacy;
using Moq;

namespace RailwayReservation.Test
{
    [TestFixture]
    public class AdminDataAccessTest
    {
        private CancellationDataAccess da;

        [SetUp]
        public void ArrangeObjects()
        {
            da = new CancellationDataAccess();
        }

        [Test]
        public void Login_WithValidCredentials_ReturnsTrue()
        {
            // Arrange
            var adminDataAccess = new AdminDataAccess();
            string username = "admin";
            string password = "admin123";

            // Act
            bool result = adminDataAccess.Login(username, password);

            // Assert
            ClassicAssert.IsTrue(result, "Login should return true for valid credentials.");
        }

        [Test]
        public void Login_WithInvalidCredentials_ReturnsFalse()
        {
            // Arrange
            var adminDataAccess = new AdminDataAccess();
            string username = "admin";
            string password = "wrongpass";

            // Act
            bool result = adminDataAccess.Login(username, password);

            // Assert
            ClassicAssert.IsFalse(result, "Login should return false for invalid credentials.");
        }


        [Test]
        public void CancelByUser_WithValidBookingId1_ReturnsExpectedRefund()
        {
            // Act
            decimal refund = da.CancelByUser(1);

            // Assert
            ClassicAssert.AreEqual(80.00m, refund);
        }


    }
}
