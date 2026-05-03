using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace OOPFoundation
{
    /// <summary>
    /// Classe abstrata base para manipulação, sanitização e validação de textos.
    /// </summary>
    public abstract class AText : ISanitization, ITextValidation
    {
        private string _text;
        private string _validPattern;

        /// <summary>
        /// Construtor protegido. Inicializa text e validPattern como strings vazias.
        /// </summary>
        protected AText()
        {
            _text = string.Empty;
            _validPattern = string.Empty;
        }

        /// <summary>
        /// Retorna o texto armazenado.
        /// </summary>
        public string GetText() => _text;

        /// <summary>
        /// Define o texto e o padrão de validação/sanitização.
        /// </summary>
        /// <param name="text">Texto a armazenar.</param>
        /// <param name="validPattern">Padrão de caracteres permitidos (usado em regex [^padrão]).</param>
        protected void SetText(string text, string validPattern)
        {
            _validPattern = validPattern;
            _text = Sanitize(text);
        }

        /// <summary>
        /// Remove todos os caracteres que não estão no padrão válido.
        /// </summary>
        /// <param name="textToSanitize">Texto a sanitizar.</param>
        /// <returns>Texto sanitizado.</returns>
        public string Sanitize(string textToSanitize)
        {
            if (string.IsNullOrEmpty(textToSanitize)) return string.Empty;
            if (string.IsNullOrEmpty(_validPattern)) return textToSanitize;

            return Regex.Replace(textToSanitize, $"[^{_validPattern}]", string.Empty);
        }

        /// <summary>
        /// Verifica se o texto armazenado é válido (não vazio após sanitização).
        /// </summary>
        /// <param name="textToValidate">Texto a validar.</param>
        /// <returns>True se válido, false caso contrário.</returns>
        public bool TextIsValid(string textToValidate)
        {
            return !string.IsNullOrWhiteSpace(Sanitize(textToValidate));
        }

        /// <summary>
        /// Retorna o texto codificado em Base64 e depois hasheado (SHA-256).
        /// </summary>
        public string ObtainHashedText() => Hash();

        /// <summary>
        /// Gera o hash SHA-256 do texto codificado em Base64.
        /// </summary>
        private string Hash()
        {
            string encoded = Encode();
            using SHA256 sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(encoded));
            return Convert.ToHexString(bytes).ToLower();
        }

        /// <summary>
        /// Codifica o texto em Base64.
        /// </summary>
        private string Encode()
        {
            byte[] bytes = Encoding.UTF8.GetBytes(_text);
            return Convert.ToBase64String(bytes);
        }
    }
}
