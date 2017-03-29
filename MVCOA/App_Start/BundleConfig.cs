﻿using System.Web;
using System.Web.Optimization;

namespace MVCOA
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // 使用 Modernizr 的开发版本进行开发和了解信息。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new ScriptBundle("~/mvcAjax").Include("~/Scripts/jquery-1.7.1.min.js", "~/Scripts/jquery.unobtrusive-ajax.min.js", "~/Scripts/jquery.validate.min.js", "~/Scripts/jquery.validate.unobtrusive.min.js", "~/Scripts/jquery.msgProcess.js"));

            bundles.Add(new ScriptBundle("~/easyUIJS").Include("~/Scripts/jquery.min.js", "~/EasyUI/jquery.easyui.min.js", "~/Scripts/jquery.msgProcess.js", "~/EasyUI/locale/easyui-lang-zh_CN.js"));

            //因为css中可能使用到了 相对路径的图片，所以 需要为 虚拟路径 指定到 能找到图片的 路径
            bundles.Add(new StyleBundle("~/EasyUI/themes/default/easycss").Include("~/EasyUI/themes/icon.css", "~/EasyUI/themes/default/easyui.css"));

            BundleTable.EnableOptimizations = true; 
        }
    }
}