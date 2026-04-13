
$(document).ready(function () {

    if (document.getElementById("gridClientes"))
        $('#gridClientes').jtable({
            title: 'Clientes',
            paging: true, //Enable paging
            pageSize: 5, //Set page size (default: 10)
            sorting: true, //Enable sorting
            defaultSorting: 'Nome ASC', //Set default sorting
            actions: {
                listAction: urlClienteList,
            },
            fields: {
                Nome: {
                    title: 'Nome',
                    width: '50%'
                },
                Email: {
                    title: 'Email',
                    width: '35%'
                },
                Alterar: {
                    title: '',
                    display: function (data) {
                        return '<button onclick="window.location.href=\'' + urlAlteracao + '/' + data.record.Id + '\'" class="btn btn-primary btn-sm">Alterar</button>';
                    }
                },
                Excluir: {
                    title: '',
                    display: function (data) {
                        return '<button onclick="ExcluirCliente(' + data.record.Id + ')" class="btn btn-primary btn-sm">Excluir</button>';
                    }
                }
            }
        });

    window.ExcluirCliente = function (id) {
        if (confirm("Deseja realmente excluir este cliente?")) {
            $.ajax({
                url: urlExclusao,
                method: "POST",
                data: { id: id },
                error: function (r) {
                    if (r.status == 400)
                        alert(r.responseJSON);
                    else if (r.status == 500)
                        alert("Ocorreu um erro interno no servidor.");
                },
                success: function (r) {
                    if (r.Result == "OK") {
                        alert(r.Message);
                        $('#gridClientes').jtable('load');
                    } else {
                        alert(r.Message);
                    }
                }
            });
        }
    }

    //Load student list from server
    if (document.getElementById("gridClientes"))
        $('#gridClientes').jtable('load');
})