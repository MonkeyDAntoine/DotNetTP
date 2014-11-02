<%@ Page ValidateRequest="false" Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApp._Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>My Text Editor</title>
</head>

<body>
        <form id="MainForm" runat="server">     
            <asp:TextBox Id="SourceCode_TextBox" TextMode="multiline" Columns="80" Rows="20" runat="server" />
            <asp:Button Id="Render_Button" runat="server" Text="Render" Width="98px" OnClick="RenderTextSource" />
            <div runat="server" Id="RenderingResult_Div"></div>  
            
            
            <asp:ScriptManager Id="scriptManager" runat="server">
               <Scripts>
                   <asp:ScriptReference Path="Scripts/jquery-2.1.0.min.js" />
                   <asp:ScriptReference Path="Scripts/jquery-textrange.js" />
               </Scripts>
            </asp:ScriptManager>     
        </form>
</body>
</html>
