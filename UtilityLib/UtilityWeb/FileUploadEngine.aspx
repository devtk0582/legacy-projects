﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUploadEngine.aspx.cs" Inherits="UtilityWeb.FileUploadEngine" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/MainUpload.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form" runat="server" enctype="multipart/form-data">
    <div>
    <asp:ScriptManager ID="scriptManager" runat="server" />
    <script type="text/javascript">
        function pageLoad(sender, args) {
            //Register the form and upload elements
            window.parent.register(
                   $get('<%= this.form.ClientID %>'),
                   $get('<%= this.fileUpload.ClientID %>')
               );
        }
       </script>
       <asp:FileUpload ID="fileUpload" runat="server" />
    </div>
    </form>
</body>
</html>
