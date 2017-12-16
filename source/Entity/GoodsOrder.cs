using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class GoodsOrder
    {
        [BsonId]
        public ObjectId ID { get; set; }

        public ObjectId OrderID { get; set; }

        public ObjectId GoodID { get; set; }

    }
}
