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
        $('#assignticket').on('click', function (event) {
            //event.preventDefault();
            console.log("onClick called!")
            //$.ajax({
            //    type: "POST",
            //    url: `/ProjectOwnerView/TicketDetails?handler=AssignTicket/${ticketId}/${qaId}`,
            //    data: { 'qaId': qaId, 'ticketid' :ticketId }
            //})
            $.ajax({
                type: "POST",
                url: `/ProjectOwnerView/TicketDetails?handler=CheckAjax`,
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                data: { 'qaid': qaId, 'ticketId': ticketId },
                //success: function (result) {
                //    console.log('Success call before checking redirectToUrl')
                //    if (result.redirectToUrl !== undefined) {
                //        console.log('Success post call!')
                //        window.location.replace(result.redirectToUrl);
                //    }
                //}
            })
        })
})

function getSelectedQa() {
    return qaName;
}

function getTicketId() {
    return $('p.ticketId').html();
}