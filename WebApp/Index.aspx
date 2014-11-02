<%@ Page ValidateRequest="false" Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApp._Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>My Text Editor</title>
    <link href="Style/bootstrap.min.css" rel="stylesheet" />
</head>

<body>
<div style="margin : 20px">
        <form id="MainForm" runat="server" >
			<div>
           		<asp:Button ID="Bold_Btn" runat="server" Text="Bold" />
            	<asp:Button ID="Italic_Btn" runat="server" Text="Italic" />
			</div>
            <div style="display:inline-block; width:40%; padding:10px">
            <asp:TextBox Id="SourceCode_TextBox" TextMode="multiline" Columns="80" Rows="20" runat="server" CssClass="form-control" />
            </div>
            <div style="display:inline-block; width:50%; vertical-align:top; padding:10px">
                <asp:Button Id="Render_Button" Text="Render" Width="98px" OnClick="RenderTextSource" runat="server" type="button" class="btn btn-default" />
                <asp:Button ID="ExportHTML_btn" runat="server" Text="Export HTML" />
				<div runat="server" Id="RenderingResult_Div"></div>  
            </div

            <asp:ScriptManager Id="scriptManager" runat="server" >
               <Scripts>
                   <asp:ScriptReference Path="Scripts/jquery-2.1.0.min.js" />
                   <asp:ScriptReference Path="Scripts/jquery-textrange.js" />
                   <asp:ScriptReference Path="Scripts/JScript.js" />
               </Scripts>
            </asp:ScriptManager>     
         </form>
</div>
</body>
</html>
