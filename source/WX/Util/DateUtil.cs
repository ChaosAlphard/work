using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Util {
    public static class DateUtil {
        public static long getCurrentSecond() {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        public static string getCurrentTimeStr(string format = "yyyy-MM-dd HH:mm:ss") {
            return timeSecond2Str(getCurrentSecond(), format);
        }

        public static string timeSecond2Str(long uninTimeStamp, string format = "yyyy-MM-dd HH:mm:ss") {
            return DateTimeOffset
                .FromUnixTimeSeconds(uninTimeStamp)
                .ToLocalTime()
                .ToString(format);
        }
    }
}
