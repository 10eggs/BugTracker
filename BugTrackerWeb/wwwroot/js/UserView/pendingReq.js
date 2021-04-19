var dataTable;

//$(document).ready(function () {
//    loadDataTable();
//})

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/UserView/PendingRequests?handler=PopulateTable",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "title", "width": "30%" },
            { "data": "description", "width": "30%" },
            { "data": "project", "width": "30%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/UserView/EditRequest?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:80px;'>
                            Edit
                        </a>
                        &nbsp;
                        </br>
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick=Delete(${data})>
                            Delete
                        </a>
                        </div>`;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}

//function Delete(data) {
//    swal({
//        title: "Are you sure?",
//        text: "Once deleted, you will not be able to recover",
//        icon: "warning",
//        buttons: true,
//        dangerMode: true,
//    }).then((willDelete) => {
//        if (willDelete) {
//            $.ajax({
//                type: "POST",
//                buttons: true,
//                url: `/UserView/DeleteRequest?handler=Delete&id=${data}`,
//                beforeSend: function (xhr) {
//                    xhr.setRequestHeader("XSRF-TOKEN",
//                        $('input:hidden[name="__RequestVerificationToken"]').val());
//                },
//            }).done(function (response) {
//                if (response.success) {
//                    toastr.success(response.message);
//                    dataTable.ajax.reload();
//                }
//                else
//                    toastr.error(response.message);
//            })
//        }
//    })
//};

function Delete(data) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "POST",
                buttons: true,
                url: `/UserView/PendingRequests?handler=Delete&id=${data}`,
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
            }).done(function (response) {
                if (response.success) {
                    toastr.success(response.message);
                }
                else {
                    toastr.error(response.message);
                }
                setTimeout(function () {
                    location.reload();
                },3000)
            })
        }
    });
}

$('.show-details').on('click', function (event) {
    var id = $(this).attr('data-id')
    $('#ticketDetails').load(`/UserView/PendingRequests?handler=RequestItemDetails&id=${id}`);
})