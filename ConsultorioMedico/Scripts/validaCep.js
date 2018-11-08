function limpa_formulário_cep() {
    // Limpa valores do formulário de cep.
    $("#Endereco").val("");
    $("#Bairro").val("");
    $("#CidadeID").val("");
    $("#EstadoID").val("");
}

function pesquisacep(valor) {//Nova variável "cep" somente com dígitos.
    var cep = valor.replace(/\D/g, '');

    //Verifica se campo cep possui valor informado.
    if (cep != "") {

        //Expressão regular para validar o CEP.
        var validacep = /^[0-9]{8}$/;

        //Valida o formato do CEP.
        if (validacep.test(cep)) {

            //Preenche os campos com "..." enquanto consulta webservice.
            $("#Endereco").val("...");
            $("#Bairro").val("...");
            $("#CidadeID").val("...");
            $("#EstadoID").val("...");

            //Consulta o webservice viacep.com.br/
            $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                if (!("erro" in dados)) {
                    //Atualiza os campos com os valores da consulta.
                    $("#Endereco").val(dados.logradouro);
                    $("#Bairro").val(dados.bairro);
                    $("#Cidade").val(dados.localidade);
                    $("#Estado").val(dados.uf);
                } //end if.
                else {
                    //CEP pesquisado não foi encontrado.
                    limpa_formulário_cep();
                    $("#Endereco").val("CEP não encontrado.");
                }
            });
        } //end if.
        else {
            //cep é inválido.
            limpa_formulário_cep();
            $("#Endereco").val("Formato de CEP inválido.");
        }
    } //end if.
    else {
        //cep sem valor, limpa formulário.
        limpa_formulário_cep();
    }
};