using FluentValidation;
using SystemRD1.Domain.Entities;
using SystemRD1.Domain.Enums;
using SystemRD1.Domain.Validations.Documents;

namespace SystemRD1.Domain.Validations
{
    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("O campo Nome não pode ser vazio")
                .MaximumLength(50).WithMessage("O campo Nome deve conter até 50 caracteres");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("O campo Sobrenome não pode ser vazio")
                .MaximumLength(50).WithMessage("O campo Sobrenome deve conter até 50 caracteres");

            RuleFor(c => c.Gender)
                .NotEmpty().WithMessage("O campo Gênero não pode ser vazio")
                .Length(1).WithMessage("O campo Gênero deve conter somente 1 caracter (M ou F)");

            When(c => c.DocumentType == EDocumentType.Cpf, () =>
            {
                RuleFor(c => c.Document.Length)
                  .Equal(CpfValidation.CpfSize).WithMessage("O campo Documento (CPF) deve conter 11 caracteres");
                RuleFor(c => CpfValidation.Validate(c.Document))
                   .Equal(true).WithMessage("O campo Documento (CPF) não é válido");
            });

            When(c => c.DocumentType == EDocumentType.Cnpj, () =>
            {
                RuleFor(c => c.Document.Length)
                    .Equal(CnpjValidation.CnpjSize).WithMessage("O campo Documento (CNPJ) deve conter 14 caracteres");
                RuleFor(c => CnpjValidation.Validate(c.Document))
                    .Equal(true).WithMessage("O campo Documento (CNPJ) não é válido");
            });

        }
    }
}
