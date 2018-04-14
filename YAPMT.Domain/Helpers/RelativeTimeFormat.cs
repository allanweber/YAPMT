using System;

namespace YAPMT.Domain.Helpers
{
    public static class RelativeTimeFormat
    {
        public static string RelativizeTime(DateTime dateTime)
        {
            int days = dateTime.Day - DateTime.Now.Day;

            if (days == 1) return "tomorrow";
            if (days == 0) return "today";
            if (days == -1) return "yesterday";

            return dateTime.ToString("MM/d");
        }
    }
}
