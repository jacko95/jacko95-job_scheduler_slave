#pragma checksum "E:\repos\Master\Master\Views\Logs\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cc43dde1d34adec05ffde3473f70465616fc8219"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Logs_Index), @"mvc.1.0.view", @"/Views/Logs/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 2 "E:\repos\Master\Master\Views\_ViewImports.cshtml"
using Master;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\repos\Master\Master\Views\_ViewImports.cshtml"
using Master.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc43dde1d34adec05ffde3473f70465616fc8219", @"/Views/Logs/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3d1960130801bca86906bd1cf46df1a3f88fd779", @"/Views/_ViewImports.cshtml")]
    public class Views_Logs_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Master.Models.Logs>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\repos\Master\Master\Views\Logs\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 11 "E:\repos\Master\Master\Views\Logs\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 14 "E:\repos\Master\Master\Views\Logs\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Timestamp));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 17 "E:\repos\Master\Master\Views\Logs\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Output));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 20 "E:\repos\Master\Master\Views\Logs\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.ExitCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 23 "E:\repos\Master\Master\Views\Logs\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Pid));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 28 "E:\repos\Master\Master\Views\Logs\Index.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 31 "E:\repos\Master\Master\Views\Logs\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 34 "E:\repos\Master\Master\Views\Logs\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Timestamp));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 37 "E:\repos\Master\Master\Views\Logs\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Output));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 40 "E:\repos\Master\Master\Views\Logs\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.ExitCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 43 "E:\repos\Master\Master\Views\Logs\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Pid));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 46 "E:\repos\Master\Master\Views\Logs\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Master.Models.Logs>> Html { get; private set; }
    }
}
#pragma warning restore 1591