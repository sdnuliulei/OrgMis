using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KNet.Data.Entity;

namespace web.Models
{
    public class OM_Right
    {
        [Field(IsAuto=true,IsKey=true)]
        public int RID { set; get; }

        public string RightName { set; get; }

        public int RTID { set; get; }
    }
}