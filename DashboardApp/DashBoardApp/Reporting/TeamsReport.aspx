<%@ Page Title="" Language="C#" MasterPageFile="~/Reporting/ReportingMaster.master" AutoEventWireup="true" CodeBehind="TeamsReport.aspx.cs" Inherits="DashBoardApp.Reporting.TeamsReport" %>
<%@ Register TagPrefix="UC" TagName="AddEditTeam" Src="~/UserControls/AddEditTeam.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="TeamsUpdatePanel" runat="server">
        <ContentTemplate>
            <table class="mainTableStyle" width="100%" cellspacing="1" cellpadding="1" style="height: 400px">
                <tr valign="top">
                    <td>
                        <table width="100%" border="0" cellpadding="5" cellspacing="2" height="60px">
                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblTitle" runat="server" Text="Teams Report" CssClass="titleStyle"></asp:Label>
                                </td>
                                <td align="right" valign="bottom" class="divlbs">
                                    <div style="float: right">
                                        <asp:Label ID="Label1" runat="server" Text="Search Team: "></asp:Label>
                                        &nbsp;
                                        <asp:TextBox ID="txtSearchTeam" runat="server" Text=""></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="txtWESearch" runat="server" TargetControlID="txtSearchTeam"
                                            WatermarkText="Search Here">
                                        </asp:TextBoxWatermarkExtender>
                                        &nbsp;
                                        <asp:LinkButton ID="lbSearchTeam" runat="server" CssClass="lbPaddingStyle" Text="Go"
                                            OnClick="lbSearchTeam_Click"></asp:LinkButton>
                                        &nbsp;
                                        <asp:LinkButton ID="lbShowAll" runat="server" CssClass="lbPaddingStyle" Text="Show All"
                                            OnClick="lbShowAll_Click"></asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="lbAddTeam" runat="server" Text="Add Team" CssClass="lbPaddingStyle"
                                            OnClick="lbAddTeam_Click" Visible="false"></asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="lblErrTeam" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr valign="top">
                    <td style="height: 350px; padding-top: 5px;">
                        <div class="divlbs">
                            <asp:GridView ID="gvTeams" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                AllowSorting="True" AllowPaging="True" EnableSortingAndPagingCallbacks="True"
                                CellPadding="3" GridLines="Both" Height="100%" Width="100%" PageSize="10" OnSorting="gvTeams_Sorting"
                                OnRowCommand="gvTeams_RowCommand" OnPageIndexChanging="gvTeams_PageIndexChanging"
                                OnRowCreated="gvTeams_RowCreated" OnRowDataBound="gvTeams_RowDataBound">
                                <HeaderStyle CssClass="gvHeaderStyle" />
                                <EmptyDataTemplate>
                                    <p class="EmptyMessageStyle">No Teams Found</p>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Team Name" SortExpression="TeamName" ItemStyle-Width="25%"
                                        HeaderStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:LinkButton CommandName="EditTeam" CommandArgument='<%#Eval("TeamID").ToString()%>'
                                                ID="lnkEditTeam" Text='<%#Eval("TeamName").ToString()%>' runat="server"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StateName" ItemStyle-Width="10%" SortExpression="StateName"
                                        HeaderText="State" HeaderStyle-Width="10%"></asp:BoundField>
                                    <asp:BoundField DataField="Arena" ItemStyle-Width="25%" SortExpression="Arena"
                                        HeaderText="Arena" HeaderStyle-Width="25%"></asp:BoundField>
                                    <asp:BoundField DataField="HeadCoach" ItemStyle-Width="25%" SortExpression="HeadCoach"
                                        HeaderText="Head Coach" HeaderStyle-Width="25%"></asp:BoundField>
                                    <asp:BoundField DataField="Champions" ItemStyle-Width="10%" SortExpression="Champions"
                                        HeaderText="Champions" HeaderStyle-Width="10%"></asp:BoundField>
                                     <asp:BoundField DataField="Founded" ItemStyle-Width="10%" SortExpression="Founded"
                                        HeaderText="Founded" HeaderStyle-Width="10%"></asp:BoundField>   
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
            <UC:AddEditTeam ID="ucAddEditTeam" runat="server" OnSaveButtonClicked="ucAddEditTeam_SaveButtonClicked" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
