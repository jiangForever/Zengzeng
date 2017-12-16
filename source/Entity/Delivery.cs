using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    /// <summary>
    /// 发货表
    /// </summary>
    public class Delivery
    {
        [BsonId]
        public ObjectId ID { get; set; }

        public ObjectId OrderID { get; set; }

        public string Platform { get; set; }

        public string PlatformOrderCode { get; set; }

        public string Remark { get; set; }
    }
}
