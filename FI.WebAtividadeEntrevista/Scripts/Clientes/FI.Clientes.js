var listaBeneficiarios = [];

$(document).ready(function () {
    $('#CPF').mask('000.000.000-00');
    $('#BenefCPF').mask('000.000.000-00');
    $('#CEP').mask('00000-000');
    var maskTelefone = function (val) {
        return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
    },
        options = {
            onKeyPress: function (val, e, field, options) {
                field.mask(maskTelefone.apply({}, arguments), options);
            }
        };
    $('#Telefone').mask(maskTelefone, options);

    $('#formCadastro').submit(function (e) {
        e.preventDefault();

        var cpf = $(this).find("#CPF").val();
        var cep = $(this).find("#CEP").val();
        var telefone = $(this).find("#Telefone").val();

        if (!validarCPF(cpf)) {
            ModalDialog("Ocorreu um erro", "O CPF informado é inválido");
            return false;
        }

        if (cep.length < 9) {
            ModalDialog("Ocorreu um erro", "O CEP informado é inválido");
            return false;
        }

        if (telefone.length < 14) {
            ModalDialog("Ocorreu um erro", "O telefone informado é inválido");
            return false;
        }

        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CEP": $(this).find("#CEP").val(),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": $(this).find("#Telefone").val(),
                "CPF": $(this).find("#CPF").val(),
                "Beneficiarios": listaBeneficiarios
            },
            error:
                function (r) {
                    if (r.status == 400)
                        ModalDialog("Ocorreu um erro", r.responseJSON);
                    else if (r.status == 500)
                        ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                },
            success:
                function (r) {
                    ModalDialog("Sucesso!", r);
                    $("#formCadastro")[0].reset();
                    listaBeneficiarios = [];
                    RenderizarGridBeneficiarios();
                    window.location.href = urlRetorno;
                }
        });
    })

    $("#btnIncluirBeneficiario").click(function () {
        var nome = $("#BenefNome").val();
        var cpf = $("#BenefCPF").val();

        if (nome == "" || cpf == "") {
            ModalDialog("Atenção", "Preencha todos os campos do beneficiário.");
            return;
        }

        if (!validarCPF(cpf)) {
            ModalDialog("Atenção", "CPF do beneficiário inválido.");
            return;
        }

        // Verifica duplicidade na lista local
        var cpfLimpo = cpf.replace(/[^\d]+/g, '');
        if (listaBeneficiarios.some(b => b.CPF == cpfLimpo)) {
            ModalDialog("Atenção", "Este CPF já foi incluído na lista de beneficiários.");
            return;
        }

        var idTemp = Math.floor(Math.random() * 1000000); // ID temporário para controle em tela
        listaBeneficiarios.push({ Id: 0, Nome: nome, CPF: cpfLimpo, IdTemp: idTemp });

        $("#BenefNome").val("");
        $("#BenefCPF").val("");
        RenderizarGridBeneficiarios();
    });

})

window.AbrirModalBeneficiarios = function () {
    $("#modalBeneficiarios").modal('show');
}

function RenderizarGridBeneficiarios() {
    var tbody = $("#gridBeneficiarios tbody");
    tbody.empty();

    $.each(listaBeneficiarios, function (index, benef) {
        var cpfFormatado = benef.CPF.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
        var row = $("<tr>");
        row.append($("<td>").text(cpfFormatado));
        row.append($("<td>").text(benef.Nome));

        var btnAlterar = $("<button>").addClass("btn btn-primary btn-sm").text("Alterar").css("margin-right", "5px").click(function () {
            PrepararAlteracaoBeneficiario(benef.IdTemp || benef.Id);
        });
        var btnExcluir = $("<button>").addClass("btn btn-primary btn-sm").text("Excluir").click(function () {
            ExcluirBeneficiario(benef.IdTemp || benef.Id);
        });

        row.append($("<td>").append(btnAlterar).append(btnExcluir));
        tbody.append(row);
    });
}

function PrepararAlteracaoBeneficiario(id) {
    var benef = listaBeneficiarios.find(b => (b.IdTemp || b.Id) == id);
    if (benef) {
        $("#BenefNome").val(benef.Nome);
        var cpfFormatado = benef.CPF.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
        $("#BenefCPF").val(cpfFormatado).trigger('input');

        listaBeneficiarios = listaBeneficiarios.filter(b => (b.IdTemp || b.Id) != id);
        RenderizarGridBeneficiarios();
    }
}

function ExcluirBeneficiario(id) {
    listaBeneficiarios = listaBeneficiarios.filter(b => (b.IdTemp || b.Id) != id);
    RenderizarGridBeneficiarios();
}

window.ModalDialog = function (titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var html = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(html);
    $('#' + random).modal('show').on('hidden.bs.modal', function () {
        $(this).remove();
    });
}

function validarCPF(cpf) {
    cpf = cpf.replace(/[^\d]+/g, '');

    if (cpf == '') return false;

    if (cpf.length != 11 ||
        cpf == "00000000000" ||
        cpf == "11111111111" ||
        cpf == "22222222222" ||
        cpf == "33333333333" ||
        cpf == "44444444444" ||
        cpf == "55555555555" ||
        cpf == "66666666666" ||
        cpf == "77777777777" ||
        cpf == "88888888888" ||
        cpf == "99999999999")
        return false;

    var add = 0;

    for (var i = 0; i < 9; i++) {
        add += parseInt(cpf.charAt(i)) * (10 - i);
    }
    var rev = 11 - (add % 11);
    if (rev == 10 || rev == 11)
        rev = 0;
    if (rev != parseInt(cpf.charAt(9)))
        return false;

    add = 0;

    for (var i = 0; i < 10; i++) {
        add += parseInt(cpf.charAt(i)) * (11 - i);
    }
    rev = 11 - (add % 11);
    if (rev == 10 || rev == 11)
        rev = 0;
    if (rev != parseInt(cpf.charAt(10)))
        return false;

    return true;
}
