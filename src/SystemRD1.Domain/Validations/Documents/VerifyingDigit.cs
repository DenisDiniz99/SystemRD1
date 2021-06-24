using System;
using System.Collections.Generic;
using System.Text;

namespace SystemRD1.Domain.Validations.Documents
{
    public class VerifyingDigit
    {
        private string _number;
        private const int Module = 11;
        private readonly List<int> _multipliers = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly IDictionary<int, string> _replacements = new Dictionary<int, string>();
        private bool _complementModule = true;

        public VerifyingDigit(string number)
        {
            _number = number;
        }

        public VerifyingDigit ComMultiplicadoresDeAte(int primeiroMultiplicador, int ultimoMultiplicador)
        {
            _multipliers.Clear();
            for (var i = primeiroMultiplicador; i <= ultimoMultiplicador; i++)
                _multipliers.Add(i);

            return this;
        }

        public VerifyingDigit Substituindo(string substituto, params int[] digitos)
        {
            foreach (var i in digitos)
            {
                _replacements[i] = substituto;
            }
            return this;
        }

        public void AddDigito(string digito)
        {
            _number = string.Concat(_number, digito);
        }

        public string CalculaDigito()
        {
            return !(_number.Length > 0) ? "" : GetDigitSum();
        }

        private string GetDigitSum()
        {
            var soma = 0;
            for (int i = _number.Length - 1, m = 0; i >= 0; i--)
            {
                var produto = (int)char.GetNumericValue(_number[i]) * _multipliers[m];
                soma += produto;

                if (++m >= _multipliers.Count) m = 0;
            }

            var mod = (soma % Module);
            var resultado = _complementModule ? Module - mod : mod;

            return _replacements.ContainsKey(resultado) ? _replacements[resultado] : resultado.ToString();
        }
    }
}
