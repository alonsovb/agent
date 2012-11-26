<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ByDate.aspx.cs" Inherits="Agent.ByDatePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="header" runat="server">
    <!-- Cargar menú de usuario. -->
    <h1><a href="/Default.aspx">Agent</a></h1>
    <% if (this.CurrentUser != null)
       { %>
    <h2 class="header-option"><a href="User/Profile.aspx"><% Response.Write(this.CurrentUser.Name); %></a></h2>
    <h2 class="header-option"><a href="User/Logout.aspx">Cerrar sesión</a></h2>
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
        <li><a href="Project.aspx?view=<% Response.Write(project.ID); %>"><% Response.Write(project.Title); %></a></li>
        <% } %>
    </ul>
    <h3>Próximas actividades</h3>
    <ul class="nav-list">
        <li><a href="/ByDate.aspx?for=today">Hoy</a></li>
        <li><a href="/ByDate.aspx?for=week">Esta semana</a></li>
        <li><a href="/ByDate.aspx?for=month">Este mes</a></li>
    </ul>
    <% } %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="content" runat="server">
    <% if (CurrentUser != null)
       { %>
    <h3>Actividades para <% Response.Write(TimeText); %></h3>
    <ul>
        <% foreach (Agent.Objects.AActivity activity in ActivityList)
           { %>
        <li><a href="/Activity.aspx?view=<% Response.Write(activity.ID); %>"><% Response.Write(activity.Title); %></a></li>
        <% } %>
    </ul>
    <% } %>
</asp:Content>
