using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherStation.Server.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStation.Server.Service.Tests
{
    [TestClass()]
    public class SensorFusionEstimatorTests
    {
        [TestMethod()]
        public void EstimateTest()
        {
            Assert.AreEqual(36.42,SensorFusionEstimator.Estimate(36.36,0.3,37.09,1));
        }
    }
}