<%@ Page Title="" Language="C#" MasterPageFile="~/Reporting/ReportingMaster.master" AutoEventWireup="true" CodeBehind="StatesReport.aspx.cs" Inherits="DashBoardApp.Reporting.StatesReport" %>
<%@ Register TagPrefix="UC" TagName="AddEditState" Src="~/UserControls/AddEditState.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="StatesUpdatePanel" runat="server">
        <ContentTemplate>
            <table class="mainTableStyle" width="100%" cellspacing="1" cellpadding="1" style="height: 400px">
                <tr valign="top">
                    <td>
                        <table width="100%" border="0" cellpadding="5" cellspacing="2" height="60px">
                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblTitle" runat="server" Text="States Report" CssClass="titleStyle"></asp:Label>
                                </td>
                                <td align="right" valign="bottom" class="divlbs">
                                    <div style="float: right">
                                        <asp:Label ID="Label1" runat="server" Text="Search State: "></asp:Label>
                                        &nbsp;
                                        <asp:TextBox ID="txtSearchState" runat="server" Text=""></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="txtWESearch" runat="server" TargetControlID="txtSearchState"
                                            WatermarkText="Search Here">
                                        </asp:TextBoxWatermarkExtender>
                                        &nbsp;
                                        <asp:LinkButton ID="lbSearchState" runat="server" CssClass="lbPaddingStyle" Text="Go"
                                            OnClick="lbSearchState_Click"></asp:LinkButton>
                                        &nbsp;
                                        <asp:LinkButton ID="lbShowAll" runat="server" CssClass="lbPaddingStyle" Text="Show All"
                                            OnClick="lbShowAll_Click"></asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="lbAddState" runat="server" Text="Add State" CssClass="lbPaddingStyle"
                                            OnClick="lbAddState_Click" Visible="false"></asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="lblErrState" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr valign="top">
                    <td style="height: 350px; padding-top: 5px;">
                        <div class="divlbs">
                            <asp:GridView ID="gvStates" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                AllowSorting="True" AllowPaging="True" EnableSortingAndPagingCallbacks="True"
                                CellPadding="3" GridLines="Both" Height="100%" Width="100%" PageSize="10" OnSorting="gvStates_Sorting"
                                OnRowCommand="gvStates_RowCommand" OnPageIndexChanging="gvStates_PageIndexChanging"
                                OnRowCreated="gvStates_RowCreated" OnRowDataBound="gvStates_RowDataBound">
                                <HeaderStyle CssClass="gvHeaderStyle" />
                                <EmptyDataTemplate>
                                    <p class="EmptyMessageStyle">No States Found</p>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="State Name" SortExpression="StateName" ItemStyle-Width="25%"
                                        HeaderStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:LinkButton CommandName="EditState" CommandArgument='<%#Eval("StateID").ToString()%>'
                                                ID="lnkEditState" Text='<%#Eval("StateName").ToString()%>' runat="server"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StateCode" ItemStyle-Width="25%" SortExpression="StateCode"
                                        HeaderText="State Code" HeaderStyle-Width="25%"></asp:BoundField>
                                    <asp:BoundField DataField="Area" ItemStyle-Width="15%" SortExpression="Area"
                                        HeaderText="Area" HeaderStyle-Width="15%"></asp:BoundField>
                                    <asp:BoundField DataField="Population" ItemStyle-Width="35%" SortExpression="Population"
                                        HeaderText="Population" HeaderStyle-Width="35%"></asp:BoundField>
                                </Columns>
                                <PagerSettings Mode="NumericFirstLast" FirstPageImageUrl="../images/paging-first.gif"
                                    LastPageImageUrl="../images/paging-last.gif" />
                                <PagerStyle CssClass="gvPagerStyle" />
                                <RowStyle CssClass="gvRowStyle" />
                                <AlternatingRowStyle CssClass="gvAltRowStyle" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
           <UC:AddEditState ID="ucAddEditState" runat="server" OnSaveButtonClicked="ucAddEditState_SaveButtonClicked" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
