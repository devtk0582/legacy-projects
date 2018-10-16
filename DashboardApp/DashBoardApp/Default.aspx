<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DashBoardApp._Default" %>

<%@ Register TagPrefix="UC" TagName="Register" Src="~/UserControls/Register.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Log In - Dash Board Application</title>
    <link rel="stylesheet" href="css/dashboard.css" type="text/css" />
</head>
<body>
    <center>
        <div class="stylesheet">
            <form id="form4" defaultbutton="btnLogin" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                        </cc1:ToolkitScriptManager>
                        <cc1:ModalPopupExtender ID="mpeForgotPW" runat="server" TargetControlID="lnkForgotPasword"
                           PopupDragHandleControlID="pnlForgotPW"  PopupControlID="pnlForgotPW" Drag="true" BackgroundCssClass="popupBG" DropShadow="true">
                        </cc1:ModalPopupExtender>

                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="height: 50px;">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table height="450" width="800px" align="center" border="0" style="background: #FFF url('images/LoginBG.gif') no-repeat;">
                                        <tr>
                                            <td>
                                                <table border="1" cellpadding="0" cellspacing="0" align="center" bgcolor="White" frame="border" style="font-size: 12px; border-color: Black;">
                                                  <tr>
                                                        <td colspan="2">
                                                            <img src="images/log-in-title.gif" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table border="0" align="center" cellpadding="1" cellspacing="5" style="width: 317px;padding: 10px;"
                                                                bgcolor="White">
                                                                                                                  
                                                                <tr>
                                                                    <td width="40%">
                                                                        <span>User Name:</span>
                                                                    </td>
                                                                    <td width="60%">
                                                                        <asp:TextBox ID="txtUserID" MaxLength="50" runat="server" Width="150px" Font-Names="Verdana"
                                                                            Font-Size="12px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="40%">
                                                                        <span class="style18">Password:</span>
                                                                    </td>
                                                                    <td width="60%">
                                                                        <asp:TextBox ID="txtPWD" MaxLength="20" TextMode="Password" runat="server" Width="150px"
                                                                            Font-Names="Verdana" Font-Size="12px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="right">
                                                                        <asp:LinkButton CssClass="linkButton" ID="btnLogin" runat="server" ForeColor="White"
                                                                            Text="Login" OnClick="btnLogin_Click" />
                                                                        <asp:LinkButton ID="btnReset" runat="server" ForeColor="White" CssClass="linkButton"
                                                                            Text="Refresh" OnClick="btnReset_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="center">
                                                                        <asp:Label ID="lblErrMessage" Font-Bold="true" runat="server" Font-Names="Verdana"
                                                                            Font-Size="12px" ForeColor="Red"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="center">
                                                                        <asp:LinkButton ID="lnkForgotPasword" ForeColor="#003366" Font-Bold="True" runat="server"
                                                                            Text="Forgot My User Name/Password" Font-Names="Verdana" Font-Size="11px" ></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" align="center">
                                                                         <asp:LinkButton ID="lbRegister" ForeColor="#003366" Font-Bold="True" runat="server"
                                                                            Text="Create New Account" Font-Names="Verdana" Font-Size="11px" 
                                                                             onclick="lbRegister_Click" ></asp:LinkButton>
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
                            <tr style="height: 50px;">
                                <td>
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="72%">
                                                &nbsp;
                                            </td>
                                            <td valign="bottom" width="28%" align="right">
                                                <asp:Label ID="lblBottomMsg" CssClass="msgStyle" runat="server" Text="Welcome To My Dashboard Application"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblTime" CssClass="msgStyle" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" height="20">
                        Copy Right @ Ke Tang 2012
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                <asp:Panel ID="pnlForgotPW" runat="server" CssClass="modalPopup">
                    <asp:UpdatePanel ID="udpInner" runat="Server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="PopupFormBG" width="60%" cellpadding="0" cellspacing="0">
                                <tr style="height: 30px;">
                                    <td align="left" class="PopupFormTitleStyle">
                                        <asp:Label ID="Label5" runat="server" Text="Forgot Password"></asp:Label>
                                    </td>
                                </tr>
                                 <tr style="padding: 0 8px 0 8px;">
                                    <td align="center">
                                        <table width="100%" class="PopupFormMainTbl">
                                            <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblpopupErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                      <tr>
                                    <td align="left" width="30%">
                                        <strong>Email</strong><font color="red">*</font>:
                                    </td>
                                    <td align="left" width="70%">
                                        <asp:TextBox ID="txtEmail" CssClass="textbox" runat="server" MaxLength="40" Width="150px" Columns="40"></asp:TextBox>
                                    </td>
                                </tr>  
                                        </table>
                                    </td>
                                </tr>
                                
                                <tr style="height: 50px;">
                                <td align="right">
                                    <table width="30%">
                                        <tr>
                                        <td align="center">
                                        <asp:LinkButton ID="lbRetrival" runat="server" CssClass="PopupFormButton"
                                            OnClick="lbRetrival_Click" Text="Retrieve" />
                                        
                                    </td>
                                    <td align="center">
                                    <asp:LinkButton ID="lbCancel" runat="server" CssClass="PopupFormButton"
                                            OnClick="lbCancel_Click" Text="Cancel" />
                                    </td>
                                        </tr>
                                    </table>
                                </td>
                                    
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
                    </td>
                </tr>
            </table>
            
            <UC:Register runat="server" id="ucRegister">
            </UC:Register>
            </form>
        </div>
    </center>
</body>
</html>
