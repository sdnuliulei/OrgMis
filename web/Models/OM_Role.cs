using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KNet.Data.Entity;

namespace web.Models
{
    public class OM_Role:ITable
    {
        [Field(IsAuto=true,IsKey=true)]
        public int RoleID { set; get; }

        public string RoleName { set; get; }
    }
}