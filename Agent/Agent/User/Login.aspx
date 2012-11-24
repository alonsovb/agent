<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Agent.WebForm5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .login-wrap {
            padding: 20px;
        }

        .login-control {
            width: 100%;
        }
    </style>
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
    <div class="login-wrap">
        <h3>Login</h3>
        <table>
            <tbody>
                <tr>
                    <td>Email:</td>
                    <td>
                        <asp:TextBox ID="TBEmail" runat="server" TextMode="Email" CssClass="login-control"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Password:</td>
                    <td>
                        <asp:TextBox ID="TBPassword" runat="server" TextMode="Password" CssClass="login-control"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="BLogin" runat="server" Text="Login" OnClick="BLogin_Click" CssClass="login-control" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
