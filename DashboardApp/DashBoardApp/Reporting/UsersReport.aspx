<%@ Page Title="" Language="C#" MasterPageFile="~/Reporting/ReportingMaster.master" AutoEventWireup="true" CodeBehind="UsersReport.aspx.cs" Inherits="DashBoardApp.Reporting.UsersReport" %>
<%@ Register TagPrefix="UC" TagName="AddEditUser" Src="~/UserControls/AddEditUser.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UsersUpdatePanel" runat="server">
        <ContentTemplate>
            <table class="mainTableStyle" width="100%" cellspacing="1" cellpadding="1" style="height: 400px">
                <tr valign="top">
                    <td>
                        <table width="100%" border="0" cellpadding="5" cellspacing="2" height="60px">
                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblTitle" runat="server" Text="Users Report" CssClass="titleStyle"></asp:Label>
                                </td>
                                <td align="right" valign="bottom" class="divlbs">
                                    <div style="float: right">
                                        <asp:Label ID="Label1" runat="server" Text="Search User: "></asp:Label>
                                        &nbsp;
                                        <asp:TextBox ID="txtSearchUser" runat="server" Text=""></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="txtWESearch" runat="server" TargetControlID="txtSearchUser"
                                            WatermarkText="Search Here">
                                        </asp:TextBoxWatermarkExtender>
                                        &nbsp;
                                        <asp:LinkButton ID="lbSearchUser" runat="server" CssClass="lbPaddingStyle" Text="Go"
                                            OnClick="lbSearchUser_Click"></asp:LinkButton>
                                        &nbsp;
                                        <asp:LinkButton ID="lbShowAll" runat="server" CssClass="lbPaddingStyle" Text="Show All"
                                            OnClick="lbShowAll_Click"></asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="lbAddUser" runat="server" Text="Add User" CssClass="lbPaddingStyle"
                                            OnClick="lbAddUser_Click" Visible="false"></asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="lblErrUser" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr valign="top">
                    <td style="height: 350px; padding-top: 5px;">
                        <div class="divlbs">
                            <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                AllowSorting="True" AllowPaging="True" EnableSortingAndPagingCallbacks="True"
                                CellPadding="3" GridLines="Both" Height="100%" Width="100%" PageSize="10" OnSorting="gvUsers_Sorting"
                                OnRowCommand="gvUsers_RowCommand" OnPageIndexChanging="gvUsers_PageIndexChanging"
                                OnRowCreated="gvUsers_RowCreated" OnRowDataBound="gvUsers_RowDataBound">
                                <HeaderStyle CssClass="gvHeaderStyle" />
                                <EmptyDataTemplate>
                                    <p class="EmptyMessageStyle">No Users Found</p>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="User Name" SortExpression="UserName" ItemStyle-Width="20%"
                                        HeaderStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfActive" runat="server" Value='<%#Eval("Active").ToString()%>' />
                                            <asp:LinkButton CommandName="EditUser" CommandArgument='<%#Eval("UserID").ToString()%>'
                                                ID="lnkEditUser" Text='<%#Eval("UserName").ToString()%>' runat="server"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FirstName" ItemStyle-Width="15%" SortExpression="FirstName"
                                        HeaderText="First Name" HeaderStyle-Width="15%"></asp:BoundField>
                                    <asp:BoundField DataField="LastName" ItemStyle-Width="15%" SortExpression="LastName"
                                        HeaderText="Last Name" HeaderStyle-Width="15%"></asp:BoundField>
                                    <asp:BoundField DataField="Email" ItemStyle-Width="15%" SortExpression="Email"
                                        HeaderText="Email" HeaderStyle-Width="15%"></asp:BoundField>
                                    <asp:BoundField DataField="StateName" ItemStyle-Width="15%" SortExpression="StateName"
                                        HeaderText="State" HeaderStyle-Width="15%"></asp:BoundField>
                                    <asp:BoundField DataField="TeamName" ItemStyle-Width="20%" SortExpression="TeamName"
                                        HeaderText="NBA Team" HeaderStyle-Width="20%"></asp:BoundField>
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
            <UC:AddEditUser ID="ucAddEditUser" runat="server" OnSaveButtonClicked="ucAddEditUser_SaveButtonClicked" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
