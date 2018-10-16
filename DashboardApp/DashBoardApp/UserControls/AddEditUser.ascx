<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddEditUser.ascx.cs" Inherits="DashBoardApp.UserControls.AddEditUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<input id="dummy" runat="server" style="display: none" type="button" />
<asp:ModalPopupExtender ID="mpePopup" runat="server" 
    BackgroundCssClass="popupBG" DropShadow="true" PopupControlID="pnlPopUp" 
    PopupDragHandleControlID="pnlPopUp" TargetControlID="dummy">
</asp:ModalPopupExtender>
<asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup">
    <center>
        <asp:UpdatePanel ID="upAddEditUser" runat="server" 
            UpdateMode="Conditional" Visible="false" >
            <ContentTemplate>
                <asp:HiddenField ID="hfUserID" runat="server" />
                <table cellpadding="0" cellspacing="0" class="PopupFormBG" width="60%">
                    <tr class="PopupFormTitleStyle" style="height: 30px;">
                        <td align="left">
                            <asp:Label ID="lblTitle" runat="server" Text="Add / Edit User"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr style="padding: 10px 8px 5px 8px;">
                        <td align="center">
                            <table class="PopUpFormMainTbl" width="100%" runat="server" id="addEditTbl">
                                <tr>
                                    <td>
                                        <strong>UserName</strong><font color="red">*</font>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUserName" runat="server" Columns="40" CssClass="textbox" 
                                            MaxLength="40" TabIndex="1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="trPW">
                                                <td>
                                                    <strong>Password</strong><font color="red">*</font>:
                                                </td>
                                                <td>
                                                    <asp:TextBox Columns="20" ID="txtPW" TextMode="Password" TabIndex="6" runat="server"
                                                        CssClass="textbox" MaxLength="20"></asp:TextBox>
                                                </td>
                                            </tr>
                                <tr runat="server" id="trConfirmPW">
                                                <td>
                                                    <strong>Confirm Password</strong><font color="red">*</font>:
                                                </td>
                                                <td>
                                                    <asp:TextBox Columns="20" ID="txtConfirmPW" TextMode="Password" TabIndex="7" runat="server"
                                                        CssClass="textbox" MaxLength="20"></asp:TextBox>
                                                    <asp:CustomValidator ID="cvPW" runat="server" ErrorMessage=""
                                                        ControlToValidate="txtConfirmPW" ClientValidationFunction="ValidatePW"></asp:CustomValidator>
                                                </td>
                                            </tr>
                                <tr>
                                    <td>
                                        <strong>First Name</strong><font color="red">*</font>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFirstName" runat="server" Columns="40" CssClass="textbox" 
                                            MaxLength="40" TabIndex="2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Last Name</strong><font color="red">*</font>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLastName" runat="server" Columns="40" CssClass="textbox" 
                                            MaxLength="40" TabIndex="3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Email</strong><font color="red">*</font>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail" runat="server" Columns="40" CssClass="textbox" 
                                            MaxLength="40" TabIndex="4"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Phone:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPhone" runat="server" Columns="20" CssClass="textbox" 
                                            MaxLength="20" TabIndex="5"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        State:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlStates" runat="server" CssClass="ddlStyle">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Team:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTeams" runat="server" CssClass="ddlStyle">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <table class="PopUpFormMainTbl" runat="server" id="readOnlyTbl">
                                <tr>
                                    <td>
                                        UserName:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUserName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        First Name:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFirstName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Last Name:
                                    </td>
                                    <td>
                                       <asp:Label ID="lblLastName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Email:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Phone:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        State:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblState" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Team:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTeam" runat="server"></asp:Label>
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
                                        <asp:LinkButton ID="lbSave" runat="server" CssClass="PopupFormButton" 
                                            OnClick="lbSave_Click" Text="Save"></asp:LinkButton>
                                    </td>
                                    <td align="center">
                                        <asp:LinkButton ID="lbCancel" runat="server" CssClass="PopupFormButton" 
                                            OnClick="lbCancel_Click" Text="Cancel"></asp:LinkButton>
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
<script type="text/javascript">
    function ValidatePW(source, arguments) {
        var pwd = document.getElementById('<%=txtPW.ClientID %>');
        var confirmpwd = document.getElementById('<%=txtConfirmPW.ClientID %>');
        if (pwd.value == confirmpwd.value)
            arguments.IsValid = true;
        else
            arguments.IsValid = false;
    }
</script>