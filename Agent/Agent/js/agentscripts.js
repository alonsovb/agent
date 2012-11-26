function reloadPage() {
    window.location.reload();
}
function lastPage() {
    window.history.back();
}

function loadRemoveProject(linkId, projectId) {
    $('<div />', { id: "project-dialog-remove", title: "Eliminar proyecto", text: "¿Realmente desea eliminar este proyecto?" })
        .css("display", "none")
        .appendTo(document.body);

    $(linkId).click(function (e) {
        e.preventDefault();
        $('#project-dialog-remove').dialog({
            modal: true,
            buttons: {
                "Eliminar": function () {
                    var service = new AgenteService;
                    service.DeleteProject(projectId, lastPage, null, null);
                }
            }
        });
    });
}

function loadEditProject(linkId, projectId) {
    $('<div />', { id: "project-dialog-edit", title: "Editar título", text: "Seleccione el nuevo título del proyecto:" })
        .css("display", "none")
        .append($('<input />', { type: "text", id: "proj-dialog-newtitle", class: "dialog-input", placeholder: "Nuevo título" }))
        .appendTo(document.body);

    $(linkId).click(function (e) {
        e.preventDefault();
        $('#project-dialog-edit').dialog({
            modal: true,
            buttons: {
                "Actualizar": function () {
                    var service = new AgenteService;
                    var newTitle = $('#proj-dialog-newtitle').val();
                    service.UpdateProjectTitle(projectId, newTitle, reloadPage, null, null);
                }
            }
        });
    });
}

function loadEditUser(linkId, userId) {
    $('<div />', { id: "user-dialog-edit", title: "Cambiar nombre", text: "Seleccione el nombre que desea utilizar:" })
        .css("display", "none")
        .append($('<input />', { type: "text", id: "user-dialog-newname", class: "dialog-input", placeholder: "Nuevo nombre" }))
        .appendTo(document.body);

    $(linkId).click(function (e) {
        e.preventDefault();
        $('#user-dialog-edit').dialog({
            modal: true,
            buttons: {
                "Actualizar": function () {
                    var service = new AgenteService;
                    var newName = $('#user-dialog-newname').val();
                    service.UpdateUser(userId, newName, reloadPage, null, null);
                }
            }
        });
    });
}

function loadRemoveActivity(linkId, activityId) {
    $('<div />', { id: "activity-dialog-remove", title: "Eliminar actividad", text: "¿Realmente desea eliminar la actividad seleccionada?" })
        .css("display", "none")
        .appendTo(document.body);

    $(linkId).click(function (e) {
        e.preventDefault();
        $('#activity-dialog-remove').dialog({
            modal: true,
            buttons: {
                "Eliminar": function () {
                    var service = new AgenteService;
                    service.DeleteActivity(activityId, lastPage, null, null);
                }
            }
        });
    });
}

function loadMarkComplete(linkId, activityId) {
    $(linkId).click(function () {
        var completed = (this.checked) ? 1 : 0;
        completeActivity(activityId, completed);
    });
}

function completeActivity(activityId, completed) {
    var service = new AgenteService;
    service.UpdateCompleted(activityId, completed);
}

function loadAddActivity(linkId, projectId, parentActivity) {
    var prioritySelect = $('<select />', { id: "act-dialog-priority", class: "dialog-input" })
        .append($('<option />', { value: 0, text: "Indefinido"}))
        .append($('<option />', { value: 1, text: "Baja"}))
        .append($('<option />', { value: 2, text: "Regular"}))
        .append($('<option />', { value: 3, text: "Alta"}));
    $('<div />', { id: "activity-dialog-add", title: "Nueva actividad", text: "Ingrese los datos para la nueva actividad" })
        .css("display", "none")
        .append($('<input />', { type: "text", id: "act-dialog-title", class: "dialog-input", placeholder: "Nuevo título" }))
        .append($('<p>Prioridad:</p>'))
        .append(prioritySelect)
        .append($('<p>Fecha</p>'))
        .append($('<input />', { type: "text", id: "act-dialog-due", class: "dialog-input" }))
        .append($('<p>Recordatorio</p>'))
        .append($('<input />', { type: "text", id: "act-dialog-reminder", class: "dialog-input" }))
        .appendTo(document.body);
    var timeoptions = {
        dateFormat: "dd/mm/yy",
        timeFormat: "hh:mm tt",
        showOtherMonths: true,
        numberOfMonths: 2,
        stepMinute: 10
    };
    $('#act-dialog-due').datetimepicker(timeoptions);
    $('#act-dialog-reminder').datetimepicker(timeoptions);


    $(linkId).click(function (e) {
        e.preventDefault();
        $('#activity-dialog-add').dialog({
            modal: true,
            buttons: {
                "Aceptar": function () {
                    var service = AgenteService,
                        title = $('#act-dialog-title').val(),
                        priority = parseInt($('#act-dialog-priority').val()),
                        due = $('#act-dialog-due').val(),
                        rem = $('#act-dialog-reminder').val();
                    service.RegisterActivity(projectId, title, parentActivity, priority, due, rem, 0, reloadPage, null, null);
                }
            }
        });
    });
}

$(document).ready(function () {
    var userid = parseInt($('#user-value').val());
    $('#nav-project-list').append('<li><a id=\'new-project-link\' href=\'#\'>Nuevo proyecto</a></li>');
    $('#new-project-link').click(function (e) {
        e.preventDefault();
        $('#proj-dialog').dialog({
            modal: true,
            buttons: {
                "Aceptar": function () {
                    var title = $('#proj-dialog-title').val();
                    var service = new AgenteService;
                    service.RegisterProject(userid, title, reloadPage, null, null);
                }
            }
        });
    });
    $('<div />', { id: "proj-dialog", title: "Nuevo proyecto" })
        .css("display", "none")
        .append($('<input />', { type: "text", id: "proj-dialog-title", class: "dialog-input", placeholder: "Título" }))
        .appendTo(document.body);
});
