using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MODEL.EasyUIModel
{
    /*  树的节点类
        id：节点id，对载入远程数据很重要。
        text：显示在节点的文本。
        state：节点状态，'open' or 'closed'，默认为'open'。当设置为'closed'时，拥有子节点的节点将会从远程站点载入它们。
        checked：表明节点是否被选择。
        attributes：可以为节点添加的自定义属性。
        children：子节点，必须用数组定义。
     */
    public class TreeNode
    {
        public int id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public bool Checked { get; set; }
        public object attributes { get; set; }
        public List<TreeNode> children { get; set; }
    }
}
