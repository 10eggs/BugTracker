var dataTable;


//$(document).ready(function () {
//    loadDataTable();
//})

function loadTicketTable(projectId) {
    console.log("Project id is",projectId);
    if ($.fn.dataTable.isDataTable('#DT_load')) {
        dataTable.destroy()
    }

    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/projectownerview" + `/${projectId}`,
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
                        <a href="/ProjectOwnerView/TicketDetails?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:80px;'>
                            Details
                        </a>
                        </div>`;
                }, "width": "20%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}