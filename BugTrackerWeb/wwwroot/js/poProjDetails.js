var requestsTable;
var ticketsTable;
var projectId;


//$(document).ready(function () {
//    loadDataTable();
//})

function loadProjectDetails(projectId) {
    console.log("Project id is", projectId);
    projectId = projectId;
    if ($.fn.dataTable.isDataTable('#DT_load')) {
        requestsTable.destroy()
    }
    if ($.fn.dataTable.isDataTable('#DT_AT')) {
        ticketsTable.destroy()
    }

    requestsTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/projectownerview/requests" + `/${projectId}`,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "title", "width": "30%" },
            { "data": "description", "width": "30%" },
            { "data": "author", "width": "30%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/ProjectOwnerView/TicketDetails?handler=RequestDetails&projectId=${projectId}&requestId=${data}" class='btn btn-success text-white' style='cursor:pointer; width:80px;'>
                            Assign
                        </a>
                        </div>`
                }, "width": "20%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });

    ticketsTable = $('#DT_AT').DataTable({
        "ajax": {
            "url": "/api/projectownerview/tickets" + `/${projectId}`,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "title", "width": "30%" },
            { "data": "description", "width": "30%" },
            { "data": "author", "width": "30%" },
            { "data": "qaName", "width": "30%" }
            
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    })
}