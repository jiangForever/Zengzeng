using Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Order
    {
        [BsonId]
        public ObjectId ID { get; set; }

        public string UserName { get; set; }

        public string LeaveMessage { get; set; }

        public string Sugguestion { get; set; }

        public PaymentState PaymentState { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime OrderDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime PaymentDate { get; set; }

        public PaymentChannel PaymentChannel { get; set; }

        public string OrderCode { get; set; }

        public long RevievePersonID { get; set; }

        public string Remark { get; set; }

    }
}
