using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL.Interfaces
{
    /// <summary>
    /// Interface para a lógica de negócio de Beneficiário
    /// </summary>
    public interface IBoBeneficiario
    {
        /// <summary>
        /// Inclui um novo beneficiário
        /// </summary>
        long Incluir(DML.Beneficiario beneficiario);

        /// <summary>
        /// Altera um beneficiário
        /// </summary>
        void Alterar(DML.Beneficiario beneficiario);

        /// <summary>
        /// Lista os beneficiários por cliente
        /// </summary>
        List<DML.Beneficiario> Listar(long idCliente);

        /// <summary>
        /// Excluir o beneficiário pelo id
        /// </summary>
        void Excluir(long id);

        /// <summary>
        /// Verifica a existência de um CPF para um determinado cliente
        /// </summary>
        bool VerificarExistencia(string CPF, long idCliente);
    }
}
