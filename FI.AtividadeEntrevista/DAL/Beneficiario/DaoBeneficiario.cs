using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.DAL.Interfaces;

namespace FI.AtividadeEntrevista.DAL
{
    internal class DaoBeneficiario : AcessoDados, IDaoBeneficiario
    {
        public long Incluir(Beneficiario beneficiario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("Nome", beneficiario.Nome));
            parametros.Add(new SqlParameter("CPF", beneficiario.CPF));
            parametros.Add(new SqlParameter("IdCliente", beneficiario.IdCliente));

            DataSet ds = base.Consultar("FI_SP_IncBeneficiario", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        public void Alterar(Beneficiario beneficiario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("Nome", beneficiario.Nome));
            parametros.Add(new SqlParameter("CPF", beneficiario.CPF));
            parametros.Add(new SqlParameter("ID", beneficiario.Id));

            base.Executar("FI_SP_AltBeneficiario", parametros);
        }

        public List<Beneficiario> Listar(long idCliente)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("IdCliente", idCliente));

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiario", parametros);
            List<Beneficiario> lista = Converter(ds);

            return lista;
        }

        public void Excluir(long id)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("Id", id));

            base.Executar("FI_SP_DelBeneficiario", parametros);
        }

        public bool VerificarExistencia(string CPF, long idCliente)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("CPF", CPF));
            parametros.Add(new SqlParameter("IdCliente", idCliente));

            DataSet ds = base.Consultar("FI_SP_VerificaBeneficiario", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        private List<Beneficiario> Converter(DataSet ds)
        {
            List<Beneficiario> lista = new List<Beneficiario>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Beneficiario benef = new Beneficiario();
                    benef.Id = row.Field<long>("Id");
                    benef.CPF = row.Field<string>("CPF");
                    benef.Nome = row.Field<string>("Nome");
                    benef.IdCliente = row.Field<long>("IdCliente");
                    lista.Add(benef);
                }
            }
            return lista;
        }
    }
}
