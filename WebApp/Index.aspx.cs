using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using TagManagerLib;

namespace WebApp
{
    public partial class _Index : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e)
        {
            ExportHTML_btn.Click += ExportHTML;
        }

        public void RenderTextSource(Object sender, EventArgs e)
        {
            SyntaxTree syntaxTree = new SyntaxTree(SourceCode_TextBox.Text);
            syntaxTree.process();
            RenderingResult_Div.InnerHtml = "<pre>"+syntaxTree.ToString()+"</pre>";
        }

        [System.Web.Services.WebMethod]
        public static string BoldText(string text)
        {
            AbstractTag tag = new TagBold();
            return tag.OpenTag + text + tag.CloseTag;
        }

        [System.Web.Services.WebMethod]
        public static string ItalicText(string text)
        {
            AbstractTag tag = new TagItalic();
            return tag.OpenTag + text + tag.CloseTag;
        }

        public void ExportHTML(object sender, EventArgs e)
        {
            SyntaxTree syntaxTree = new SyntaxTree(SourceCode_TextBox.Text);
            syntaxTree.process();
            HtmlRenderer renderer = new HtmlRenderer();

            string path = Path.GetTempPath() + "/render.html";
            TextWriter tw = new StreamWriter(path);
            // write a line of text to the file
            tw.WriteLine(renderer.Render(syntaxTree));
            // close the stream
            tw.Close();

            string name = Path.GetFileName(path);
            Response.AppendHeader("content-disposition", "attachment; filename=" + name);
            Response.ContentType = "text/html";
            Response.WriteFile(path);
            Response.End();
        }
    }
}
