getDateAndTime();

function getDateAndTime() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!

    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }

    let hours = today.getHours();
    let minutes = today.getMinutes();
    let time = hours + ":" + minutes;
    var today = mm + '/' + dd + '/' + yyyy;

    $("#datetext").html("Fecha: " + today);

    $('#datetext').val(today);
    if (minutes < 10) {
        $("#hourtext").html("Hora: " + hours + ":0" + minutes);
    } else {
        $("#hourtext").html("Hora: " + time);
    }
}
$(function () {
    $('#fileId').change(function () {
        // When the user selects a file, read the selected filename
        // and set it to the textbox
        var filename = $(this).val();
        $('#fileName').val(filename);
    });
});