<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/FormMaster.master" AutoEventWireup="true" CodeBehind="PrintMovies.aspx.cs" Inherits="DashBoardApp.Forms.PrintMovies" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
                <tr>
            <td align="left" colspan="2">
                <asp:Label ID="lblErr" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>
            </td>
        </tr>
    <tr>
        <td align="left" colspan="2">
            <asp:Label ID="Label1" runat="server" Text="Print Movie Information"></asp:Label>
        </td>
    </tr>
    <tr valign="top">
        <td width="200px">
            <table width="100%" cellpadding="5" cellspacing="2" style="text-align: left;">
                <tr>
                    <td>
                        Movie Types:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMovieTypes" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Year:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlYear" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Month:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMonth" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Review:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReview" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="padding: 20px 0 20px 0;">
                    <td align="center">
                        <asp:LinkButton ID="lbGetPDF" runat="server" Text="Preview" 
                            onclick="lbGetPDF_Click" CssClass="lbPaddingStyle"></asp:LinkButton>
                    </td>
                    <td align="center">
                        <asp:LinkButton ID="lbDownloadPDF" runat="server" Text="Download" 
                            onclick="lbDownloadPDF_Click" CssClass="lbPaddingStyle"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:DataList ID="DLForm" runat="server" Width="100%" RepeatDirection="Horizontal" RepeatColumns="3" OnItemCommand="DLForm_ItemCommand">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgIcon" runat="server" ImageUrl="../images/PDFImg.jpg" CommandName="Preview" /><br />
                                        <asp:Label ID="lblDocName" runat="server" Text='<%# Bind("Key") %>' />
                                        <asp:HiddenField ID="hfPath" runat="server" Value='<%# Bind("Value") %>'/>
                                     </ItemTemplate>
                         </asp:DataList>
                    </td>
                </tr>
            </table>
        </td>
        <td width="400px">
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:Image ID="imgPreview" runat="server" ImageUrl="~/images/Preview.gif" />
                </td>
            </tr>
            <tr>
                            <td>
                      <iframe width="100%" height="400px" name="preview" id="iframePreview" runat="server"
                            visible="false"></iframe>
                </td>
            </tr>
        </table>
        </td>
    </tr>
</table>
</asp:Content>
