using log4net.Core;
using log4net.Layout;

namespace FundMate.Web.Logging;

/// <summary>
/// Emits the logging event's timestamp as UTC (DateTime.Kind = Utc).
/// log4net's built-in <see cref="log4net.Layout.RawTimeStampLayout"/> emits
/// DateTime.Kind = Local, which Npgsql 6+ rejects when writing to a
/// PostgreSQL "timestamp with time zone" column (the "Logs" table's
/// "Timestamp" column), causing AdoNetAppender to fail on every insert.
/// </summary>
public class RawUtcTimeStampLayout : IRawLayout
{
    public object Format(LoggingEvent loggingEvent) => loggingEvent.TimeStampUtc;
}
