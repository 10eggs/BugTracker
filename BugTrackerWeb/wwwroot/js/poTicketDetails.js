var qaName;

$(document).ready(function () {
    $('#qas').on('click', '.clickable-row', function (event) {
        $(this).addClass('bg-info').siblings().removeClass('bg-info');
        qaName = $(this).find('.qaName').html();
        console.log('You\'ve clicked on ', qaName);
        document.getElementById("QaName").val = qaName;
    })
})

function getSelectedQa() {
    return qaName;
}