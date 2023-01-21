using System;
using System.Data;
using Data;

/// <summary>
/// Summary description for TimetableManager
/// </summary>
public class TimetableManager
{

    public static int UnmapBlock(TimeSpan start)
    {
        return start.Hours;
    }

    public static TimeSpan MapBlock(int id)
    {
        return TimeSpan.FromHours(id);
    }

    public static DateTime UnmapEventStart(DateTime start)
    {
        return UnmapEventStart(start.Date, start.TimeOfDay);
    }

    public static DateTime UnmapEventStart(DateTime day, TimeSpan time)
    {
        int block = UnmapBlock(time);
        return UnmapEventStart(day, block);
    }

    public static DateTime UnmapEventStart(DateTime day, int block)
    {
        DataRow dr = new DataManager().GetBlock(block);
        if (dr == null)
        {
            throw new Exception("Can't map event start (block not found).");
        }
        DateTime blockStart = (DateTime)dr["BlockStart"];
        return day.Date.Add(blockStart.TimeOfDay);
    }

    public static DateTime UnmapEventEnd(DateTime end)
    {
        return UnmapEventEnd(end.Date, end.TimeOfDay);
    }

    public static DateTime UnmapEventEnd(DateTime day, TimeSpan time)
    {
        int block = UnmapBlock(time) - 1;
        return UnmapEventEnd(day, block);
    }

    public static DateTime UnmapEventEnd(DateTime day, int block)
    {
        DataRow dr = new DataManager().GetBlock(block);
        if (dr == null)
        {
            throw new Exception("Can't map event start (block not found).");
        }
        DateTime blockEnd = (DateTime)dr["BlockEnd"];
        return day.Date.Add(blockEnd.TimeOfDay);
    }

}