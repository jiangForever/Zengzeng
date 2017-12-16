using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Class1
    {
        public void A()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("foo");
            var collection = database.GetCollection<Student>("Student");
            //FilterDefinition<Student> filter = 
            MongoDB.Driver.FilterDefinitionBuilder<Student> filterB = new FilterDefinitionBuilder<Student>();
            var filter = filterB.Eq("", "") & filterB.Eq("", "");
            UpdateDefinitionBuilder<Student> updateB = new UpdateDefinitionBuilder<Student>();
            var update = updateB.Set("", "").Set("", "");
            collection.FindOneAndUpdateAsync<Student>(filter, update);

        }
    }

    public class Student { }
}
