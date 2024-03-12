namespace DateScheduler_MAUI.Model;

public class CalendarDate(DateTime date) : ICalendarDate
{
    public DateTime Date { get; } = date;
}