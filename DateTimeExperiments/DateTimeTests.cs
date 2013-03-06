using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DateTimeExperiments
{
    using NodaTime;

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

        /// <summary>
        /// Is the specified datetime DST?
        /// </summary>
        [TestMethod]
        public void DST_Started_On_April_27_1980()
        {
            var ts = new DateTime(1980, 4, 15, 12, 0, 0);

            var isDst = Utils.EasternTimeZone.IsDaylightSavingTime(ts);

            Assert.IsFalse(isDst);
        }


        /// <summary>
        /// Test is day light savings time using noda time.
        /// </summary>
        [TestMethod]
        public void Test_NodaTime_IsDayLightSavingTime()
        {
            var ts = new DateTime(1980, 4, 15, 12, 0, 0);

            var local = LocalDateTime.FromDateTime(ts);
            var zdt = Utils.TzEast.ResolveLocal(local, Utils.CustomResolver);

            Assert.IsFalse(zdt.IsDaylightSavingTime());
        }

        [TestMethod]
        // Out of DST (According to 1987 rules, DST would have started on 4/5/1970, but it started on 4/26/1970 according to rules actually in place in 1970.)
        public void Test_1970_Out_DST()
        {
            var dateString = "1970-04-15 12:00:00.000000";
            var expectedValue = "1970-04-15 17:00:00.000000";

            var ts = new DateTime(1970, 4, 15, 12, 0, 0);            

            var actualValue = Utils.GetUtcTz(ts).ToString(Utils.Format);

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
