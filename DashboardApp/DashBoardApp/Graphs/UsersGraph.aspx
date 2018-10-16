﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Graphs/GraphMaster.master" AutoEventWireup="true"
    CodeBehind="UsersGraph.aspx.cs" Inherits="DashBoardApp.Graphs.UsersGraph" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="vertical-align: top">
        <tr>
            <td align="center">
                <asp:LinkButton ID="lbTitle" runat="server" Text="Users Graph" CssClass="graphTitle"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td align="center">
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
            <td align='center' valign="top">
                <asp:Label ID="lblEmtMsg" runat="server" ForeColor="Red"></asp:Label>
                <asp:Literal ID="FCChart" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="center" valign="top" style="height: 200px;">
                    <asp:DataGrid ID="dgChart" ShowFooter="True" runat="server" AutoGenerateColumns="false"
                     ForeColor="#333333" GridLines="None" Width="100%"
                        Font-Names="Verdana" Font-Size="11px">
                        <Columns>
                            <asp:BoundColumn DataField="StateName" SortExpression="StateName" HeaderText="State Name">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="UserCount" SortExpression="UserCount" HeaderText="Number Of Users">
                            </asp:BoundColumn>
                        </Columns>
                        <ItemStyle CssClass="dlItemStyle" />
                        <HeaderStyle CssClass="dlHeaderStyle" />
                        <AlternatingItemStyle CssClass="dlAlterItemStyle" />
                        <FooterStyle CssClass="dlFooterStyle" />
                    </asp:DataGrid>
             </td>
        </tr>
    </table>
</asp:Content>