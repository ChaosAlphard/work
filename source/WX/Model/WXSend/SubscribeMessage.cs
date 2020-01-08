using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.Model {
    public class SubscribeMessage<T> {
        public String touser { get; set; }
        public String template_id { get; set; }
        public String page { get; set; }
        public T data { get; set; }

        public override string ToString() {
            return $"SubscribeMessage[" +
                $"touser:'{touser}'," +
                $"template_id:'{template_id}'," +
                $"page:'{page}'" +
                $"data:{data?.ToString()}" +
                $"]";
        }
    }
}
