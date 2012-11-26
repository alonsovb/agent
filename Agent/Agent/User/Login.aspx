<%@ Page Title="Iniciar sesión" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Agent.LoginPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" runat="server">
    <h1><a href="/Default.aspx">Agent</a></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="navigation" runat="server">
    <ul class="nav-list">
        <li><a href="Login.aspx">Iniciar sesión</a></li>
        <li><a href="SignUp.aspx">Registrarse</a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="content" runat="server">
    <h3>Iniciar sesión</h3>
    <table>
        <tbody>
            <tr>
                <td>Email</td>
                <td>
                    <asp:TextBox ID="TBEmail" runat="server" TextMode="Email" CssClass="login-input"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Contraseña</td>
                <td>
                    <asp:TextBox ID="TBPassword" runat="server" TextMode="Password" CssClass="login-input"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="BLogin" runat="server" Text="Iniciar sesión" OnClick="BLogin_Click" CssClass="login-button" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
