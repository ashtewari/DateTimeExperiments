// -----------------------------------------------------------------------
// <copyright file="TestRunner.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DateTimeExperiments
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TestRunner
    {
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
                var utc = Utils.GetUtc(ts.Value);
                var nodaTzUtc = Utils.GetUtcTz(ts.Value);
                var nodaBclUtc = Utils.GetUtcBcl(ts.Value);

                result = string.Format(
                    "{0},{1},{2},{3},{4},{5},{6}",
                    ts.Value.ToString(Utils.Format),
                    utc.ToString(Utils.Format),
                    nodaBclUtc.ToString(Utils.Format),
                    nodaTzUtc.ToString(Utils.Format),
                    utc.ToString(Utils.Format) == nodaBclUtc.ToString(Utils.Format),
                    utc.ToString(Utils.Format) == nodaTzUtc.ToString(Utils.Format),
                    nodaBclUtc.ToString(Utils.Format) == nodaTzUtc.ToString(Utils.Format));
            }

            return result;
        }
    }
}
