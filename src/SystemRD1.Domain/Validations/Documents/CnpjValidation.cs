using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemRD1.Domain.Validations.Documents
{
    public static class CnpjValidation
    {
        public const int CnpjSize = 14;

        public static bool Validate(string cpnj)
        {
            var cnpjNumeros = Utils.OnlyNumber(cpnj);

            if (!ValidSize(cnpjNumeros)) return false;
            return !RepeatedDigits(cnpjNumeros) && ValidDigits(cnpjNumeros);
        }

        private static bool ValidSize(string valor)
        {
            return valor.Length == CnpjSize;
        }

        private static bool RepeatedDigits(string valor)
        {
            string[] invalidNumbers =
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };
            return invalidNumbers.Contains(valor);
        }

        private static bool ValidDigits(string valor)
        {
            var number = valor.Substring(0, CnpjSize - 2);

            var digitoVerificador = new VerifyingDigit(number)
                .ComMultiplicadoresDeAte(2, 9)
                .Substituindo("0", 10, 11);
            var firstDigit = digitoVerificador.CalculaDigito();
            digitoVerificador.AddDigito(firstDigit);
            var secondDigit = digitoVerificador.CalculaDigito();

            return string.Concat(firstDigit, secondDigit) == valor.Substring(CnpjSize - 2, 2);
        }
    }
}
