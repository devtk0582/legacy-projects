<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddEditTeam.ascx.cs" Inherits="DashBoardApp.UserControls.AddEditTeam" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<input id="dummy" runat="server" style="display: none" type="button" />
<asp:ModalPopupExtender ID="mpePopup" runat="server" 
    BackgroundCssClass="popupBG" DropShadow="true" PopupControlID="pnlPopUp" 
    PopupDragHandleControlID="pnlPopUp" TargetControlID="dummy">
</asp:ModalPopupExtender>
<asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup">
    <center>
        <asp:UpdatePanel ID="upAddEditTeam" runat="server" UpdateMode="Conditional" 
            Visible="false">
            <ContentTemplate>
                <asp:HiddenField ID="hfTeamID" runat="server" />
                <table cellpadding="0" cellspacing="0" class="PopupFormBG" width="60%">
                    <tr class="PopupFormTitleStyle" style="height: 30px;">
                        <td align="left">
                            <asp:Label ID="lblTitle" runat="server" Text="Add / Edit Team"></asp:Label>
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
                                        <strong>Team Name</strong><font color="red">*</font>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTeamName" runat="server" Columns="30" CssClass="textbox" 
                                            MaxLength="30" TabIndex="1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>State</strong><font color="red">*</font>:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlStates" runat="server" TabIndex="2">
                                    </asp:DropDownList>
                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Team Stars:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTeamStars" runat="server" Columns="60" CssClass="textbox" 
                                            MaxLength="60" TabIndex="3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Arena:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtArena" runat="server" Columns="30" CssClass="textbox" 
                                            MaxLength="30" TabIndex="4"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Head Coach:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHeadCoach" runat="server" Columns="30" CssClass="textbox" 
                                            MaxLength="30" TabIndex="5"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Champions:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChampions" runat="server" Columns="3" CssClass="textbox" 
                                            MaxLength="3" TabIndex="6"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Founded:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFounded" runat="server" Columns="5" CssClass="textbox" 
                                            MaxLength="5" TabIndex="7"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table ID="readOnlyTbl" runat="server" class="PopUpFormMainTbl">
                                <tr>
                                    <td>
                                        Team Name:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTeamName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        State:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTeamState" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Team Stars:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTeamStars" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Arena:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblArena" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Head Coach:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblHeadCoach" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Champions:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblChampions" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Founded Year:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFounded" runat="server"></asp:Label>
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