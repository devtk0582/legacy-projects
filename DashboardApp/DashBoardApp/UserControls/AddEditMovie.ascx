<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddEditMovie.ascx.cs" Inherits="DashBoardApp.UserControls.AddEditMovie" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<input id="dummy" runat="server" style="display: none" type="button" />
<asp:ModalPopupExtender ID="mpePopup" runat="server" 
    BackgroundCssClass="popupBG" DropShadow="true" PopupControlID="pnlPopUp" 
    PopupDragHandleControlID="pnlPopUp" TargetControlID="dummy">
</asp:ModalPopupExtender>
<asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup">
    <center>
        <asp:UpdatePanel ID="upAddEditMovie" runat="server" UpdateMode="Conditional" 
            Visible="false">
            <ContentTemplate>
                <asp:HiddenField ID="hfMovieID" runat="server" />
                <table cellpadding="0" cellspacing="0" class="PopupFormBG" width="60%">
                    <tr class="PopupFormTitleStyle" style="height: 30px;">
                        <td align="left">
                            <asp:Label ID="lblTitle" runat="server" Text="Add / Edit Movie"></asp:Label>
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
                                        <strong>Movie Name</strong><font color="red">*</font>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMovieName" runat="server" Columns="30" CssClass="textbox" 
                                            MaxLength="30" TabIndex="1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Movie Type</strong><font color="red">*</font>:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMovieTypes" runat="server" TabIndex="2">
                                    </asp:DropDownList>
                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Movie Date</strong><font color="red">*</font>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMovieDate" runat="server" Columns="10" CssClass="textbox" 
                                            MaxLength="10" TabIndex="3"></asp:TextBox>
                                        <asp:CalendarExtender ID="ceMovieDate" runat="server" TargetControlID="txtMovieDate">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Movie Comment:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMovieComment" runat="server" Columns="30" CssClass="textbox" 
                                             Rows="6" TextMode="MultiLine" TabIndex="4"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Movie Review:
                                    </td>
                                    <td>
                                        <asp:Rating ID="ratingMovieReview" runat="server" MaxRating="5" StarCssClass="star" EmptyStarCssClass="emptyStar" WaitingStarCssClass="waitingStar" FilledStarCssClass="star">
                                        </asp:Rating>
                                    </td>
                                </tr>
                            </table>
                            <table ID="readOnlyTbl" runat="server" class="PopUpFormMainTbl">
                                <tr>
                                    <td>
                                        Movie Name:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMovieName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Movie Type:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMovieType" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Movie Date:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMovieDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Movie Comment:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMovieComment" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Movie Review:
                                    </td>
                                    <td>
                                        <asp:Rating ID="ratingMovieReviewRO" ReadOnly="true" runat="server" MaxRating="5" StarCssClass="star" EmptyStarCssClass="emptyStar" WaitingStarCssClass="waitingStar" FilledStarCssClass="star">
                                        </asp:Rating>
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