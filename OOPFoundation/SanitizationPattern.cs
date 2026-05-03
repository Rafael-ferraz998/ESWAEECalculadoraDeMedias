namespace OOPFoundation
{
    /// <summary>
    /// Padrões de sanitização para diferentes tipos de campo.
    /// Cada constante define os caracteres permitidos para aquele tipo.
    /// </summary>
    public class SanitizationPattern
    {
        /// <summary>Padrão para CNPJ: letras e números.</summary>
        public const string CNPJ = "a-zA-Z0-9";

        /// <summary>Padrão para CPF: apenas dígitos.</summary>
        public const string CPF = "0-9";

        /// <summary>Padrão para ISBN: apenas dígitos.</summary>
        public const string ISBN = "0-9";

        /// <summary>Padrão para ISSN: dígitos e X (maiúsculo e minúsculo).</summary>
        public const string ISSN = "0-9Xx";

        /// <summary>Padrão para PHONE: apenas dígitos.</summary>
        public const string PHONE = "0-9";

        /// <summary>Padrão para RG: dígitos e X (maiúsculo e minúsculo).</summary>
        public const string RG = "0-9Xx";

        /// <summary>Padrão para notas (dígitos e vírgula).</summary>
        public const string NOTE = @"0-9,";
    }
}
