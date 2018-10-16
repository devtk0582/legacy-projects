<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Register.ascx.cs" Inherits="DashBoardApp.UserControls.Register" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<input id="dummy" type="button" style="display: none" runat="server" />
<ajaxToolkit:ModalPopupExtender runat="server" ID="mpePopup" TargetControlID="dummy"
   PopupDragHandleControlID="pnlPopUp"  PopupControlID="pnlPopUp" BackgroundCssClass="popupBG" DropShadow="true" />
<asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup">
<center>
    <asp:UpdatePanel ID="upRegister" runat="server" UpdateMode="Conditional" Visible="false">
        <ContentTemplate>
            <table class="PopupFormBG" width="60%" cellpadding="0" cellspacing="0">
                <tr style="height: 30px;">
                    <td align="left" class="PopupFormTitleStyle">
                        <asp:Label ID="lblTitle" runat="server" Text="Sign On" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr style="padding: 10px 8px 5px 8px;">
                    <td align="center">
                        <table width="100%" class="PopUpFormMainTbl">
                        <tr>
                            <td>
                                    <strong>UserName</strong><font color="red">*</font>:
                                </td>
                                <td>
                                   <asp:TextBox Columns="50" ID="txtUserName" TabIndex="1" runat="server" CssClass="textbox"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                             </tr> 
                             <tr>
                            <td>
                                    <strong>Password</strong><font color="red">*</font>:
                                </td>
                                <td>
                                   <asp:TextBox Columns="50" ID="txtPassword" TabIndex="2" runat="server" CssClass="textbox"
                                        MaxLength="50" TextMode="Password"></asp:TextBox>
                                </td>
                             </tr> 
                        <tr>
                                <td>
                                    <strong>First Name</strong><font color="red">*</font>:
                                </td>
                                <td>
                                    <asp:TextBox Columns="50" ID="txtFirstName" TabIndex="3" runat="server" CssClass="textbox"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                                </tr>
                                <tr>
                                <td>
                                    <strong>Last Name</strong><font color="red">*</font>:
                                </td>
                                <td>
                                    <asp:TextBox Columns="50" ID="txtLastName" TabIndex="4" runat="server" CssClass="textbox"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                             <tr> 
                                <td>
                                    <strong>Email</strong><font color="red">*</font>:
                                </td>
                                <td>
                                   <asp:TextBox Columns="50" ID="txtEmail" TabIndex="5" runat="server" CssClass="textbox"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                           
                            <tr>
                                <td>
                                    Phone:
                                </td>
                                <td>
                                    <asp:TextBox Columns="20" ID="txtPhone" TabIndex="6" runat="server" CssClass="textbox"
                                        MaxLength="20"></asp:TextBox>
                                </td>
                            </tr> 
                        </table>
                    </td>
                </tr>
                <tr style="height: 50px;">
                    <td align="right">
                        <table width="100px">
                            <tr>
                                <td align="center">
                                    <asp:LinkButton ID="lbSave" runat="server" Text="Add" CssClass="PopupFormButton" OnClick="lbSave_Click"></asp:LinkButton>
                                </td>
                                <td align="center">
                                    <asp:LinkButton ID="lbCancel" runat="server" Text="Cancel" CssClass="PopupFormButton" OnClick="lbCancel_Click"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </center>
</asp:Panel>