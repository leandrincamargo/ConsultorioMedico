$("#EspecialidadeID").change(function () {
    var id = $(this).find(":selected").val();
    $.ajax({
        url: "/Consultas/ObterMedico/" + id,
        success: function (data) {
            $("#MedicoID").empty();
            $.each(data, function (index, element) {
                $("#MedicoID").append('<option value="' + element.Value + '">' + element.Text + '</option>');
            });
        }
    });
});

$("#dataConsulta").change(function () {
    var medicoId = $("#MedicoID").find(":selected").val();
    var dataConsulta = $(this).val();

    var consultaId = null;
    var dataOriginal = null;
    var horarioOriginal = null;
    var selecionar = false;

    if ($("#ConsultaID").val() != undefined) {
        consultaId = $("#ConsultaID").val();
        dataOriginal = $("#dataOriginal").val();
        horarioOriginal = $("#horarioOriginal").val();
        selecionar = dataOriginal == dataConsulta ? true : false;
    }

    $("#horarioConsulta").empty();
    if (dataConsulta == "")
        $("#horarioConsulta").append('<option value=-1>Selecione o Médico e a Data</option>');
    else {
        $.ajax({
            type: "GET",
            url: "/Consultas/ObterHorario/",
            data: { "medicoId": medicoId, "dataConsulta": dataConsulta, "consultaId": consultaId },
            success: function (data) {
                $.each(data, function (index, element) {
                    if (selecionar && horarioOriginal == element.Value) {
                        $("#horarioConsulta").append('<option selected="true" value="' + element.Value + '">' + element.Text + '</option>');
                    }
                    else {
                        $("#horarioConsulta").append('<option value="' + element.Value + '">' + element.Text + '</option>');
                    }
                });
            }
        });
    }
});