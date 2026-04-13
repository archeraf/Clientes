using System.Collections.Generic;

namespace FI.AtividadeEntrevista.DAL.Interfaces
{
    /// <summary>
    /// Interface para acesso a dados de Cliente
    /// </summary>
    public interface IDaoCliente
    {
        long Incluir(DML.Cliente cliente);
        DML.Cliente Consultar(long Id);
        bool VerificarExistencia(string CPF);
        List<DML.Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd);
        List<DML.Cliente> Listar();
        void Alterar(DML.Cliente cliente);
        void Excluir(long Id);
    }
}
