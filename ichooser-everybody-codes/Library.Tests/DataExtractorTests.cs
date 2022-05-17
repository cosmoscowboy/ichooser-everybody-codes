using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Library.Tests
{
    [TestClass]
    public class DataExtractorTests
    {
        [TestMethod]
        public void Can_Extract_Data_From_CSV_File()
        {
            var csvFilename = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"Data\cameras-defb.csv");
            IDataExtractor dataExtractor = new CSVDataExtractor(csvFilename);

            var cameraLocations = dataExtractor.GetData<Models.CameraLocation>();

            Assert.IsNotNull(cameraLocations);
            Assert.IsTrue(cameraLocations.Count > 0);

        }
    }
}