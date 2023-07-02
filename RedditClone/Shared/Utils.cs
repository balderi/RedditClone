using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditClone.Shared
{
    public static class Utils
    {
        public static string FormatTime(DateTime dt)
        {
            var span = DateTime.UtcNow.Subtract(dt);
            if(span.TotalDays > 365)
            {
                var value = (int)(span.TotalDays / 365);
                var time = value == 1 ? "year" : "years";
                return $"{value} {time} ago";
            }
            if(span.TotalDays > 30)
            {
                var value = (int)(span.TotalDays / 30);
                var time = value == 1 ? "month" : "months";
                return $"{value} {time} ago";
            }
            if(span.TotalHours > 24)
            {
                var value = (int)(span.TotalDays);
                var time = value == 1 ? "day" : "days";
                return $"{value} {time} ago";
            }
            if(span.TotalMinutes > 60)
            {
                var value = (int)(span.TotalHours);
                var time = value == 1 ? "hour" : "hours";
                return $"{value} {time} ago";
            }
            if(span.TotalMinutes >= 2)
            {
                var value = (int)(span.TotalMinutes);
                var time = value == 1 ? "minute" : "minutes";
                return $"{value} {time} ago";
            }
            if(span.TotalSeconds > 60)
            {
                return "1 minute ago";
            }
            return "less than a minute ago";
        }
    }
}
