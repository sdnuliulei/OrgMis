using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ray.Plat.Controls
{
    public class zTreeNode
    {
        public string id { get; set; }

        public string name { get; set; }

        public string pId { get; set; }

        public string code { get; set; }

        public string icon { get; set; }

        public string level { get; set; }

        public string isParent { get; set; }

        public string open { get; set; }

        public string sort { get; set; }
    }

    public class TreeNode
    {
        public string id { set; get; }
        public string text { set; get; }
        public string state { set; get; }
        public string iconCls { set; get; }
        public Dictionary<string, string> attributes { set; get; }
        public object children { set; get; }
    }

    public class EncodeTree
    {
        public static List<TreeNode> initTree(List<zTreeNode> nodes)
        {
            List<TreeNode> rootNode = new List<TreeNode>();
            foreach (zTreeNode node in nodes)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.id = node.id;
                treeNode.text = node.name;
                treeNode.state = node.open;
                treeNode.iconCls = "none";
                treeNode.attributes = CreateUrl(nodes, treeNode);
                treeNode.children = CreateChildTree(nodes, treeNode);
                rootNode.Add(treeNode);
            }
            return rootNode;
        }

        private static List<TreeNode> CreateChildTree(List<zTreeNode> nodes, TreeNode treeNode)
        {
            string keyid = treeNode.id;                                        //根节点ID
            List<TreeNode> nodeList = new List<TreeNode>();
            var children = nodes.Where<zTreeNode>(n => { return n.pId == keyid; });
            foreach (zTreeNode dr in children)
            {
                TreeNode node = new TreeNode();
                node.id = dr.id;
                node.text = dr.name;
                node.state = "closed";
                node.iconCls = "none";
                node.attributes = CreateUrl(nodes, node);
                node.children = CreateChildTree(nodes, node);
                List<TreeNode> cs = (List<TreeNode>)node.children;
                if (cs.Count <= 0)
                {
                    node.state = "open";
                }
                nodeList.Add(node);
            }
            return nodeList;
        }


        private static Dictionary<string, string> CreateUrl(List<zTreeNode> nodes, TreeNode jt)    //把Url属性添加到attribute中，如果需要别的属性，也可以在这里添加
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            return dic;
        }
    }
}
