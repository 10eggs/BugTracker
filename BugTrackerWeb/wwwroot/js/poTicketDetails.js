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
                url: `/ProjectOwnerView/TicketDetails?handler=CheckAjax`,
                dataType: 'json',
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                data: { 'qaid': qaId, 'ticketId': ticketId },
                //success: function (result) {
                //    console.log('SUCCESS!!!!')
                //},
                //error: function (result) {
                //    console.log('Error is here!')
                //},
                //complete: function (result) {
                //    console.log('Completet!')
                //}
                //Weird response here ... ;)
                //success: function (result) {
                //    console.log('Success call before checking redirectToUrl')
                //    if (result.redirectToUrl !== undefined) {
                //        console.log('Success post call!')
                //        window.location.replace(result.redirectToUrl);
                //    }
                //}
                //success: function (response) {
                //    if (respons == true)
                //        console.log('This is the response: ', response)
                //    window.location = "https://localhost:44333/ProjectOwnerView";
                //}
            }).done(function (response){
                $('#response').html(response);
                window.location = 'https://localhost:44333/ProjectOwnerView';
             })

            //if (response.ok) {
            //    console.log('HOLA!')
            //}
                //.done(function (result) {
                //    console.log('Ajax call is done!')
                //    window.location = "https://localhost:44333/ProjectOwnerView"
                //})
        })
})

function getSelectedQa() {
    return qaName;
}

function getTicketId() {
    return $('p.ticketId').html();
}