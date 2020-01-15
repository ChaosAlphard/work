using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WX.Dto;
using WX.Model;

namespace WX.Service {
    interface RecordService {
        VDto<Record> findAllRecord();

        VDto<Record> findByOpenid(string openid);

        VDto<Record> findByGuid(string guid);

        int insertRecord(Record record);

        int updateRecord(string id, int judge);
    }
}
