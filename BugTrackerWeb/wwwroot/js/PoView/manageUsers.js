$('.show-team-members').on('click', function (event) {
    var id = $(this).attr('projectid');
    $('#details').load(`/ProjectOwnerView/ManageUsers?handler=TeamMembers&id=${id}`);
})

$('.show-team-members').on('click', function (event) {
    $('#details').load(`/ProjectOwnerView/ManageUsers?handler=AddMember`);
})