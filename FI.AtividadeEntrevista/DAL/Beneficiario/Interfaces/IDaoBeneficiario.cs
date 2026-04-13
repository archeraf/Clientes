using System.Collections.Generic;

namespace FI.AtividadeEntrevista.DAL.Interfaces
{
    /// <summary>
    /// Interface para acesso a dados de Beneficiário
    /// </summary>
    public interface IDaoBeneficiario
    {
        long Incluir(DML.Beneficiario beneficiario);
        void Alterar(DML.Beneficiario beneficiario);
        List<DML.Beneficiario> Listar(long idCliente);
        void Excluir(long id);
        bool VerificarExistencia(string CPF, long idCliente);
    }
}
