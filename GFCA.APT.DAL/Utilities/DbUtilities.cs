using System;

namespace GFCA.APT.DAL
{
    public static class DbUtilities
    {
        public static DateTime ToDateTime2(this DateTime self)
        {
            return self.AddHours(14);
        }

        public static DateTime NowUtc2 => DateTime.UtcNow.ToDateTime2();
        public static DateTime DateTime2 => (DateTime)(new DateTime2());

    }


}

namespace GFCA.APT.DAL
{
    public class DateTime2
    {
        private DateTime _inner;
        public static explicit operator DateTime(DateTime2 self)
        {
            return self._inner.AddHours(14);
        }
    }
}