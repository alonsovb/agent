<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Project.aspx.cs" Inherits="Agent.ProjectPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            var projId = parseInt($('#project-value').val());
            loadRemoveProject("#proj-dialog-remove", projId);
            loadEditProject("#proj-dialog-edit", projId);
            loadAddActivity("#act-dialog-link", projId, 0);
            var total = parseInt($('#project-total').val()),
                completed = parseInt($('#project-completed').val());
            $('#progress').progressbar({
                max: total,
                value: completed
            });
        });
    </script>
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
    <% if (this.CurrentUser == null)
       { %>
    <p>Para utilizar Agent debes tener una cuenta registrada e iniciar sesión. Si ya tienes una cuenta, procede a iniciar sesión:</p>
    <a href="User/Login.aspx">Iniciar sesión</a>
    <p>Si aún no tienes una cuenta, puedes registrarte, es completamente gratuito:</p>
    <a href="User/SignUp.aspx">Registrarme</a>
    <% }
       else
       { %>
    <input type="hidden" id="user-value" value="<% Response.Write(CurrentUser.ID); %>"/>
    <% if (CurrentProject != null)
       { %>
    <input type="hidden" id="project-value" value="<% Response.Write(CurrentProject.ID); %>"/>
    <input type="hidden" id="project-total" value="<% Response.Write(ActivityList.Count); %>"/>
    <input type="hidden" id="project-completed" value="<% Response.Write(Completed); %>" />
    <h3><%= CurrentProject.Title %></h3>
    <div id="progress"></div>
    <a id="proj-dialog-edit" class="op-link" href="#">Renombrar</a>
    <a id="proj-dialog-remove" class="op-link" href="#">Eliminar</a>
    <h4>Actividades</h4>
    <a id="act-dialog-link" href="#" class="op-link">Agregar actividad</a>
    <ul class="main-project-activity-list">
        <% foreach (Agent.Objects.AActivity activity in ActivityList)
           { %>
        <li><a <% if (activity.Completed) Response.Write("class='completed'"); %> href="Activity.aspx?view=<% Response.Write(activity.ID); %>"><% Response.Write(activity.Title); %></a></li>
        <% } %>
    </ul>
    <% } %>
    <% } %>
</asp:Content>
