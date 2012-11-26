<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Agent.DefaultPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="header" runat="server">
    <!-- Cargar menú de usuario. -->
    <h1><a runat="server" href="~/Default.aspx">Agent</a></h1>
    <% if (this.CurrentUser != null)
       { %>
    <h2 class="header-option"><a runat="server" href="~/User/Profile.aspx"><% Response.Write(this.CurrentUser.Name); %></a></h2>
    <h2 class="header-option"><a runat="server" href="~/User/Logout.aspx">Cerrar sesión</a></h2>
    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="navigation" runat="server">
    <!-- Cargar en la navegación los proyectos y lista de actividades próximas -->
    <% if (this.CurrentUser != null)
       { %>
    <h3>Proyectos</h3>
    <ul id="nav-project-list" class="nav-list">
        <% foreach (Agent.Objects.AProject project in Projects)
           { %>
        <li><a  href="Project.aspx?view=<% Response.Write(project.ID); %>"><% Response.Write(project.Title); %></a></li>
        <% } %>
    </ul>
    <h3>Próximas actividades</h3>
    <ul class="nav-list">
        <li><a runat="server" href="~/ByDate.aspx?for=today">Hoy</a></li>
        <li><a runat="server" href="~/ByDate.aspx?for=week">Esta semana</a></li>
        <li><a runat="server" href="~/ByDate.aspx?for=month">Este mes</a></li>
    </ul>
    <% } %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="content" runat="server">
    <% if (this.CurrentUser == null)
       { %>
    <p>Para utilizar Agent debes tener una cuenta registrada e iniciar sesión. Si ya tienes una cuenta, procede a iniciar sesión:</p>
    <a runat="server" href="~/User/Login.aspx">Iniciar sesión</a>
    <p>Si aún no tienes una cuenta, puedes registrarte, es completamente gratuito:</p>
    <a runat="server" href="~/User/SignUp.aspx">Registrarme</a>
    <% }
       else
       { %>
    <input type="hidden" id="user-value" value="<% Response.Write(CurrentUser.ID); %>" />
    <h3>Bienvenido, <%= CurrentUser.Name %>.</h3>
    <% if (Reminders.Count > 0)
       { %>
    <h3>Recordatorios para hoy:</h3>
    <ul>
        <% foreach (Agent.Objects.AActivity activity in Reminders)
           { %>
        <li><a href="/Activity.aspx?view=<% Response.Write(activity.ID); %>"><% Response.Write(activity.Title); %></a></li>
        <% } %>
    </ul>
    <% }
       else
       { %>
    <p>No hay recordatorios para el día de hoy.</p>
    <% } %>
    <% } %>
</asp:Content>
