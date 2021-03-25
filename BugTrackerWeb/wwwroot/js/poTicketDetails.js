var qaId;
var ticketId;

$(document).ready(function () {
    $('#qas').on('click', '.clickable-row', function (event) {
        $(this).addClass('bg-info').siblings().removeClass('bg-info');
        qaId = $(this).find('#qaId').html();
        ticketId = $(document).find('#ticketId').html();
        console.log('You\'ve clicked on ', qaId);
        console.log('TicketId is  ', ticketId);
        
    }),
    $('#assignticket').on('click', async function (event) {
        event.preventDefault();
        console.log("onClick called!")
        const response = await $.ajax({
            type: "POST",
            url: `/ProjectOwnerView/TicketDetails?handler=AssignTicket`,
            dataType: 'json',
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { 'qaid': qaId, 'ticketId': ticketId },
        }).done(function (response) {
            $('#assignticket').prop('disabled', true);
                if (response.success) {
                    toastr.success(response.message);
                }
                else {
                    toastr.error(response.errormsg);
                }
        })
        setTimeout(function () {
            window.location = "https://localhost:44333/ProjectOwnerView"
        },3000);
    })
})

function getSelectedQa() {
    return qaName;
}

function getTicketId() {
    return $('p.ticketId').html();
}