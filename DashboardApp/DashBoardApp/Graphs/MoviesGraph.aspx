<%@ Page Title="" Language="C#" MasterPageFile="~/Graphs/GraphMaster.master" AutoEventWireup="true"
    CodeBehind="MoviesGraph.aspx.cs" Inherits="DashBoardApp.Graphs.MoviesGraph" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                <asp:LinkButton ID="lbTitle" runat="server" Text="Movies Graph" CssClass="graphTitle"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblErr" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblMsg0" runat="server" CssClass="graphText">Choose Chart Type :</asp:Label>
                <asp:RadioButtonList ID="rblChartTypes" runat="server" AutoPostBack="true" RepeatColumns="6"
                    RepeatDirection="Horizontal" OnSelectedIndexChanged="rblChartTypes_SelectedIndexChanged">
                    <asp:ListItem Text="Column" Selected="True" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Bar" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Line" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Pie" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Doughnut" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Funnel" Value="6"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:LinkButton ID="lbChooseDate" runat="server" ForeColor="#003366"
                    Font-Bold="True" Font-Names="Verdana" Font-Size="12px">Click Here to Change Date Range</asp:LinkButton>
                <asp:Panel ID="plChooseDate" runat="server" Style="cursor: move; color: Black">
                    <div align="center">
                        <table cellpadding="4" cellspacing="4" bgcolor="#FFFFCC">
                            <tr bgcolor="#E1E1E1">
                                <td style="font-weight: bold; font-size: 12px; color: #003366; font-family: Verdana"
                                    colspan="2" align="center">
                                    <asp:Label ID="lblPopUpMsg" runat="server" Text="Choose Date Range"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblPopupErrMsg" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-weight: bold; font-size: 12px; color: #003366; font-family: Verdana"
                                    align="right">
                                    Start&nbsp;Date :
                                </td>
                                <td align="left" valign="middle">
                                    <asp:TextBox ID="txtStartDate" runat="server" class="inputbox" Width="141px" Font-Size="X-Small"
                                        Font-Names="Verdana"></asp:TextBox>
                                    <asp:ImageButton runat="Server" ID="btnStartDate" ImageUrl="../images/Calendar.png"
                                        AlternateText="Click to show calendar" ImageAlign="Middle" />
                                    <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtStartDate"
                                        Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                        ErrorTooltipEnabled="True">
                                    </asp:MaskedEditExtender>
                                    <asp:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender5"
                                        ControlToValidate="txtStartDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                        Display="Dynamic" TooltipMessage="Input a date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                        ValidationGroup="MKE"></asp:MaskedEditValidator>
                                    <asp:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="txtStartDate"
                                        PopupButtonID="btnStartDate">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-weight: bold; font-size: 12px; color: #003366; font-family: Verdana"
                                    align="right">
                                    End&nbsp;Date :
                                </td>
                                <td align="left" valign="middle">
                                    <asp:TextBox ID="txtEndDate" runat="server" class="inputbox" Width="141px" Font-Size="X-Small"
                                        Font-Names="Verdana"></asp:TextBox>
                                    <asp:ImageButton runat="Server" ID="btnEndDate" ImageUrl="../images/Calendar.png"
                                        AlternateText="Click to show calendar" ImageAlign="Middle" />
                                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtEndDate"
                                        Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                        ErrorTooltipEnabled="True">
                                    </asp:MaskedEditExtender>
                                    <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender5"
                                        ControlToValidate="txtEndDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                        Display="Dynamic" TooltipMessage="Input a date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                        ValidationGroup="MKE"></asp:MaskedEditValidator>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEndDate"
                                        PopupButtonID="btnEndDate">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="OkButton" runat="server" CssClass="cssbutton" Text="Re-Generate Graph"
                                        Width="137px" OnClick="OkButton_Click" />
                                    <asp:Button ID="CancelButton" runat="server" CssClass="cssbutton" Text="Cancel" OnClick="CancelButton_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <input id="dummy" runat="server" style="display: none" type="button" />
                <asp:ModalPopupExtender ID="popupChooseDate" runat="server" TargetControlID="lbChooseDate"
                    PopupControlID="plChooseDate" DropShadow="true" BackgroundCssClass="popupBG">
                </asp:ModalPopupExtender>
            </td>
        </tr>
        <tr>
            <td align="center" valign="top">
                <asp:Label ID="lblEmtMsg" runat="server" ForeColor="Red"></asp:Label>
                <asp:Literal ID="FCChart" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="center" valign="top">
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="200px">
               <asp:DataGrid ID="dgChart" ShowFooter="True" runat="server" AutoGenerateColumns="false"
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" Font-Names="Verdana"
                    Font-Size="11px">
                    <Columns>
                        <asp:BoundColumn DataField="TypeName" SortExpression="TypeName" HeaderText="Type Name">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="MoviesCount" SortExpression="MoviesCount" HeaderText="Number Of Movies">
                        </asp:BoundColumn>
                    </Columns>
                    <ItemStyle CssClass="dlItemStyle" />
                    <HeaderStyle CssClass="dlHeaderStyle" />
                    <AlternatingItemStyle CssClass="dlAlterItemStyle" />
                    <FooterStyle CssClass="dlFooterStyle" />
                </asp:DataGrid>
                </asp:Panel>
                
            </td>
        </tr>
    </table>
</asp:Content>
