<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="UtilityWeb.FileUpload" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>File Upload</title>
    <link href="Content/MainUpload.css" rel="stylesheet" type="text/css"/>
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        MessageStatus = { Success: 1,
            Information: 2,
            Warning: 3,
            Error: 4
        }

        MessageCSS = {
            Success: "Success",
            Information: "Information",
            Warning: "Warning",
            Error: "Error"
        }

        var intervalID = 0;
        var subintervalID = 0;
        var fileUpload;
        var form;
        var previousClass = '';

        function pageLoad() {
            $addHandler($get('upload'), 'click', onUploadClick);
        }

        function register(form, fileUpload) {
            this.form = form;
            this.fileUpload = fileUpload;
        }

        function onUploadClick() {
            if (fileUpload.value.length > 0) {
                var filename = fileExists();
                if (filename == '') {
                    updateMessage(MessageStatus.Information, 'Initializing upload ...', '', '0 of 0 Bytes');
                    form.submit();
                    Sys.UI.DomElement.addCssClass($get('dvUploader'), 'StartUpload');
                    setProgress(0);
                    startProgress();
                    intervalID = window.setInterval(function () {
                        var lbl = "good";
                        PageMethods.GetUploadStatus(function (result) {
                            if (result) {
                                setProgress(result.percentComplete);
                                updateMessage(MessageStatus.Information, result.message, result.fileName, result.downloadBytes);
                                if (result == 100) {
                                    window.clearInterval(intervalID);
                                    clearTimeout(subintervalID);
                                }
                            }
                        });
                        PageMethods.GetTestString(function (result) {
                            if (result) {
                                $get('dvDownload').innerHTML = result;
                            }
                        });
//                        $.ajax({
//                            type: "Get",
//                            url: "GetUploadStatus.ashx",
//                            data: "",
//                            success: function (result) {
//                                $get('dvDownload').innerHTML = result;
//                            }

//                        });
                    }, 500);
                }
                else
                    onComplete(MessageStatus.Error, "File name '<b>" + filename + "'</b> already exists in the list.", '', '0 of 0 Bytes');
            }
            else
                onComplete(MessageStatus.Warning, 'You need to select a file.', '', '0 of 0 Bytes');
        }

        function onComplete(type, msg, filename, downloadBytes) {
            window.clearInterval(intervalID);
            clearTimeout(subintervalID);
            updateMessage(type, msg, filename, downloadBytes);
            if (type == MessageStatus.Success) setProgress(100);
            Sys.UI.DomElement.removeCssClass($get('dvUploader'), 'StartUpload');
            refreshFileList('<%=hdRefereshGrid.ClientID %>');
        }

        function updateMessage(type, message, filename, downloadBytes) {
            var _className = MessageCSS.Error;
            var _messageTemplate = $get('tblMessage');
            var _icon = $get('dvIcon');
            _icon.innerHTML = message;
            $get('dvDownload').innerHTML = downloadBytes;
            $get('dvFileName').innerHTML = filename;
            switch (type) {
                case MessageStatus.Success:
                    _className = MessageCSS.Success;
                    break;
                case MessageStatus.Information:
                    _className = MessageCSS.Information;
                    break;
                case MessageStatus.Warning:
                    _className = MessageCSS.Warning;
                    break;
                default:
                    _className = MessageCSS.Error;
                    break;
            }
            _icon.className = '';
            _messageTemplate.className = '';
            Sys.UI.DomElement.addCssClass(_icon, _className);
            Sys.UI.DomElement.addCssClass(_messageTemplate, _className);
        }

        function refreshFileList(hiddenFieldID) {
            var hiddenField = $get(hiddenFieldID);
            if (hiddenField) {
                hiddenField.value = (new Date()).getTime();
                __doPostBack(hiddenFieldID, '');
            }
        }

        //Set progressbar based on completion value
        function setProgress(completed) {
            $get('dvProgressPrcent').innerHTML = completed + '%';
            $get('dvProgress').style.width = completed + '%';
        }

        //Display mouse over and out effect of file upload list
        function eventMouseOver(_this) {
            previousClass = _this.className;
            _this.className = 'GridHoverRow';
        }
        function eventMouseOut(_this) {
            _this.className = previousClass;
        }

        //This will call every 200 milisecnd and update the progress based on value
        function startProgress() {
            var increase = $get('dvProgressPrcent').innerHTML.replace('%', '');
            increase = Number(increase) + 1;
            if (increase <= 100) {
                setProgress(increase);
                subintervalID = setTimeout("startProgress()", 200);
            }
            else {
                window.clearInterval(subintervalID);
                clearTimeout(subintervalID);
            }
        }

        //This will check whether will was already exist on the server, 
        //if file was already exists it will return file name else empty string.
        function fileExists() {
            var selectedFile = fileUpload.value.split('\\');
            var file = $get('gvNewFiles').getElementsByTagName('a');
            for (var f = 0; f < file.length; f++) {
                if (file[f].innerHTML == selectedFile[selectedFile.length - 1]) {
                    return file[f].innerHTML;
                }
            }
            return '';
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <table width="480px" cellpadding="5" cellspacing="5" border="0">
            <tr>
                <td>
                    <table class="ContainerWrapper" border="0" cellpadding="2" cellspacing="0" width="100%">
                        <tr class="ContainerHeader">
                            <td>
                                File upload control
                            </td>
                        </tr>
                        <tr>
                            <td class="ContainerMargin">
                                <table class="Container" cellpadding="0" cellspacing="4" width="100%" border="0">
                                    <tr>
                                        <td>
                                            <div id="dvUploader">
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td style="width: 50%">
                                                            <iframe id="uploadFrame" frameborder="0" height="25" width="200" scrolling="no" src="FileUploadEngine.aspx">
                                                            </iframe>
                                                        </td>
                                                        <td>
                                                            <input id="upload" type="button" value="Upload" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="tblMessage" cellpadding="4" cellspacing="4" class="Information" border="0">
                                                <tr>
                                                    <td style="text-align: left" colspan="2">
                                                        <div id="dvIcon" class="Information">
                                                            Please select a file to upload
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="2" width="100%" border="0">
                                                <tr>
                                                    <td style="width: 100px; text-align: left">
                                                        Progress
                                                    </td>
                                                    <td style="width: auto">
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td align="left">
                                                                    <div id="dvProgressContainer">
                                                                        <div id="dvProgress">
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div id="dvProgressPrcent">
                                                                        0%
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        Download Bytes
                                                    </td>
                                                    <td align="right">
                                                        <div id="dvDownload">
                                                            Bytes
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        File Name
                                                    </td>
                                                    <td align="right">
                                                        <div id="dvFileName">
                                                            FileName
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="ContainerWrapper" border="0" cellpadding="2" cellspacing="0" width="100%">
                        <tr class="ContainerHeader">
                            <td>
                                List of uploaded files
                            </td>
                        </tr>
                        <tr>
                            <td class="ContainerMargin">
                                <asp:UpdatePanel runat="server" ID="upFiles" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="hdRefereshGrid" runat="server" OnValueChanged="hdRefereshGrid_ValueChanged" />
                                        <table class="Container" cellpadding="0" cellspacing="0" width="100%" border="0">
                                            <tr class="GridHeader">
                                                <td class="Separator" style="width: 5%;" align="right">
                                                </td>
                                                <td class="Separator" style="width: 69%">
                                                    File
                                                </td>
                                                <td class="Separator" style="width: 18%" align="right">
                                                    Size
                                                </td>
                                                <td style="width: 4%">
                                                </td>
                                                <td style="width: 4%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <div style="height: 140px; overflow: auto;">
                                                        <asp:GridView DataKeyNames="Name" ID="gvNewFiles" AllowPaging="false" runat="server"
                                                            PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="false" Width="100%"
                                                            CellPadding="0" BorderWidth="0" GridLines="None" ShowHeader="false" OnRowCommand="gvNewFiles_RowCommand"
                                                            OnRowDataBound="gvNewFiles_RowDataBound">
                                                            <AlternatingRowStyle CssClass="GridAlternate" />
                                                            <RowStyle CssClass="GridNormalRow" />
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                                                                    <ItemTemplate>
                                                                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                                            <tr>
                                                                                <td class="GridNumberRow" style="width: 5%;" align="right">
                                                                                    <%# string.Format("{0}",Container.DataItemIndex + 1 +".") %>
                                                                                </td>
                                                                                <td style="width: 63%; padding-left: 2px;" align="left">
                                                                                    <asp:LinkButton ToolTip='<%# String.Format("Download {0}",Eval("Name")) %>' runat="server"
                                                                                        ID="lbtnFiles" Text='<%#Eval("Name") %>' CommandArgument='<%#Eval("Name") %>'
                                                                                        CommandName="downloadFile"></asp:LinkButton>
                                                                                </td>
                                                                                <td style="width: 22%" align="right">
                                                                                    <%#Eval("ConvertedSize")%>
                                                                                </td>
                                                                                <td colspan="2" style="width: 5%" align="center">
                                                                                    <asp:ImageButton Width="10" runat="server" ImageUrl="~/Images/Grid_ActionDelete.gif"
                                                                                        ID="imgBtnDel" CommandName="deleteFile" CommandArgument='<%#Eval("Name") %>'
                                                                                        AlternateText="Delete" ToolTip="Delete File" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataRowStyle CssClass="GridEmptyRow" />
                                                            <EmptyDataTemplate>
                                                                <span>No file uploaded</span>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr class="GridFooter">
                                                <td colspan="5">
                                                    <div style="float: left">
                                                        Total Files:
                                                        <%= gvNewFiles.Rows.Count  %>
                                                    </div>
                                                    <div style="float: right">
                                                        Total Size:
                                                        <asp:Label runat="server" ID="lblTotalSize" Text="0 K"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="gvNewFiles" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
