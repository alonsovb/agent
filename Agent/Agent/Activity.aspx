<%@ Page Title="" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="Activity.aspx.cs" Inherits="Agent.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        var x = $(document);
        var userid = <%= user.ID.ToString() %>;
        var activityid = <%= activity.ID %>;
        x.ready(init);

        function init() {
            $('#addDialog').hide();
            loadProjects();
            loadActivity();
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

        function loadActivity() {
            var service = new AgenteService;
            service.GetActivity(activityid, activityReceived, null, null);
        }
        function activityReceived(xml) {
            var xmlDoc = $.parseXML(xml),
                $xml = $(xmlDoc),
                activity = $xml.find('activity');
            if (activity.length > 0) {
                activity.each(function () {
                    $this = $(this);
                    $('#actTitle').text($this.children('title').text());
                });
            }
        }
    </script>
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
<asp:Content ID="ContentHeader" ContentPlaceHolderID="header" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
        <Services>
            <asp:ServiceReference Path="~/AgenteService.svc" />
        </Services>
    </asp:ScriptManager>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 id="actTitle"></h3>
    <p>Esta es la información de la actividad seleccionada.</p>
</asp:Content>
