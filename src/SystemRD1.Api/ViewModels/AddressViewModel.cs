using System.ComponentModel.DataAnnotations;

namespace SystemRD1.Api.ViewModels
{
    public class AddressViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
        [Display(Name = "Rua", Prompt = "Informe o nome da Rua", Description = "Informe o nome da Rua")]
        public string Street { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(7, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 1)]
        [Display(Name = "Número", Prompt = "Informe o número da residência", Description = "Informe o número da residência")]
        public string Number { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
        [Display(Name = "Bairro", Prompt = "Informe o nome do Bairro", Description = "Informe o nome do Bairro")]
        public string Neighborhood { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(9, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres")]
        [Display(Name = "CEP", Prompt = "Informe o CEP", Description = "Informe o CEP")]
        public string ZipCode { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
        [Display(Name = "Cidade", Prompt = "Informe o nome da Cidade", Description = "Informe o nome da Cidade")]
        public string City { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(2, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres")]
        [Display(Name = "Estado", Prompt = "Informe a sigla do Estado", Description = "Informe a sigla do Estado")]
        public string State { get; set; }
    }
}
