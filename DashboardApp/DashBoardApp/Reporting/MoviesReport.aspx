<%@ Page Title="" Language="C#" MasterPageFile="~/Reporting/ReportingMaster.master" AutoEventWireup="true" CodeBehind="MoviesReport.aspx.cs" Inherits="DashBoardApp.Reporting.MoviesReport" %>
<%@ Register TagPrefix="UC" TagName="AddEditMovie" Src="~/UserControls/AddEditMovie.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="MoviesUpdatePanel" runat="server">
        <ContentTemplate>
            <table class="mainTableStyle" width="100%" cellspacing="1" cellpadding="1" style="height: 400px">
                <tr valign="top">
                    <td>
                        <table width="100%" border="0" cellpadding="5" cellspacing="2" height="60px">
                            <tr>
                                <td align="left" valign="top">
                                    <asp:Label ID="lblTitle" runat="server" Text="Movies Report" CssClass="titleStyle"></asp:Label>
                                </td>
                                <td align="right" valign="bottom" class="divlbs">
                                    <div style="float: right">
                                        <asp:Label ID="Label1" runat="server" Text="Search Movie: "></asp:Label>
                                        &nbsp;
                                        <asp:TextBox ID="txtSearchMovie" runat="server" Text=""></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="txtWESearch" runat="server" TargetControlID="txtSearchMovie"
                                            WatermarkText="Search Here">
                                        </asp:TextBoxWatermarkExtender>
                                        &nbsp;
                                        <asp:LinkButton ID="lbSearchMovie" runat="server" CssClass="lbPaddingStyle" Text="Go"
                                            OnClick="lbSearchMovie_Click"></asp:LinkButton>
                                        &nbsp;
                                        <asp:LinkButton ID="lbShowAll" runat="server" CssClass="lbPaddingStyle" Text="Show All"
                                            OnClick="lbShowAll_Click"></asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="lbAddMovie" runat="server" Text="Add Movie" CssClass="lbPaddingStyle"
                                            OnClick="lbAddMovie_Click" Visible="false"></asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="lblErrMovie" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr valign="top">
                    <td style="height: 350px; padding-top: 5px;">
                        <div class="divlbs">
                            <asp:GridView ID="gvMovies" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                AllowSorting="True" AllowPaging="True" EnableSortingAndPagingCallbacks="True"
                                CellPadding="3" GridLines="Both" Height="100%" Width="100%" PageSize="10" OnSorting="gvMovies_Sorting"
                                OnRowCommand="gvMovies_RowCommand" OnPageIndexChanging="gvMovies_PageIndexChanging"
                                OnRowCreated="gvMovies_RowCreated" OnRowDataBound="gvMovies_RowDataBound">
                                <HeaderStyle CssClass="gvHeaderStyle" />
                                <EmptyDataTemplate>
                                    <p class="EmptyMessageStyle">No Movies Found</p>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Movie Name" SortExpression="MovieName" ItemStyle-Width="25%"
                                        HeaderStyle-Width="25%">
                                        <ItemTemplate>
                                            <asp:LinkButton CommandName="EditMovie" CommandArgument='<%#Eval("MovieID").ToString()%>'
                                                ID="lnkEditMovie" Text='<%#Eval("MovieName").ToString()%>' runat="server"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TypeName" ItemStyle-Width="15%" SortExpression="TypeName"
                                        HeaderText="Type" HeaderStyle-Width="15%"></asp:BoundField>
                                    <asp:BoundField DataField="MovieDate" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-Width="15%" SortExpression="MovieDate"
                                        HeaderText="Movie Date" HeaderStyle-Width="15%"></asp:BoundField>
                                    <asp:BoundField DataField="MovieComment" ItemStyle-Width="25%" SortExpression="MovieComment"
                                        HeaderText="Comment" HeaderStyle-Width="25%"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Rating" SortExpression="Rating">
                                        <ItemTemplate>
                                            <asp:Rating ID="ratingMovie" runat="server" MaxRating="5" ReadOnly="true" 
                                            CurrentRating='<%#Eval("Rating") == null ? 0 : Eval("Rating")%>' StarCssClass="star" EmptyStarCssClass="emptyStar" WaitingStarCssClass="waitingStar" FilledStarCssClass="star">
                                            </asp:Rating>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" />
                                    </asp:TemplateField>
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
            <UC:AddEditMovie ID="ucAddEditMovie" runat="server" OnSaveButtonClicked="ucAddEditMovie_SaveButtonClicked" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
