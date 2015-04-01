using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace  Ray.Plat.Controls
{
    public class zTreeNodeHelper
    {

        public static zTreeNode ObjTozTreeNode<T>(T t) where T : new()
        {
            zTreeNode node = new zTreeNode();
            Type type = t.GetType();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (pi.Name.Contains("_id"))
                {
                    node.id = pi.GetValue(t, null).ToString();
                }

                if (pi.Name.Contains("_name"))
                {
                    node.name = pi.GetValue(t, null).ToString(); ;
                }

                if (pi.Name.Contains("_pId"))
                {
                    node.pId = pi.GetValue(t, null).ToString();
                }
                if (pi.Name.Contains("_icon"))
                {
                    if (pi.GetValue(t, null) != null)
                    {
                        node.icon = pi.GetValue(t, null).ToString();
                    }
                    else
                    {
                        node.icon = "";
                    }
                }

                if (pi.Name.Contains("_code"))
                {
                    if (pi.GetValue(t, null) != null)
                    {
                        node.code = pi.GetValue(t, null).ToString();
                    }
                }
                if (pi.Name.Contains("_sort"))
                {
                    if (pi.GetValue(t, null) != null)
                    {
                        node.code = pi.GetValue(t, null).ToString();
                    }
                }
                if (pi.Name.Contains("_isOpen"))
                {
                    if (pi.GetValue(t, null) != null)
                    {
                        if (pi.GetValue(t, null).ToString() == "True")
                        {
                            node.open = "true";
                        }
                    }
                }
            }
            return node;
        }


        /// <summary>
        /// 本次定制的开发
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<zTreeNode> ObjTozTreeNodes<T>(IList<T> list) where T:new()
        {
            List<zTreeNode> result = new List<zTreeNode>();
            foreach (T t in list)
            {
                zTreeNode node = new zTreeNode();
                Type type = t.GetType();
                foreach (PropertyInfo pi in type.GetProperties())
                {
                    if (pi.Name.Contains("_id"))
                    {
                        node.id = pi.GetValue(t,null).ToString();
                        continue;
                    }

                    if (pi.Name.Contains( "_name")) 
                    {
                        node.name = pi.GetValue(t, null).ToString(); ;
                        continue;
                    }

                    if (pi.Name.Contains("_pId"))
                    {
                        node.pId = pi.GetValue(t, null).ToString();
                        continue;
                    }
                    if (pi.Name.Contains("_icon"))
                    {
                        if (pi.GetValue(t, null) != null)
                        {
                            node.icon = pi.GetValue(t, null).ToString();
                        }
                        else
                        {
                            node.icon = "";
                        }
                        continue;
                    }

                    if (pi.Name.Contains("_code"))
                    {
                        if (pi.GetValue(t, null) != null)
                        {
                            node.code = pi.GetValue(t, null).ToString();
                        }
                        continue;
                    }
                    if (pi.Name.Contains("_sort"))
                    {
                        if (pi.GetValue(t, null) != null)
                        {
                            node.sort = pi.GetValue(t, null).ToString();
                        }
                        continue;
                    }

                    if (pi.Name.Contains("_isOpen"))
                    {
                        if (pi.GetValue(t, null) != null)
                        {
                            if (pi.GetValue(t, null).ToString() == "True")
                            {
                                node.open = "true";
                            }
                        }
                        continue;
                    }
                }
                result.Add(node);
            }
            return result;
        }

        /// <summary>
        /// 将树形结构Model映射到zTreeNode
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<zTreeNode> ObjTozTreeNodes(DataTable dt,object obj)
        {
            List<zTreeNode> result = new List<zTreeNode>();
            foreach (DataRow row in dt.Rows)
            {
                zTreeNode node = new zTreeNode();
                Type type = obj.GetType();
                foreach (PropertyInfo pi in type.GetProperties())
                {
                    if (pi.Name.Contains("_id"))
                    {
                        node.id = row[pi.Name].ToString();
                        continue;
                    }
                    if (pi.Name.Contains("_name"))
                    {
                        node.name = row[pi.Name].ToString();
                        continue;
                    }
                    if (pi.Name.Contains("_pId"))
                    {
                        node.pId = row[pi.Name].ToString();
                        continue;
                    }
                    if (pi.Name.Contains("_level"))
                    {
                        node.level = row[pi.Name].ToString();
                        continue;
                    }
                    if (pi.Name.Contains("_isParent"))
                    {
                        node.isParent = row[pi.Name].ToString();
                        continue;
                    }
                    if (pi.Name.Contains("_open"))
                    {
                        node.open = Convert.ToBoolean(row[pi.Name].ToString()).ToString();
                        continue;
                    }
                    if (pi.Name.Contains("_sort"))
                    {
                        node.sort = row[pi.Name].ToString();
                        continue;
                    }
                }
                result.Add(node);
            }

            return result;
        }

    }
}
