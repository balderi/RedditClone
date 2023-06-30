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
                return $"{(int)(span.TotalDays / 365)} years ago";
            }
            if(span.TotalDays > 30)
            {
                return $"{(int)(span.TotalDays / 30)} months ago";
            }
            if(span.TotalHours > 24)
            {
                return $"{(int)(span.TotalDays)} days ago";
            }
            if(span.TotalMinutes > 60)
            {
                return $"{(int)(span.TotalHours)} hours ago";
            }
            if(span.TotalMinutes >= 2)
            {
                return $"{(int)(span.TotalMinutes)} minutes ago";
            }
            if(span.TotalSeconds > 60)
            {
                return "1 minute ago";
            }
            return "less than a minute ago";
        }
    }
}
