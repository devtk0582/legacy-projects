<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/FormMaster.master" AutoEventWireup="true"
    CodeBehind="CertificateForm.aspx.cs" Inherits="DashBoardApp.Forms.CertificateForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <table width="100%">
        <tr>
            <td colspan="2">
                <asp:Label ID="lblHead" runat="server" Text="Certificate Form"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table style="border: 1px solid gray; width: 100%; height: 400px;">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Company Name"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="CompanyName" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style10" colspan="2" align="left">
                            <asp:Label ID="Label2" runat="server" Text="PDF Template"></asp:Label>
                            :
                        </td>
                    </tr>
                    <tr>
                        <td class="style6" colspan="2" align="center" valign="top">
                            <asp:CheckBox ID="Form4506" runat="server" Text="Form 4506" />
                            <br />
                            <br />
                            <br />
                            <asp:CheckBox ID="Merge" runat="server" Text="Merge" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style8" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" class="style11" valign="top">
                            <asp:Label ID="Label3" runat="server" Text="Request 1:"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="FirstYear" runat="server" Height="29px" OnSelectedIndexChanged="FirstYear_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                            <br />
                            <br />
                            <asp:Label ID="Label4" runat="server" Text="Request 2:"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="SecondYear" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style12" colspan="2" align="center">
                            <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:DataList ID="DLForm" runat="server" OnItemCommand="dlUpload_ItemCommand" Width="100%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgIcon" runat="server" ImageUrl="~/Image/PDFImg.jpg" CommandName="ClickImg" /><br />
                                    <asp:Label ID="lblDoc_Type_Name" runat="server" Text='<%# Bind("FileName") %>' />
                                    <asp:Label ID="lblDocExtx" runat="server" Visible="false" Text='<%# Bind("Path") %>' />
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="border-style: solid">
                <iframe width="100%" height="440px" name="preview" id="iframePreview" runat="server"
                    visible="false"></iframe>
            </td>
        </tr>
    </table>--%>
</asp:Content>
