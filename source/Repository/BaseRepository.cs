using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Repository
{
    public class BaseRepository<T>
    {
        const String DbName = "Zengzeng";

        public IMongoCollection<T> Collection = null;

        public FilterDefinitionBuilder<T> Filter = null;

        public UpdateDefinitionBuilder<T> Update = null;

        public ProjectionDefinitionBuilder<T> Projection = null;

        public SortDefinitionBuilder<T> Sort = null;


        public BaseRepository()
        {
            string connectionString = ConfigurationManager.AppSettings["mongodb"];
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(DbName);
            Collection = database.GetCollection<T>(typeof(T).Name);
            Filter = new FilterDefinitionBuilder<T>();
            Update = new UpdateDefinitionBuilder<T>();
            Sort = new SortDefinitionBuilder<T>();
            Projection = new ProjectionDefinitionBuilder<T>();

        }
    }
}
