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


ticketTitle = $('#ticketTitle');
ticketDesc = $('#ticketDescription');
ticketCategory = $('#ticketCategory');
ticketPriority = $('#ticketPriority');
ticketStatus = $('#ticketStatus');
ticketAuthor = $('#ticketAuthor');

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
            ticketTitle.text(response.title);
            ticketDesc.text(response.description);
            ticketCategory.text(response.ticketCategory);
            ticketPriority.text(response.ticketPriority);
            ticketStatus.text(response.ticketStatus);
            ticketAuthor.text(response.author);
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
            serializedForm += "&EditedTicket.TicketId=" + ticketId;
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


