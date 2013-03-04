namespace DateTimeExperiments
{
    using System;
    using System.IO;

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
        private static readonly TimeZoneInfo EasternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        private static readonly NodaTime.DateTimeZone TzEast = NodaTime.DateTimeZoneProviders.Tzdb.GetZoneOrNull("America/New_York");
        private static readonly NodaTime.DateTimeZone BclEast = NodaTime.DateTimeZoneProviders.Bcl.GetZoneOrNull("Eastern Standard Time");

        private static readonly ZoneLocalMappingResolver CustomResolver = Resolvers.CreateMappingResolver(Resolvers.ReturnLater, Resolvers.ReturnStartOfIntervalAfter);

        /// <summary>
        /// Writes out the UTC converted values.
        /// </summary>
        public void ListUtcConvertedValues()
        {
            var current = new DateTime(1900, 1, 1);

            using (System.IO.TextWriter writer = new StreamWriter("C:\\temp\\dstNoda.csv"))
            {
                writer.WriteLine(CompareUtcValues(null));

                while (current.Year < 2015)
                {
                    writer.WriteLine(CompareUtcValues(current));

                    current = current.AddDays(1);
                }
            }
        }

        private static string CompareUtcValues(DateTime? ts)
        {
            string result = "ts,utc,Bcl,Tz,utc-bcl,utc-tz,bcl-tz";

            if (ts.HasValue)
            {
                var utc = GetUtc(ts.Value);
                var nodaTzUtc = GetUtcTz(ts.Value);
                var nodaBclUtc = GetUtcBcl(ts.Value);

                result = string.Format(
                    "{0},{1},{2},{3},{4},{5},{6}",
                    ts.Value.ToString(Format),
                    utc.ToString(Format),
                    nodaBclUtc.ToString(Format),
                    nodaTzUtc.ToString(Format),
                    utc.ToString(Format) == nodaBclUtc.ToString(Format),
                    utc.ToString(Format) == nodaTzUtc.ToString(Format),
                    nodaBclUtc.ToString(Format) == nodaTzUtc.ToString(Format));
            }

            return result;
        }

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
    }
}
