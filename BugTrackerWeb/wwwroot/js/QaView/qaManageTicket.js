var ticketId;

$(document).ready(function () {
    $('#tickets').on('click', '.clickable-row', function (event) {
        $(this).addClass('bg-info').siblings().removeClass('bg-info');
        ticketId = $(this).find('#ticketId').html();
        console.log('TicketId is  ', ticketId);

         $.ajax({
            type: "GET",
            url: "/QAView/ProjectDetails?handler=ShowDetails",
             dateType: 'json',
             beforeSend: function (xhr) {
                 xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
             },
            data: { 'ticketid': ticketId }
        }).done(function (response) {
            console.log(response);

            var item = `<li>${response.title}</li>`;
            $('#selectedTicket').append(item);
        })

    })
})

function getSelectedQa() {
    return qaName;
}

function getTicketId() {
    return $('p.ticketId').html();
}