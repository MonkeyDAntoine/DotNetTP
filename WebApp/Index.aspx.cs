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
using TagManagerLib;

namespace WebApp
{
    public partial class _Index : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void RenderTextSource(Object sender, EventArgs e)
        {
            SyntaxTree syntaxTree = new SyntaxTree(SourceCode_TextBox.Text);
            syntaxTree.process();
            RenderingResult_Div.InnerHtml = "<p>"+HttpUtility.HtmlEncode(syntaxTree.ToString())+"</p>";
        }

    }
}
