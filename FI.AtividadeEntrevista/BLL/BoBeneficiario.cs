using System.Collections.Generic;
using FI.AtividadeEntrevista.BLL.Interfaces;
using FI.AtividadeEntrevista.DAL.Interfaces;
namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario : IBoBeneficiario
    {
        private readonly IDaoBeneficiario _dao;

        public BoBeneficiario(IDaoBeneficiario dao)
        {
            _dao = dao;
        }

        /// <summary>
        /// Inclui um novo beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiário</param>
        public long Incluir(DML.Beneficiario beneficiario)
        {
            return _dao.Incluir(beneficiario);
        }

        /// <summary>
        /// Altera um beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiário</param>
        public void Alterar(DML.Beneficiario beneficiario)
        {
            _dao.Alterar(beneficiario);
        }

        /// <summary>
        /// Lista os beneficiários por cliente
        /// </summary>
        /// <param name="idCliente">Id do Cliente</param>
        public List<DML.Beneficiario> Listar(long idCliente)
        {
            return _dao.Listar(idCliente);
        }

        /// <summary>
        /// Excluir o beneficiário pelo id
        /// </summary>
        /// <param name="id">id do beneficiário</param>
        public void Excluir(long id)
        {
            _dao.Excluir(id);
        }

        /// <summary>
        /// Verifica a existência de um CPF para um beneficiário de um cliente
        /// </summary>
        /// <param name="CPF"></param>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF, long idCliente)
        {
            return _dao.VerificarExistencia(CPF, idCliente);
        }
    }
}
