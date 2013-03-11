namespace DateTimeExperiments
{
    using System;
    using System.Collections.Generic;

    using NodaTime;
    using NodaTime.TimeZones;

    /// <summary>
    /// Functions to Convert To and From UTC
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// The date format for display/compare
        /// </summary>
        public const string Format = "yyyy-MM-dd HH:mm:ss.ffffff";

        /// <summary>
        /// The eastern U.S. time zone
        /// </summary>
        internal static readonly NodaTime.DateTimeZone BclEast = NodaTime.DateTimeZoneProviders.Bcl.GetZoneOrNull("Eastern Standard Time");
                
        public static readonly TimeZoneInfo EasternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        internal static readonly NodaTime.DateTimeZone TzEast = NodaTime.DateTimeZoneProviders.Tzdb.GetZoneOrNull("America/New_York");

        internal static readonly ZoneLocalMappingResolver CustomResolver = Resolvers.CreateMappingResolver(Resolvers.ReturnLater, Resolvers.ReturnStartOfIntervalAfter);

        public static DateTime GetUtc(DateTime ts)
        {
            return TimeZoneInfo.ConvertTimeToUtc(EasternTimeZone.IsInvalidTime(ts) ? ts.AddHours(1.0) : ts, EasternTimeZone);
        }

        public static DateTime GetUtcTz(DateTime ts)
        {
            var local = LocalDateTime.FromDateTime(ts);
            var zdt = TzEast.ResolveLocal(local, CustomResolver);
            return zdt.ToDateTimeUtc();            
        }

        public static DateTime GetUtcBcl(DateTime ts)
        {
            var local = LocalDateTime.FromDateTime(ts);
            var zdt = BclEast.ResolveLocal(local, CustomResolver);
            return zdt.ToDateTimeUtc();
        }

        public TimeZoneInfo CreateEasternTimeZoneWithCompleteAdjustmentRules()
        {
            return null;
        }

        public static bool IsDaylightSavingTime(DateTime ts)
        {
            var local = LocalDateTime.FromDateTime(ts);
            var zdt = Utils.TzEast.ResolveLocal(local, Utils.CustomResolver);
            return zdt.IsDaylightSavingTime();
        }

        /// <summary>
        /// Determines whether [is daylight saving time using custom time zone] [the specified ts].
        /// </summary>
        /// <param name="ts">The ts.</param>
        /// <returns>
        ///   <c>true</c> if [is daylight saving time using custom time zone] [the specified ts]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDaylightSavingTimeUsingCustomTimeZone(DateTime ts)
        {
            var tz = CreateCustomTimeZoneInfoWithDstRules();
            return tz.IsDaylightSavingTime(ts);
        }

        /// <summary>
        /// Creates the custom time zone info with DST rules.
        /// </summary>
        /// <returns>The custom time zone.</returns>
        public static TimeZoneInfo CreateCustomTimeZoneInfoWithDstRules()
        {
            // Clock is adjusted one hour forward or backward
            var delta = new TimeSpan(1, 0, 0);

            //This will hold all the DST adjustment rules
            var listOfAdjustments = new List<TimeZoneInfo.AdjustmentRule>();

            /*
                Rule	US	1918	1919	-	Mar	lastSun	2:00	1:00	D
                Rule	US	1918	1919	-	Oct	lastSun	2:00	0	S             
             */
            var ruleStart = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, 2, 0, 0), 03, 05, DayOfWeek.Sunday);
            var ruleEnd = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, 2, 0, 0), 10, 05, DayOfWeek.Sunday);
            var adjustment = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(new DateTime(1918, 1, 1), new DateTime(1919, 12, 31), delta, ruleStart, ruleEnd);

            listOfAdjustments.Add(adjustment);

            /*
                Rule	US	1942	only	-	Feb	9	2:00	1:00	W # War 
             */
            ruleStart = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, 2, 0, 0), 02, 09);
            adjustment = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(new DateTime(1942, 1, 1), new DateTime(1942, 12, 31),
                                                                       delta, ruleStart, ruleEnd);
            listOfAdjustments.Add(adjustment);

            /*
                Rule	US	1945	only	-	Aug	14	23:00u	1:00	P # Peace
                Rule	US	1945	only	-	Sep	30	2:00	0	S
             */
            ruleStart = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, 23, 0, 0), 08, 14);
            ruleEnd = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, 2, 0, 0), 09, 30);
            adjustment = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(new DateTime(1945, 1, 1), new DateTime(1945, 12, 31),
                                                                       delta, ruleStart, ruleEnd);
            listOfAdjustments.Add(adjustment);

            /*
                Rule	US	1967	2006	-	Oct	lastSun	2:00	0	S             
             */
            ruleEnd = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, 2, 0, 0), 10, 5, DayOfWeek.Sunday);

            /*
                Rule	US	1967	1973	-	Apr	lastSun	2:00	1:00	D
             */
            ruleStart = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, 2, 0, 0), 04, 05, DayOfWeek.Sunday);
            adjustment = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(new DateTime(1967, 1, 1), new DateTime(1973, 12, 31),
                                                                       delta, ruleStart, ruleEnd);
            listOfAdjustments.Add(adjustment);

            /*
                Rule	US	1974	only	-	Jan	6	2:00	1:00	D
             */
            ruleStart = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, 2, 0, 0), 01, 06);
            adjustment = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(new DateTime(1974, 1, 1), new DateTime(1974, 12, 31),
                                                                       delta, ruleStart, ruleEnd);
            listOfAdjustments.Add(adjustment);


            /*
                Rule	US	1975	only	-	Feb	23	2:00	1:00	D             
             */
            ruleStart = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, 2, 0, 0), 02, 23);
            adjustment = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(new DateTime(1975, 1, 1), new DateTime(1975, 12, 31),
                                                                       delta, ruleStart, ruleEnd);
            listOfAdjustments.Add(adjustment);

            /*
                Rule	US	1976	1986	-	Apr	lastSun	2:00	1:00	D             
             */
            ruleStart = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, 2, 0, 0), 04, 05, DayOfWeek.Sunday);
            adjustment = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(new DateTime(1976, 1, 1), new DateTime(1986, 12, 31),
                                                                       delta, ruleStart, ruleEnd);
            listOfAdjustments.Add(adjustment);

            /*
                Rule	US	1987	2006	-	Apr	Sun>=1	2:00	1:00	D             
             */
            ruleStart = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, 2, 0, 0), 04, 01, DayOfWeek.Sunday);
            adjustment = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(new DateTime(1987, 1, 1), new DateTime(2006, 12, 31),
                                                                       delta, ruleStart, ruleEnd);
            listOfAdjustments.Add(adjustment);

            /*
                Rule	US	2007	max	-	Mar	Sun>=8	2:00	1:00	D
                Rule	US	2007	max	-	Nov	Sun>=1	2:00	0	S             
             */
            ruleStart = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, 2, 0, 0), 03, 02, DayOfWeek.Sunday);
            ruleEnd = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, 2, 0, 0), 11, 01, DayOfWeek.Sunday);
            adjustment = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(new DateTime(2007, 1, 1), DateTime.MaxValue.Date,
                                                                       delta, ruleStart, ruleEnd);
            listOfAdjustments.Add(adjustment);
            
            var adjustments = new TimeZoneInfo.AdjustmentRule[listOfAdjustments.Count];
            listOfAdjustments.CopyTo(adjustments);

            return TimeZoneInfo.CreateCustomTimeZone("Custom Eastern Standard Time", new TimeSpan(-5, 0, 0),
                  "(GMT-05:00) Eastern Time (US Only)", "Eastern Standard Time", "Eastern Daylight Time", adjustments);
        }
    }
}
