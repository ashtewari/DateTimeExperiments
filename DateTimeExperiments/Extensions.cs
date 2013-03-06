// -----------------------------------------------------------------------
// <copyright file="Extensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DateTimeExperiments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NodaTime;

    /// <summary>
    /// Extension Methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// http://stackoverflow.com/questions/15211052/what-is-the-system-timezoneinfo-isdaylightsavingtime-equivalent-in-nodatime
        /// </summary>
        /// <param name="zonedDateTime"></param>
        /// <returns>true if the specified ZonedDateTime is in DST.</returns>
        public static bool IsDaylightSavingTime(this ZonedDateTime zonedDateTime)
        {
            var instant = zonedDateTime.ToInstant();
            var zoneInterval = zonedDateTime.Zone.GetZoneInterval(instant);
            return zoneInterval.Savings != Offset.Zero;
        }
    }
}
