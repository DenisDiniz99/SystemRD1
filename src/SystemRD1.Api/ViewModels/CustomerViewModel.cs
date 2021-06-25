using System;
using System.ComponentModel.DataAnnotations;

namespace SystemRD1.Api.ViewModels
{
    public class CustomerViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DataType(DataType.Date, ErrorMessage = "O campo {0} não está em um formato válido")]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1, ErrorMessage = "O campo {0} deve conter {1} caracter")]
        public string Gender { get; set; }

        public int DocumentType { get; set; }

        
        public string Document { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "O campo {0} está em um formato inválido")]
        public string Phone { get; set; }

        [DataType(DataType.Date, ErrorMessage = "O campo {0} está em um formato inválido")]
        public DateTime Birthday { get; set; }

        public AddressViewModel Address { get; set; }
      
        public bool Active { get; set; }
    }
}
