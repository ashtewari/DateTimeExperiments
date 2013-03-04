using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DateTimeExperiments
{
    [TestClass]
    public class DateTimeTests
    {
        [TestMethod]
        public void Test_Invalid_Date()
        {
            var ts = new DateTime(2013, 3, 10, 2, 15, 45);

            // Convert to UTC using System.TimeZoneInfo
            var utc = Utils.GetUtc(ts).ToString(Utils.Format);

            // Convert to UTC using NodaTime (Tzdb/Olson dataabase)
            var utcNodaTime = Utils.GetUtcTz(ts).ToString(Utils.Format);

            Assert.AreEqual(utc, utcNodaTime);
        }
    }
}
