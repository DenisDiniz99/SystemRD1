using System.ComponentModel.DataAnnotations;

namespace SystemRD1.Api.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 5)]
        [EmailAddress(ErrorMessage = "O campo {0} não é válido")]
        public string Email { get; set; }
    }
}
