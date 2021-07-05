using FluentValidation;
using SystemRD1.Domain.ValueObjects;

namespace SystemRD1.Domain.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(a => a.Street)
                .NotEmpty().WithMessage("O campo Rua não pode ser vazio")
                .MaximumLength(50).WithMessage("O campo Rua deve conter até 50 caracteres");

            RuleFor(a => a.Number)
                .NotEmpty().WithMessage("O campo Número não pode ser vazio")
                .MaximumLength(7).WithMessage("O campo Número deve conter até 7 caracteres");

            RuleFor(a => a.Neighborhood)
                .NotEmpty().WithMessage("O campo Bairro não pode ser vazio")
                .MaximumLength(50).WithMessage("O campo Bairro deve conter até 50 caracteres");

            RuleFor(a => a.ZipCode)
                .NotEmpty().WithMessage("O campo CEP não pode ser vazio")
                .Length(8).WithMessage("O campo CEP deve conter 8 caracteres");

            RuleFor(a => a.City)
                .NotEmpty().WithMessage("O campo Cidade não pode ser vazio")
                .MaximumLength(50).WithMessage("O campo Cidade deve conter até 50 caracteres");

            RuleFor(a => a.State)
                .NotEmpty().WithMessage("O campo Estado não pode ser vazio")
                .Length(2).WithMessage("O campo Estado deve conter 2 caracteres");
        }
    }
}
