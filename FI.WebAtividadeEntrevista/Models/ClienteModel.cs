using System.ComponentModel.DataAnnotations;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo de Cliente
    /// </summary>
    public class ClienteModel
    {
        private string _cpf;
        public long Id { get; set; }

        /// <summary>
        /// CEP
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "CEP inválido")]
        public string CEP { get; set; }

        /// <summary>
        /// Cidade
        /// </summary>
        [Required]
        public string Cidade { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Digite um e-mail válido")]
        public string Email { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        [Required]
        [MaxLength(2)]
        public string Estado { get; set; }

        /// <summary>
        /// Logradouro
        /// </summary>
        [Required]
        public string Logradouro { get; set; }

        /// <summary>
        /// Nacionalidade
        /// </summary>
        [Required]
        public string Nacionalidade { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// Sobrenome
        /// </summary>
        [Required]
        public string Sobrenome { get; set; }

        /// <summary>
        /// Telefone
        /// </summary>
        [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$", ErrorMessage = "Telefone inválido")]
        public string Telefone { get; set; }

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
        /// Beneficiários
        /// </summary>
        public System.Collections.Generic.List<BeneficiarioModel> Beneficiarios { get; set; }

    }
}