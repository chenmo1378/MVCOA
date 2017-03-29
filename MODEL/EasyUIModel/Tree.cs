using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MODEL.EasyUIModel
{
    /*  树对象
        url（超链接）	string（字符串）	用以载入远程数据的超链接地址。	null
        method（方法）	string（字符串）	获取数据的HTTP方法。	post
        animate（动画）	boolean（布尔型）	定义当节点打开或关闭时是否显示动画效果。	false
        checkbox（复选框）	boolean（布尔型）	定义是否在每个节点之前显示复选框。	false
        cascadeCheck（级联选择）	boolean（布尔型）	定义是否支持级联选择。	true
        onlyLeafCheck（只选叶子节点）	boolean（布尔型）	定义是否只在叶子节点之前显示复选框。	false
        dnd（拖放）	boolean（布尔型）	定义是否支持拖放。	false
        data（数据）
     */
    public class Tree
    {
        public string url { get; set; }
        public string method { get; set; }
        public bool animate { get; set; }
        public bool checkbox { get; set; }
        public bool cascadeCheck { get; set; }
        public bool onlyLeafCheck { get; set; }
        public bool dnd { get; set; }
        public List<TreeNode> data { get; set; }
    }
}
