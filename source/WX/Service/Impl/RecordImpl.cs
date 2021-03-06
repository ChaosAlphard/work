﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WX.Dao;
using WX.Dto;
using WX.Model;
using WX.Util;

namespace WX.Service {
    public class RecordImpl : RecordService {
        private RecordDao rd = new RecordDao();

        public VDto<Record> findAllRecord() {
            var res = rd.queryAll();
            if(res == null) {
                return VDto<Record>.Of(Status.SQL_ERROR);
            }
            try {
                var lis = DBUtil.data2List(new List<Record>(), res);
                return VDto<Record>.OfData(Status.GET_DATA_SUCCESS, lis);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return VDto<Record>.Of(Status.DATA_TO_LIST_FAIL);
            }
        }

        public VDto<Record> findByOpenid(string openid) {
            var res = rd.queryByOpenid(openid);
            if(res == null) {
                return VDto<Record>.Of(Status.SQL_ERROR);
            }
            try {
                var lis = DBUtil.data2List(new List<Record>(), res);
                return VDto<Record>.OfData(Status.GET_DATA_SUCCESS, lis);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return VDto<Record>.Of(Status.DATA_TO_LIST_FAIL);
            }
        }

        public VDto<Record> findByGuid(string guid) {
            var res = rd.findByGuid(guid);
            if(res == null) {
                return VDto<Record>.Of(Status.SQL_ERROR);
            }
            try {
                var data = DBUtil.data2Model(new Record(), res.Rows[0]);
                return VDto<Record>.OfModel(Status.GET_DATA_SUCCESS, data);
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return VDto<Record>.Of(Status.DATA_TO_MODEL_FAIL);
            }
        }

        public int insertRecord(Record record) {
            if(!record.isValid()) {
                return 0;
            }
            return rd.insertRecord(
                    record.id,
                    record.openid,
                    record.name,
                    record.message,
                    record.time
                );
        }

        public int updateRecord(string id, int judge) {
            if(id==null||judge==0) {
                return 0;
            }
            return rd.updateRecord(id, judge);
        }
    }
}
