using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Util {
    public static class RandomUtil {
        public static string getGUID(string format = "D") {
            return Guid.NewGuid().ToString(format);
        }
    }
}
