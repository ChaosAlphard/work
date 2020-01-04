using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Dto {
    public class VDto<T> {
        public int code { get; set; }
        public string msg { get; set; }
        public T model { get; set; }
        public List<T> data { get; set; }

        public static VDto<T> Of(ValueTuple<int, string> vt) {
            return new VDto<T>(vt);
        }

        public static VDto<T> OfModel(ValueTuple<int, string> vt, T model) {
            return new VDto<T>(vt, model);
        }

        public static VDto<T> OfData(ValueTuple<int, string> vt, List<T> data) {
            return new VDto<T>(vt, data);
        }

        public VDto<T> setModel(T model) {
            this.model = model;
            return this;
        }

        public VDto<T> setData(List<T> data) {
            this.data = data;
            return this;
        }

        public VDto(ValueTuple<int, string> vt) {
            this.code = vt.Item1;
            this.msg = vt.Item2;
        }

        public VDto(ValueTuple<int, string> vt, T model) {
            this.code = vt.Item1;
            this.msg = vt.Item2;
            this.model = model;
        }

        public VDto(ValueTuple<int, string> vt, List<T> data) {
            this.code = vt.Item1;
            this.msg = vt.Item2;
            this.data = data;
        }

        public VDto(int code, string msg) {
            this.code = code;
            this.msg = msg;
        }

        public VDto(int code, string msg, T model) {
            this.code = code;
            this.msg = msg;
            this.model = model;
        }

        public VDto(int code, string msg, List<T> data) {
            this.code = code;
            this.msg = msg;
            this.data = data;
        }
    }
}
