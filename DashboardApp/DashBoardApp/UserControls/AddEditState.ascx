<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddEditState.ascx.cs" Inherits="DashBoardApp.UserControls.AddEditState" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<input id="dummy" runat="server" style="display: none" type="button" />
<asp:ModalPopupExtender ID="mpePopup" runat="server" 
    BackgroundCssClass="popupBG" DropShadow="true" PopupControlID="pnlPopUp" 
    PopupDragHandleControlID="pnlPopUp" TargetControlID="dummy">
</asp:ModalPopupExtender>
<asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup">
    <center>
        <asp:UpdatePanel ID="upAddEditState" runat="server" UpdateMode="Conditional" 
            Visible="false">
            <ContentTemplate>
                <asp:HiddenField ID="hfStateID" runat="server" />
                <table cellpadding="0" cellspacing="0" class="PopupFormBG" width="60%">
                    <tr class="PopupFormTitleStyle" style="height: 30px;">
                        <td align="left">
                            <asp:Label ID="lblTitle" runat="server" Text="Add / Edit State"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr style="padding: 10px 8px 5px 8px;">
                        <td align="center">
                            <table ID="addEditTbl" runat="server" class="PopUpFormMainTbl" width="100%">
                                <tr>
                                    <td>
                                        <strong>State Name</strong><font color="red">*</font>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStateName" runat="server" Columns="30" CssClass="textbox" 
                                            MaxLength="30" TabIndex="1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>State Code</strong><font color="red">*</font>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStateCode" runat="server" Columns="5" CssClass="textbox" 
                                            MaxLength="5" TabIndex="6"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Area:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtArea" runat="server" Columns="1" CssClass="textbox" 
                                            MaxLength="1" TabIndex="2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Population:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPopulation" runat="server" Columns="10" CssClass="textbox" 
                                            MaxLength="10" TabIndex="3"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table ID="readOnlyTbl" runat="server" class="PopUpFormMainTbl">
                                <tr>
                                    <td>
                                        State Name:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStateName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        State Code:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStateCode" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Area:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblArea" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Population:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPopulation" runat="server"></asp:Label>
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

