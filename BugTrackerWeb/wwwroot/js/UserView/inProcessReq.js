﻿var ticketId;

$(document).ready(function () {

    $('#requestsTable').on('click', '.clickable-row', function (event) {
        $(this).addClass('bg-info').siblings().removeClass('bg-info');
        ticketId = $(this).find('#ticketId').html();
        console.log('Selected ticket id is  ', ticketId);

        $('#ticketDetails').load(`/UserView/InProcessRequests?handler=RequestTicketDetails&id=${ticketId}`);
    })
})