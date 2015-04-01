using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KNet.Data.Entity;

namespace web.Models
{
    public class OM_Org:ITable
    {
        [Field(IsKey=true)]
        public string Org_id { set; get; }

        public string Org_name { set; get; }

        public string Org_pId { set; get; }
    }
}