#pragma checksum "C:\Dev\Estudos\SignalR\ZapWeb\ZapWeb\Views\Home\Cadastro.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3c48fd871f3f2a8499b411a2773c23eaeb02f544"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Cadastro), @"mvc.1.0.view", @"/Views/Home/Cadastro.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Cadastro.cshtml", typeof(AspNetCore.Views_Home_Cadastro))]
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
#line 1 "C:\Dev\Estudos\SignalR\ZapWeb\ZapWeb\Views\_ViewImports.cshtml"
using ZapWeb;

#line default
#line hidden
#line 2 "C:\Dev\Estudos\SignalR\ZapWeb\ZapWeb\Views\_ViewImports.cshtml"
using ZapWeb.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3c48fd871f3f2a8499b411a2773c23eaeb02f544", @"/Views/Home/Cadastro.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e6e270d4ee7345f9517ad8d5c1076d5c6ef18c20", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Cadastro : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            DefineSection("style", async() => {
                BeginContext(16, 184, true);
                WriteLiteral("\r\n    <style type=\"text/css\">\r\n        body {\r\n            background-color: #32A060;\r\n        }\r\n\r\n        input[type=text] {\r\n            max-width: 690px;\r\n        }\r\n    </style>\r\n");
                EndContext();
            }
            );
            BeginContext(203, 553, true);
            WriteLiteral(@"
<div class=""container-login"">
    <img src=""/imagem/logo.png"" />
    <h1>ZapWeb</h1>
    <div class=""container-form"" id=""form-cadastro"">
        <input type=""text"" placeholder=""Nome"" id=""nome"" />
        <br />
        <input type=""text"" placeholder=""E-mail"" id=""email"" />
        <br />
        <input type=""text"" placeholder=""Senha"" id=""senha""/>

        <div class=""container-button"">
            <input type=""button"" value=""CADASTRAR"" id=""btnCadastrar"" />
        </div>

        <span id=""mensagem""></span>

    </div>
</div>

");
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
