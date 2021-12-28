using api.Util.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Util
{
    [TestClass]
    public class UtilTest
    {
        [TestMethod]
        public void ToDescriptionString_WithDescriptionAttributte_ShowDescriptionAttibutte()
        {
            //Arrange
            string expected = "UN";
            
            //Act
            string actual = UnityOfMeasurement.Unity.ToDescriptionString();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToDescriptionString_WithoutDescriptionAttributte_ShowEnumName()
        {
            //Arrange
            string expected = "Milligram";

            //Act
            string actual = UnityOfMeasurement.Milligram.ToDescriptionString();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}