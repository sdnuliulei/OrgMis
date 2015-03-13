using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Ray.Plat.Common
{
    public class JsonHelper
    {
        private static JavaScriptSerializer js = new JavaScriptSerializer();

        /// <summary>
        /// 加载数据列表
        /// </summary>
        /// <param name="objs">当前页数据集</param>
        /// <param name="total">数据记录总数</param>
        /// <returns></returns>
        public static string ObjectsToJson<T>(IList<T> objs, int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append("\"total\":\"" + total + "\",\"rows\":[");
            int i = 0;
            foreach (object o in objs)
            {
                if (i > 0)
                {
                    sb.Append("," + js.Serialize(o));
                    i++;
                }
                else
                {
                    sb.Append(js.Serialize(o));
                    i++;
                }
            }
            sb.Append("]}");
            return sb.ToString();
        }
    }
}
