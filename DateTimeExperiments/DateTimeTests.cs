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
        /// Compares the custom time zone info with default.
        /// </summary>
        [TestMethod]
        public void CompareCustomTimeZoneInfoWithDefault()
        {
            var ts = new DateTime(1980, 4, 15, 12, 0, 0);
            
            var isDstDefault = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").IsDaylightSavingTime(ts);
            var isDstCustom = Utils.CreateCustomTimeZoneInfoWithDstRules().IsDaylightSavingTime(ts);

            Assert.IsTrue(isDstDefault);
            Assert.IsFalse(isDstCustom);
        }

        /// <summary>
        /// Test is day light savings time using noda time.
        /// </summary>
        [TestMethod]
        public void Test_NodaTime_IsDayLightSavingTime()
        {
            var ts = new DateTime(1980, 4, 15, 12, 0, 0);
            
            var isDst = Utils.IsDaylightSavingTime(ts);
            
            Assert.IsFalse(isDst);
        }

        [TestMethod]
        // Out of DST (According to 1987 rules, DST would have started on 4/5/1970, but it started on 4/26/1970 according to rules actually in place in 1970.)
        public void Test_1970_Out_DST()
        {
            //// var dateString = "1970-04-15 12:00:00.000000";
            var expectedValue = "1970-04-15 17:00:00.000000";

            var ts = new DateTime(1970, 4, 15, 12, 0, 0);            

            var actualValue = Utils.GetUtcTz(ts).ToString(Utils.Format);

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void AddAdjustmentRulesToEasternTimeZone()
        {
            var rules = Utils.EasternTimeZone.GetAdjustmentRules();
            
            foreach (var rule in rules)
            {
                Console.WriteLine(string.Format("{0} [{2}] {1} : {3} > {4}", rule.DateStart, rule.DateEnd, rule.DaylightDelta, rule.DaylightTransitionStart, rule.DaylightTransitionEnd)); 
            }

            var isDst = false;
            isDst = Utils.EasternTimeZone.IsDaylightSavingTime(new DateTime(1980, 3, 31));
            isDst = Utils.EasternTimeZone.IsDaylightSavingTime(new DateTime(1980, 4, 15));
            isDst = Utils.EasternTimeZone.IsDaylightSavingTime(new DateTime(1980, 4, 30));

            isDst = Utils.IsDaylightSavingTime(new DateTime(1980, 3, 31));
            isDst = Utils.IsDaylightSavingTime(new DateTime(1980, 4, 15));
            isDst = Utils.IsDaylightSavingTime(new DateTime(1980, 4, 30));

            var etz = Utils.CreateCustomTimeZoneInfoWithDstRules();
            isDst = etz.IsDaylightSavingTime(new DateTime(1980, 3, 31));
            isDst = etz.IsDaylightSavingTime(new DateTime(1980, 4, 15));
            isDst = etz.IsDaylightSavingTime(new DateTime(1980, 4, 30));

        }        
    }
}
