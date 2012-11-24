<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Agent.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="nav" runat="server">
    <h2>User</h2>
    <ul>
        <asp:Menu ID="ProjectsMenu" runat="server">
            <StaticMenuStyle CssClass="menu" />
            <StaticMenuItemStyle CssClass="normal" />
            <StaticHoverStyle BackColor="#E0E0E0" />
            <Items>
                <asp:MenuItem Text="Login" NavigateUrl="~/User/Login.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Registration" NavigateUrl="~/User/Registration.aspx"></asp:MenuItem>
            </Items>
        </asp:Menu>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Registro</h3>
    <table>
        <tbody>
            <tr>
                <td>Email:</td>
                <td>
                    <asp:TextBox ID="TBEmail" runat="server" TextMode="Email"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Password:</td>
                <td>
                    <asp:TextBox ID="TBPassword" runat="server" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Name:</td>
                <td>
                    <asp:TextBox ID="TBName" runat="server" TextMode="SingleLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button runat="server" Text="Register" OnClick="BRegisterUser_Click" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
