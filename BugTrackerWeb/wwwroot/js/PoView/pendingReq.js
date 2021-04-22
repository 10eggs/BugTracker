$('.show-details').on('click', function (event) {
    var id = $(this).attr('data-id');
    $('#ticketDetails').load(`/ProjectOwnerView/PendingRequests?handler=RequestItemDetails&id=${id}`);
})