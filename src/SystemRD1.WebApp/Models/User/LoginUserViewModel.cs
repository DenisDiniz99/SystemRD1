using System.ComponentModel.DataAnnotations;

namespace SystemRD1.WebApp.Models.User
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 5)]
        [EmailAddress(ErrorMessage = "O campo {0} não é válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(16, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 4)]
        [DataType(DataType.Password, ErrorMessage = "O campo {0} não é válido")]
        public string Password { get; set; }
    }
}
