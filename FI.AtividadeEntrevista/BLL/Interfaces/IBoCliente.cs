using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL.Interfaces
{
    /// <summary>
    /// Interface para a lógica de negócio de Cliente
    /// </summary>
    public interface IBoCliente
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        long Incluir(DML.Cliente cliente);

        /// <summary>
        /// Altera um cliente
        /// </summary>
        void Alterar(DML.Cliente cliente);

        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        DML.Cliente Consultar(long id);

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        void Excluir(long id);

        /// <summary>
        /// Lista os clientes
        /// </summary>
        List<DML.Cliente> Listar();

        /// <summary>
        /// Lista os clientes com paginação e ordenação
        /// </summary>
        List<DML.Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd);

        /// <summary>
        /// Verifica a existência de um CPF
        /// </summary>
        bool VerificarExistencia(string CPF);
    }
}
