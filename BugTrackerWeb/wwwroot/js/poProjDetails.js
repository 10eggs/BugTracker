var dataTable;


//$(document).ready(function () {
//    loadDataTable();
//})

function loadTicketTable(projectId) {
    console.log('Load data invoked!');
    if ($.fn.dataTable.isDataTable('#DT_load')) {
        console.log('if statement works!')
        //dataTable = $('#DT_load').DataTable();
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
            { "data": "id", "width": "20%" }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}

function clickMe(myVar) {
    console.log('Project id is equal to ',myVar);
}