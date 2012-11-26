<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Activity.aspx.cs" Inherits="Agent.ActivityPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            var activityId = parseInt($('#activity-value').val()),
                projId = parseInt($('#project-value').val());
            loadRemoveActivity("#activity-dialog-edit", activityId);
            loadMarkComplete("#activity-check-edit", activityId);
            loadAddActivity("#act-derive-link", projId, activityId);
        });
    </script>
    <% if (ActivityImages.Count > 0)
       { %>
    <script type="text/javascript" src="/js/slides.js"></script>
    <script>
        $(function () {
            $("#slides").slides({
                generateNextPrev: true,
                generatePagination: false,
                play: 3000,
                hoverPause: true
            });
        });
    </script>
    <% } %>
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
    <% if (CurrentUser != null && CurrentActivity != null)
       { %>
    <p>Esta actividad pertenece al proyecto <a href="Project.aspx?view=<% Response.Write(OwnerProject.ID); %>"><% Response.Write(OwnerProject.Title); %></a></p>
    <% if (ParentActivity != null)
       { %>
    <p>Derivada de la siguiente actividad: <a href="Activity.aspx?view=<% Response.Write(ParentActivity.ID); %>"><% Response.Write(ParentActivity.Title); %></a></p>
    <% }
       else
       { %>
    <p>No deriva de ninguna actividad.</p>
    <% } %>

    <% if (ChildActivities.Count > 0)
       { %>
    <h4>Actividades derivadas</h4>
    <ul>
        <% foreach (Agent.Objects.AActivity activity in ChildActivities)
           { %>
        <li><a href="Activity.aspx?view=<% Response.Write(activity.ID); %>"><% Response.Write(activity.Title); %></a></li>
        <% } %>
    </ul>
    <% }
       else
       { %>
    <p>No posee actividades derivadas</p>
    <% } %>
    <a id="act-derive-link" href="#" class="op-link">Derivar nueva actividad</a>
    <% } %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="content" runat="server">
    <% if (CurrentUser != null && CurrentActivity != null)
       { %>
    <input id="user-value" type="hidden" value="<% Response.Write(CurrentUser.ID); %>" />
    <input id="project-value" type="hidden" value="<% Response.Write(OwnerProject.ID); %>" />
    <input id="activity-value" type="hidden" value="<% Response.Write(CurrentActivity.ID); %>" />
    <h3><% Response.Write(CurrentActivity.Title); %></h3>
    <a id="activity-dialog-edit" class="op-link" href="#">Eliminar</a>
    <p>
        Completado:
        <input id="activity-check-edit" type="checkbox" <% if (CurrentActivity.Completed) Response.Write("checked"); %> />
    </p>
    <% if (ActivityImages.Count > 0) { %>
    <div id="slides">
        <div class="slides_container">
            <% foreach (Agent.Objects.AImage image in ActivityImages)
               { %>
            <div>
                <img src="GetImage.ashx?id=<% Response.Write(image.ID); %>" class="thumbnail" />
            </div>
            <% } %>
        </div>
    </div>
    <% } %>
    <div id="activity-textblock">
        <p>Fecha: <% Response.Write(CurrentActivity.Date.Value.ToLongDateString()); %></p>
        <p>Hora: <% Response.Write(CurrentActivity.Date.Value.ToLongTimeString()); %></p>
        <p>Recordatorio: <% Response.Write(CurrentActivity.Reminder.Value.ToString()); %></p>
        <p>Prioridad: <% Response.Write(Agent.AgentUtileries.priorities[CurrentActivity.Priority]); %></p>

    </div>
    <h4>Adjuntar imagen</h4>
    <asp:FileUpload runat="server" ID="FileUpload" />
    <asp:Button runat="server" ID="BUpload" Text="Subir imagen" OnClick="BUpload_Click" />
    <% } %>
</asp:Content>
