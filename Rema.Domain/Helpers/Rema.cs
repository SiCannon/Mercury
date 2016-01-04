using System;

namespace Rema.Domain.Helpers
{
    class Rema
    {
        public static int? TimeToDuration(string time)
        {
            if (!string.IsNullOrEmpty(time))
            {
                string hh = time.Substring(0, 1);
                string mm = time.Substring(3, 2);
                string ss = time.Substring(6, 2);
                int h, m, s;
                if (int.TryParse(hh, out h) && int.TryParse(mm, out m) && int.TryParse(ss, out s))
                    return s + (60 * m) + (60 * 60 * h);
                else
                    return null;
            }
            else
                return null;
        }
    }
}
