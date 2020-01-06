using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Util {
    public static class DateUtil {
        public static long getCurrentSecond() {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        }
    }
}
