using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Util {
    public static class DateUtil {
        public static long getCurrentSecond() {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        public static string timeSecond2Str(long uninTimeStamp) {
            return DateTimeOffset
                .FromUnixTimeSeconds(uninTimeStamp)
                .ToLocalTime()
                .ToString();
        }
    }
}
