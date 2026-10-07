using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Waypoints.Infrastructure.Converters;

public class UtcDateTimeConverter() : ValueConverter<DateTime, DateTime>(
    v => v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : v,
    v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
{
}