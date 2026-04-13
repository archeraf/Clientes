using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo de Beneficiário
    /// </summary>
    public class BeneficiarioModel
    {
        private string _cpf;

        public long Id { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [Required]
        [CpfValidation(ErrorMessage = "CPF inválido")]
        public string CPF
        {
            get
            {
                return _cpf;
            }
            set
            {
                _cpf = value?.Replace(".", "").Replace("-", "").ToString();
            }
        }

        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// IdCliente
        /// </summary>
        public long IdCliente { get; set; }
    }
}
