using System.ComponentModel.DataAnnotations;

namespace SystemRD1.Api.ViewModels
{
    public class ResetPasswordUserViewModel
    {

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(16, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 4)]
        [DataType(DataType.Password, ErrorMessage = "O campo {0} não é válido")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(16, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 4)]
        [DataType(DataType.Password, ErrorMessage = "O campo {0} não é válido")]
        [Compare("NewPassword", ErrorMessage = "As senhas não conferem")]
        public string ConfirmNewPassword { get; set; }
    }
}
