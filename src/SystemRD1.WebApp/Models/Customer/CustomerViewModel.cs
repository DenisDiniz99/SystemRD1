using System;
using System.ComponentModel.DataAnnotations;

namespace SystemRD1.WebApp.Models.Customer
{
    public class CustomerViewModel
    {
        [Key]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
        [Display(Name = "Nome", Prompt = "Informe o Nome", Description = "Informe o primeiro Nome")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
        [Display(Name = "Sobrenome", Prompt = "Informe o Sobrenome", Description = "Informe o Sobrenome")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1, ErrorMessage = "O campo {0} deve conter {1} caracter")]
        [Display(Name = "Gênero", Prompt = "Informe o Gênero", Description = "Informe o Gênero")]
        public string Gender { get; set; }


        [Display(Name = "Tipo Documento", Prompt = "Informe o Tipo do Documento", Description = "Informe o Tipo do Documento CPF ou CNPJ")]
        public int DocumentType { get; set; }


        [Display(Name = "Documento", Prompt = "Informe o número do Documento", Description = "Informe o número do Documento CPF ou CNPJ")]
        public string Document { get; set; }


        [DataType(DataType.PhoneNumber, ErrorMessage = "O campo {0} está em um formato inválido")]
        [Display(Name = "Telefone", Prompt = "Informe o número do Telefone", Description = "Informe o número do Telefone")]
        public string Phone { get; set; }


        [DataType(DataType.Date, ErrorMessage = "O campo {0} está em um formato inválido")]
        [Display(Name = "Data de Nascimento", Prompt = "Informe Data de Nascimento", Description = "Informe Data de Nascimento")]
        public DateTime Birthday { get; set; }

        public AddressViewModel Address { get; set; }

        [Display(Name = "Ativo?", Prompt = "Cliente Ativo?", Description = "Informe se o Cliente está ou não Ativo")]
        public bool Active { get; set; }
    }
}
