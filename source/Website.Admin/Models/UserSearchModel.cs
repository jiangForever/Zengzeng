using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Admin.Models
{
    public class UserSearchModel
    {
        public string ID { get; set; }

        public string Mobile { get; set; }

        public int PageNumber { get; set; }

        public int PageCount { get; set; }
    }
}