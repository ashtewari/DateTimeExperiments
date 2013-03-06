namespace DateTimeExperiments
{
    using System;

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
                
        internal static readonly TimeZoneInfo EasternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

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
    }
}
