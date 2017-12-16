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
    /// <summary>
    /// 商品
    /// </summary>
    public class Goods
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        public float Price { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public List<Picture> Pictures { get; set; }

        public string Source { get; set; }

        public int Sort { get; set; }

        public string Name { get; set; }

        public GoodsState State { get; set; }
    }

    public class Picture
    {
        public string PictureUrl { get; set; }

    }
}
