using System.Diagnostics;
using System.Reflection;

namespace IndFusion.Exxerpro.Domain.Models;

public class DateTimeMachine(TimeProvider? timeProvider = null) : IDateTimeMachine
{
    private readonly TimeProvider _timeProvider = timeProvider ?? TimeProvider.System;

    public void AdvanceTime(TimeSpan timeSpan)
    {
#if DEBUG
        try
        {


            var timeProviderType = _timeProvider.GetType();
            if (timeProviderType.Name == "FakeTimeProvider")
            {
                var advanceMethod = timeProviderType.GetMethod("Advance");
                if (advanceMethod != null)
                {
                    advanceMethod.Invoke(_timeProvider, new object[] { timeSpan });
                }
                else
                {
                    // Log a warning when the method is not found
                    Trace.TraceWarning("The method 'Advance' was not found on the FakeTimeProvider.");

                    // Handle the case where the method is not found
                    throw new InvalidOperationException("The method 'Advance' was not found on the FakeTimeProvider.");
                }
            }
        }
        catch (TargetInvocationException ex)
        {
            // Log a warning when the method is not found
            Trace.TraceWarning($"An error occurred while advancing time: {ex.Message}");

            // Handle exceptions thrown by the invoked method
            throw new InvalidOperationException("An error occurred while advancing time.", ex);
        }
        catch (ArgumentException ex)
        {
            // Log a warning when an error occurs during method invocation
            Trace.TraceWarning($"An invalid argument was provided to the 'Advance' method: {ex.Message}");

            // Handle invalid argument exceptions
            throw new InvalidOperationException("An invalid argument was provided to the 'Advance' method.", ex);
        }
        catch (Exception ex)
        {
            // Log a warning for any other unexpected exceptions
            Trace.TraceWarning($"An unexpected error occurred while advancing time: {ex.Message}");

            // Handle any other unexpected exceptions
            throw new InvalidOperationException("An unexpected error occurred while advancing time.", ex);
        }
#else
    // Production code, if any
            Trace.TraceWarning($"feature not supported on production:");
#endif
    }

    public System.DateTime Now => _timeProvider.GetUtcNow().DateTime;
    public int Hour => _timeProvider.GetUtcNow().Hour;
    public int Minute => _timeProvider.GetUtcNow().Minute;
    public int Second => _timeProvider.GetUtcNow().Second;
    public int Year => _timeProvider.GetUtcNow().Year;
    public int Month => _timeProvider.GetUtcNow().Month;
    public int Day => _timeProvider.GetUtcNow().Day;
    public int DayOfYear => _timeProvider.GetUtcNow().DayOfYear;
    public TimeProvider? TimeProvider { get; set; }

    public DateTimeOffset GetLocalNow()
    {
        return _timeProvider.GetLocalNow();
    }

    public DateTimeOffset GetUtcNow()
    {
        return _timeProvider.GetUtcNow();
    }

    public DateTimeOffset AddDays(DateTimeOffset dateTime, int days)
    {
        return dateTime.AddDays(days);
    }

    public DateTimeOffset AddHours(DateTimeOffset dateTime, int hours)
    {
        return dateTime.AddHours(hours);
    }

    public DateTimeOffset AddMinutes(DateTimeOffset dateTime, int minutes)
    {
        return dateTime.AddMinutes(minutes);
    }

    public DateTimeOffset AddSeconds(DateTimeOffset dateTime, int seconds)
    {
        return dateTime.AddSeconds(seconds);
    }

    public DateTimeOffset AddMilliseconds(DateTimeOffset dateTime, double milliseconds)
    {
        return dateTime.AddMilliseconds(milliseconds);
    }

    public DateTimeOffset AddTicks(DateTimeOffset dateTime, long ticks)
    {
        return dateTime.AddTicks(ticks);
    }

    public DateTimeOffset AddMonths(DateTimeOffset dateTime, int months)
    {
        return dateTime.AddMonths(months);
    }

    public DateTimeOffset AddYears(DateTimeOffset dateTime, int years)
    {
        return dateTime.AddYears(years);
    }

    public TimeSpan GetTimeSpan(DateTimeOffset from, DateTimeOffset to)
    {
        return to - from;
    }

    public int GetDay(DateTimeOffset dateTime)
    {
        return dateTime.Day;
    }

    public int GetMonth(DateTimeOffset dateTime)
    {
        return dateTime.Month;
    }

    public int GetYear(DateTimeOffset dateTime)
    {
        return dateTime.Year;
    }

    public DayOfWeek GetDayOfWeek(DateTimeOffset dateTime)
    {
        return dateTime.DayOfWeek;
    }

    public int GetDayOfYear(DateTimeOffset dateTime)
    {
        return dateTime.DayOfYear;
    }
}