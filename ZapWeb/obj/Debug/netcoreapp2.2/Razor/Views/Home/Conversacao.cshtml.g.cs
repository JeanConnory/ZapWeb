#pragma checksum "C:\Projetos\Estudos\SignalR\ZapWeb\ZapWeb\Views\Home\Conversacao.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e372b6270f6abe6e54f5ebbbabbea2c1eef9e12f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Conversacao), @"mvc.1.0.view", @"/Views/Home/Conversacao.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Conversacao.cshtml", typeof(AspNetCore.Views_Home_Conversacao))]
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
#line 1 "C:\Projetos\Estudos\SignalR\ZapWeb\ZapWeb\Views\_ViewImports.cshtml"
using ZapWeb;

#line default
#line hidden
#line 2 "C:\Projetos\Estudos\SignalR\ZapWeb\ZapWeb\Views\_ViewImports.cshtml"
using ZapWeb.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e372b6270f6abe6e54f5ebbbabbea2c1eef9e12f", @"/Views/Home/Conversacao.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e6e270d4ee7345f9517ad8d5c1076d5c6ef18c20", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Conversacao : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            DefineSection("style", async() => {
                BeginContext(16, 112, true);
                WriteLiteral("\r\n    <style type=\"text/css\">\r\n        body {\r\n            background-color: #F0F0F0;\r\n        }\r\n    </style>\r\n");
                EndContext();
            }
            );
            BeginContext(131, 1599, true);
            WriteLiteral(@"
<!-- Side navigation -->
<div class=""sidenav"">
    <div class=""container-logo"">
        <img src=""/imagem/logo.png"" style=""width: 50%;"" />
    </div>
    <div id=""users"">
        <div class=""container-user-item"">
            <img src=""/imagem/logo.png"" style=""width: 20%;"" />
            <div>
                <span>Aline (online)</span>
                <span class=""email"">aline123@gmail.com</span>
            </div>
        </div>
        <div class=""container-user-item"">
            <img src=""/imagem/logo.png"" style=""width: 20%;"" />
            <div>
                <span>Aline (offline)</span>
                <span class=""email"">aline123@gmail.com</span>
            </div>
        </div>
    </div>

</div>

<!-- Page content -->
<div class=""main"" id=""tela-conversacao"">
    <div class=""container-messages"">
        <div class=""message message-left"">
            <div class=""message-head"">
                <img src=""/imagem/chat.png"" />
                Aline
            </div>
  ");
            WriteLiteral(@"          <div class=""message-message"">
                Olá José! Como vai?
            </div>


        </div>
        <div class=""message message-right"">
            <div class=""message-head"">
                <img src=""/imagem/chat.png"" />
                Eu
            </div>
            <div class=""message-message"">
                Olá José! Como vai?
            </div>
        </div>

    </div>

    <div class=""container-button"">
        <input type=""text"" placeholder=""Mensagem"" />
        <button class=""btn-send""></button>
    </div>
</div>");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
