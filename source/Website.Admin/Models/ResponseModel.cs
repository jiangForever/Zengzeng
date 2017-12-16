using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Admin.Models
{
    public class ResponseModel
    {
        public int Code { get; set; }

        public string Message { get; set; }
    }

    public class ResponseModel<T> : ResponseModel
    {
        public T Data { get; set; }
    }
}