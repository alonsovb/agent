<%@ Page Title="Proyecto" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="Project.aspx.cs" Inherits="Agent.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        var x = $(document);
        var userid = <%= user.ID.ToString() %>;
        var projectid = <%= _project.ID %>;
        x.ready(init);

        function init() {
            $('#addDialog').hide();
            loadProjects();
            loadActivities();
            $('#addActivity').click(function () {
                $('#addDialog').dialog({
                    resizable: false,
                    modal: true,
                    width: 200,
                    buttons: {
                        "Agregar": function () {
                            var service = new AgenteService;
                            var title = $('#title').val();
                            var priority = $('#priority').val();
                            service.RegisterActivity(projectid, title, priority, loadActivities, null, null);
                            $(this).dialog("close");
                        },
                        "Cancelar": function () {
                            $(this).dialog("close");
                        }
                    }
                });
            });
            $('#reminderDate').datepicker();
            $('#baddActivity').click(function () {

            });
        }

        function loadProjects() {
            var service = new AgenteService;
            service.GetProjects(userid, projectsReceived, null, null);
        }
        function projectsReceived(xml) {
            $('#projectList').html('');
            var xmlDoc = $.parseXML(xml),
                $xml = $(xmlDoc),
                projects = $xml.find('project');
            if (projects.length > 0) {
                projects.each(function () {
                    var $this = $(this);
                    var li = $('<li/>');
                    var a = $('<a/>', {
                        text: $this.children('title').text(),
                        href: 'Project.aspx?id=' + $this.children('id').text()
                    });
                    li.append(a);
                    $('#projectList').append(li);
                });
            }
        }

        function loadActivities() {
            var service = new AgenteService;
            service.GetActivities(projectid, activitiesReceived, null, null);
        }
        function activitiesReceived(xml) {
            $('#activityList').html('');
            var xmlDoc = $.parseXML(xml),
                $xml = $(xmlDoc),
                projects = $xml.find('activity');
            if (projects.length > 0) {
                projects.each(function () {
                    var $this = $(this);
                    var li = $('<li/>');
                    var a = $('<a/>', {
                        text: $this.children('title').text(),
                        href: 'Activity.aspx?id=' + $this.children('id').text()
                    });
                    li.append(a);
                    $('#activityList').append(li);
                });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="ContentHeader" ContentPlaceHolderID="header" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
        <Services>
            <asp:ServiceReference Path="~/AgenteService.svc" />
        </Services>
    </asp:ScriptManager>
</asp:Content>
<asp:Content ID="ContentNav" ContentPlaceHolderID="nav" runat="server">
    <h2>Próximas actividades</h2>
    <ul class="navmenu">
        <li><a href="#">Hoy</a></li>
        <li><a href="#">Esta semana</a></li>
        <li><a href="#">Este mes</a></li>
    </ul>
    <h2>Proyectos</h2>
    <ul id="projectList" class="navmenu">
    </ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="addDialog" title="Agregar actividad">
        <input type="text" id="title" placeholder="Title" />
        <select name="priority" id="priority">
            <option value="0">Indefinido</option>
            <option value="1">Importancia baja</option>
            <option value="2">Regular</option>
            <option value="3">Importante</option>
        </select>
        <input type="text" id="reminderDate" />
    </div>

    <h2><%= _project.Title %></h2>
    <h3>Actividades</h3>
    <input type="button" id="addActivity" value="Agregar actividad" />
    <ul id="activityList">
    </ul>
</asp:Content>
