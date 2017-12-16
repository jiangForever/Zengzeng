using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Pager<T>
    {
        public long TotalCount { get; set; }

        public List<T> Data { get; set; }
    }
}
