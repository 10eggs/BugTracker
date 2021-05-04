//$('.show-team-members').on('click', async function (event) {
//    var id = $(this).attr('projectid');
//    await $('#details').load(`/ProjectOwnerView/ManageUsers?handler=TeamMembers&id=${id}`);
//})

//$('.show-team-members').on('click', async function (event) {
//    await $('#details').load(`/ProjectOwnerView/ManageUsers?handler=AddMember`);
//})


function Delete(qaId,projectId) {
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
                url: `/ProjectOwnerView/ManageUsers?handler=DischargeQa&qaId=${qaId}&projectId=${projectId}`,
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
                }, 2000)
            })
        }
    });
}