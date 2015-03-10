using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KNet.Data.Entity;

namespace web.Models
{
    public class OM_RightType:ITable
    {
        [Field(IsAuto=true,IsKey=true)]
        public int RTID { set; get; }

        public string RTypeName { set; get; }
    }
}