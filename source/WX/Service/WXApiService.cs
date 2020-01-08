using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Service {
    interface WXApiService {

        T requireSubMsg<T, O> (
            String openid,
            String templateid,
            O msgData,
            String page = null
        ) where T : new()
        where O : new();
    }
}
