$(document).ready(function () {
    var consultaID = $("#ConsultaID").val()
    $.get("/Prontuarios/DetalhesConsulta/" + consultaID, function (data) {
        $("#dadosConsulta").html(data);
    });
});

$("#ConsultaID").change(function () {
    var consultaID = $("#ConsultaID").val()
    $.get("/Prontuarios/DetalhesConsulta/" + consultaID, function (data) {
        $("#dadosConsulta").html(data);
    });
});