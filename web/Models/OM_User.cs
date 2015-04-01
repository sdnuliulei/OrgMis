using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KNet.Data.Entity;

namespace web.Models
{
    public class OM_User:ITable
    {
        [Field(IsKey=true)]
        public string UserID { set; get; }

        public string UserName { set; get; }

        public string Password { set; get; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { set; get; }

        public string Email { set; get; }

        public string Phone { set; get; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string Org_id { set; get; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleID { set; get; }

        public bool Status { set; get; }

        public DateTime CreateTime { set; get; }
    }
}