using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        public string OpenId { get; set; }

        public string NickName { get; set; }

        public string Mobile { get; set; }

        public string Address { get; set; }

        public string CardNo { get; set; }

        public string Avator { get; set; }

        public string Unique { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateDate { get; set; }

        public string Remark { get; set; }
    }
}
