using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemRD1.Domain.Validations.Documents
{
    public static class CpfValidation
    {
        public const int CpfSize = 11;

        public static bool Validate(string cpf)
        {
            var cpfNumeros = Utils.OnlyNumber(cpf);

            if (!ValidSize(cpfNumeros)) return false;
            return !RepeatedDigits(cpfNumeros) && ValidDigits(cpfNumeros);
        }

        private static bool ValidSize(string valor)
        {
            return valor.Length == CpfSize;
        }

        private static bool RepeatedDigits(string valor)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };
            return invalidNumbers.Contains(valor);
        }

        private static bool ValidDigits(string valor)
        {
            var number = valor.Substring(0, CpfSize - 2);
            var digitoVerificador = new VerifyingDigit(number)
                .ComMultiplicadoresDeAte(2, 11)
                .Substituindo("0", 10, 11);
            var firstDigit = digitoVerificador.CalculaDigito();
            digitoVerificador.AddDigito(firstDigit);
            var secondDigit = digitoVerificador.CalculaDigito();

            return string.Concat(firstDigit, secondDigit) == valor.Substring(CpfSize - 2, 2);
        }
    }
}
