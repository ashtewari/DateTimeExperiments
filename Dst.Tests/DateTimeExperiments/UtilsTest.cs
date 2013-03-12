// <copyright file="UtilsTest.cs">Copyright ©  2013</copyright>
using System;
using DateTimeExperiments;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework.Generated;

namespace DateTimeExperiments
{
    /// <summary>This class contains parameterized unit tests for Utils</summary>
    [PexClass(typeof(Utils))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class UtilsTest
    {
        /// <summary>Test stub for IsDaylightSavingTimeUsingCustomTimeZone(DateTime)</summary>
        [PexMethod]
        public bool IsDaylightSavingTimeUsingCustomTimeZone(DateTime ts)
        {
            bool result = Utils.IsDaylightSavingTimeUsingCustomTimeZone(ts);
            return result;
            // TODO: add assertions to method UtilsTest.IsDaylightSavingTimeUsingCustomTimeZone(DateTime)
        }
        [PexMethod]
        public DateTime GetUtc(DateTime ts)
        {
            DateTime result = Utils.GetUtc(ts);
            return result;
            // TODO: add assertions to method UtilsTest.GetUtc(DateTime)
        }
        [TestMethod]
        public void GetUtc769()
        {
            DateTime dt;
            dt = this.GetUtc(default(DateTime));
            Assert.AreEqual<int>(1, dt.Day);
            Assert.AreEqual<DayOfWeek>(DayOfWeek.Monday, dt.DayOfWeek);
            Assert.AreEqual<int>(1, dt.DayOfYear);
            Assert.AreEqual<int>(5, dt.Hour);
            Assert.AreEqual<DateTimeKind>(DateTimeKind.Utc, dt.Kind);
            Assert.AreEqual<int>(0, dt.Millisecond);
            Assert.AreEqual<int>(0, dt.Minute);
            Assert.AreEqual<int>(1, dt.Month);
            Assert.AreEqual<int>(0, dt.Second);
            Assert.AreEqual<int>(1, dt.Year);
        }
    }
}
