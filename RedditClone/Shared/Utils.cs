namespace RedditClone.Shared
{
    public static class Utils
    {
        static Random _random = new Random();

        public static int FuzzVote(int vote)
        {
            var fuzzPercentage = 10;
            var v = (double)vote;
            var r = (double)(100 - _random.Next(-fuzzPercentage, fuzzPercentage)) / 100;
            v *= r;
            return (int)Math.Round(v);
        }

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
