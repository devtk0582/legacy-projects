<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="DashBoardApp.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="height: 250px; background: #FFF url('../images/MainBG2.gif') repeat-x;">
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr style="height: 30px;">
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" cellpadding="3px" cellspacing="3px">
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ibtnUsers" runat="server" Width="64px" Height="64px" ImageUrl="~/images/Users.gif" PostBackUrl="~/Graphs/UsersGraph.aspx" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lbUsers" runat="server" Text="Users" PostBackUrl="~/Graphs/UsersGraph.aspx"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ibtnMovies" runat="server" Width="64px" Height="64px" 
                                            ImageUrl="~/images/Movies.gif" PostBackUrl="~/Graphs/MoviesGraph.aspx" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lbMovies" runat="server" Text="Movies" PostBackUrl="~/Graphs/MoviesGraph.aspx"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ibtnTeams" runat="server" Width="64px" Height="64px" 
                                            ImageUrl="~/images/Teams.gif" PostBackUrl="~/Graphs/TeamsGraph.aspx" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lbTeams" runat="server" Text="Teams" PostBackUrl="~/Graphs/TeamsGraph.aspx"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ibtnStates" runat="server" Width="64px" Height="64px" 
                                            ImageUrl="~/images/States.gif" PostBackUrl="~/Graphs/StatesGraph.aspx" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lbStates" runat="server" Text="States" PostBackUrl="~/Graphs/StatesGraph.aspx"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
