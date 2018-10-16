<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/ReportMaster.Master" AutoEventWireup="true" CodeBehind="TypeMoviesReport.aspx.cs" Inherits="DashBoardApp.Reports.TypeMoviesReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table border='0' cellpadding='0' cellspacing='0' width='100%'>
<tr>
<td>
    <asp:Label ID="lblErr" runat="server" ForeColor="Red"></asp:Label>
</td>
    </tr>
        <tr>
            <td colspan='6' style="height: 5px; border-bottom: 1px solid blue">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan='6'>
                <asp:Label ID='lblHead' runat='server' Text="Type Movies Report"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan='6' style='height: 5px'>
            </td>
        </tr>
        <tr>
            <td colspan='6'>
                <asp:Label ID='lblType' runat='server'></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan='5' align='left'>
                <%=System.DateTime.Today.ToString("dd-MMMM-yy")%>
            </td>
            
            <td colspan='1' align='right'>
                <asp:LinkButton ID='lnkExport' runat='server' Text='Export&nbsp;Excel' 
                    onclick="lnkExport_Click"></asp:LinkButton>
                &nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID='PdfExport' runat='server' Text='Export&nbsp;PDF' 
                    onclick="PdfExport_Click"></asp:LinkButton>
                &nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID='txtExport' runat='server' Text='Export&nbsp;Text' 
                    onclick="txtExport_Click"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan='6'>
                <asp:Label ID='lblDataMsg' runat='server' ForeColor='Red'></asp:Label>
                <asp:DataList ID='dlReport' runat='server' Width='100%' OnItemDataBound='dlReport_ItemDataBound'>
                    <HeaderTemplate>
                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                            <tr id='trimg1'><td colspan='6' style="height: 5px; border-bottom: 1px solid blue;">&nbsp;</td></tr>
                            <tr id='trHeader'>
                                <td style='width: 20%; font-weight:bold;'>
                                    Movie Type
                                </td>
                                <td style='width: 20%; font-weight:bold;'>
                                    Movie Name
                                </td>
                                <td style='width: 15%; font-weight:bold;'>
                                    Movie Date
                                </td>
                                <td style='width: 35%; font-weight:bold;'>
                                    Movie Comment
                                </td>
                            </tr>
                            <tr id='trimg2'><td colspan='6' style="height: 5px; border-top: 1px solid blue; ">&nbsp;</td></tr></table>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                        <tr>
                            <td style='width: 20%; text-align: left;'>
                                <%#Eval("TypeName").ToString()%>
                            </td>                                                                   
                            <td style='width: 20%; text-align: left;'>
                                <%#Eval("MovieName").ToString()%>
                            </td>
                            <td style='width: 15%; text-align: left; '>
                                <%#String.Format("{0:dd/MM/yyyy}", Eval("MovieDate"))%>
                            </td>
                            <td style='width: 35%; text-align: left; '>
                                <%#Eval("MovieComment") == null ? "No Comment" : Eval("MovieComment").ToString() %>
                            </td>
                         </tr></table>                                   
                    </ItemTemplate>
                    <FooterTemplate>
                    <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                        </table>
                    </FooterTemplate>
                </asp:DataList>
            </td>
        </tr>
    </table>
</asp:Content>
