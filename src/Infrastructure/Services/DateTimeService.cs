using SmartRef.Application.Common.Interfaces;

namespace SmartRef.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
