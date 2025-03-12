using System;
using System.Collections.Generic;

namespace UFID_Reader.Services;

public interface IDateTimeService
{
    string GetCurrentDay();
    TimeSpan GetCurrentTime();
}

public class DateTimeService : IDateTimeService
{
    public string GetCurrentDay()
    {
        var dayMap = new Dictionary<string, string>
        {
            {"Sunday", "U"},
            {"Monday", "M"},
            {"Tuesday", "T"},
            {"Wednesday", "W"},
            {"Thursday", "R"},
            {"Friday", "F"},
            {"Saturday", "S"}
        };
        
        var currentDay = DateTime.Now.DayOfWeek.ToString();
        return dayMap[currentDay];  
    }

    public TimeSpan GetCurrentTime()
    {
        return DateTime.Now.TimeOfDay;
    }
}