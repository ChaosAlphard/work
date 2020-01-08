using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Service {
    interface WXApiService {
        string getUserInfo(string code);
    }
}
