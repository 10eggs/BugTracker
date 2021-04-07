//script variables
var ticketId;

//DOM ELEMENTS
const selectedTicketArticle = document.querySelector('.selected-ticket');
const btns = document.querySelectorAll('.tab-btn');
const tabs = document.querySelectorAll('.content');
//const ticketDetails = document.querySelector('article.selected-ticket-content .data-id=');
const ticketDetailsTab = $("div[id='details']")
const changeStatusTab = $("div[id='change-status']")

const ticketHistoryTab = document.querySelector('article.selected-ticket-content #history');
const ticketActionTab = document.querySelector('article.selected-ticket-content #change-status');

let ticketStatusDDL = $('#ticketstatus')
const updateTicketBtn = $('#update-ticket');
const ticketForm = $('#edit_ticket');


//function getUpTickVals() {
//    updatedTicketVals.status = $('#ticketstatus').val();
//    updatedTicketVals.description = $('#ticketdescription').val();
//    return updatedTicketVals;
//}


$(document).ready(function () {
    $('#tickets').on('click', '.clickable-row', function (event) {
        $(this).addClass('bg-info').siblings().removeClass('bg-info');
        ticketId = $(this).find('#ticketId').html();
        console.log('Selected ticket id is  ', ticketId);

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

            //Comments are reduntant. Write your code in a way where you dont need to comment it out (hypocrite-style ouuu yeee)
            //Populate details
            var detailsContent = `<h3>Ticket details</h3>
                        <li>${response.title}</li>
                        <h5>Description</h5>
                        <li>${response.description}</li>
                        <h5>Ticket status</h5>
                        <li>${response.ticketStatus}</li>
                        <h5>Author</h5>
                        <li>${response.requestAuthor}</li>`
            ticketDetailsTab.html(detailsContent);

            //Populate drop down list for ticket details
            //if (ticketStatusDDL.is(':empty')) {
            //    console.log('EMPTY!');
            //    $.each(response.ticketStatusType, function (key, entry) {
            //        ticketStatusDDL.append($('<option></option>').attr('value', key).text(entry));
            //    })
            //}


        })
    }),

        selectedTicketArticle.addEventListener('click', function (e) {
            const id = e.target.dataset.id;
            if (id) {
                btns.forEach(function (btn) {
                    btn.classList.remove('active');
                })
                e.target.classList.add('active');
                tabs.forEach(function (tab) {
                    tab.classList.remove('active')
                    if (id === tab.getAttribute('id')) {
                        tab.classList.add('active');
                    }
                })
            }
        }),

        //Button submit
        updateTicketBtn.click(async function (e) {
            e.preventDefault();
            var serializedForm = $('#edit_ticket').serialize();
            console.log('This is serialized form: ', serializedForm);

            await $.ajax({
                type: "POST",
                url: "/QAView/ProjectDetails?handler=UpdateTicket",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                dateType: 'application/json',
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',   
                data: serializedForm
            }).done(function (response) {
                console.log('RESPONSE',response);
                if (response.success) {
                    toastr.success(response.message);
                }
                else {
                    toastr.error(response.errormessage);
                }
            })
        });
});


//Pass serialized array
//function objectifyForm(formArray) {//serialize data function
//    var returnArray = {};
//    for (var i = 0; i < formArray.length; i++) {
//        returnArray[formArray[i]['name']] = formArray[i]['value'];
//    }
//    return returnArray;