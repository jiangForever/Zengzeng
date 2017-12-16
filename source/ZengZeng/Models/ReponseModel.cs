using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZengZeng.Models
{
    public class ReponseModel
    {
        public int Code { get; set; }

        public string Message { get; set; }

    }

    public class ReponseModel<T> : ReponseModel
    {
        public T Data { get; set; }
    }

}