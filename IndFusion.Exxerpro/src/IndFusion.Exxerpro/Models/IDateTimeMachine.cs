namespace IndFusion.Exxerpro.Models;

public interface IDateTimeMachine
{
    void AdvanceTime(TimeSpan timeSpan);
    System.DateTime Now { get; }
    int Hour { get; }
    int Minute { get; }
    int Second { get; }
    int Year { get; }
    int Month { get; }
    int Day { get; }
    int DayOfYear { get; }
    TimeProvider? TimeProvider { get; set; }
    DateTimeOffset GetLocalNow();
    DateTimeOffset GetUtcNow();
    DateTimeOffset AddDays(DateTimeOffset dateTime, int days);
    DateTimeOffset AddHours(DateTimeOffset dateTime, int hours);
    DateTimeOffset AddMinutes(DateTimeOffset dateTime, int minutes);
    DateTimeOffset AddSeconds(DateTimeOffset dateTime, int seconds);
    DateTimeOffset AddMilliseconds(DateTimeOffset dateTime, double milliseconds);
    DateTimeOffset AddTicks(DateTimeOffset dateTime, long ticks);
    DateTimeOffset AddMonths(DateTimeOffset dateTime, int months);
    DateTimeOffset AddYears(DateTimeOffset dateTime, int years);
    TimeSpan GetTimeSpan(DateTimeOffset from, DateTimeOffset to);
    int GetDay(DateTimeOffset dateTime);
    int GetMonth(DateTimeOffset dateTime);
    int GetYear(DateTimeOffset dateTime);
    DayOfWeek GetDayOfWeek(DateTimeOffset dateTime);
    int GetDayOfYear(DateTimeOffset dateTime);
}