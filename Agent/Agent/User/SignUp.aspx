<%@ Page Title="Registro" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="Agent.SignUpPage" %>

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
    <h3>Registro</h3>
    <table>
        <tbody>
            <tr>
                <td>Email</td>
                <td>
                    <asp:TextBox ID="TBEmail" runat="server" TextMode="Email" CssClass="login-input" /></td>
            </tr>
            <tr>
                <td>Contraseña</td>
                <td>
                    <asp:TextBox ID="TBPassword" runat="server" TextMode="Password" CssClass="login-input" /></td>
            </tr>
            <tr>
                <td>Nombre</td>
                <td>
                    <asp:TextBox ID="TBName" runat="server" TextMode="SingleLine" CssClass="login-input" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="BRegister" runat="server" Text="Completar registro" OnClick="BRegisterUser_Click" CssClass="login-button" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
