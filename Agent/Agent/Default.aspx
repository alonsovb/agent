<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Client.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Agent.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        var x = $(document);
        x.ready(init);
        var userid = <%= user.ID.ToString() %>;

        function init() {
            $('#addDialog').hide();
            loadProjects();
            $('#bShowAddDialog').click(function () {
                $('#addDialog').dialog({
                    resizable: false,
                    modal: true,
                    width: 200,
                    buttons: {
                        "Agregar": function () {
                            var service = new AgenteService;
                            var title = $('#aTitle').val();
                            service.RegisterProject(userid, title, loadProjects, null, null);
                            $(this).dialog("close");
                        },
                        "Cancelar": function () {
                            $(this).dialog("close");
                        }
                    }
                });
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
    </script>
</asp:Content>
<asp:Content ID="ContentHeader" ContentPlaceHolderID="header" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
        <Services>
            <asp:ServiceReference Path="~/AgenteService.svc" />
        </Services>
    </asp:ScriptManager>
    <asp:Panel runat="server" ID="OptionPanel"></asp:Panel>
</asp:Content>
<asp:Content ID="ContentNav" ContentPlaceHolderID="nav" runat="server">
    <h2>Proyectos</h2>
    <ul id="projectList" class="navmenu">
    </ul>
    <input type="button" id="bShowAddDialog" value="Nuevo" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="addDialog" title="Nuevo proyecto">
        <input type="text" placeholder="Título" id="aTitle" />
    </div>
    <div id="app">
        <h2>Recordatorios</h2>
    </div>
</asp:Content>
