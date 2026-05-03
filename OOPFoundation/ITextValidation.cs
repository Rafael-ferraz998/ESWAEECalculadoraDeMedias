namespace OOPFoundation
{
    /// <summary>
    /// Interface para validação de strings.
    /// </summary>
    public interface ITextValidation
    {
        /// <summary>
        /// Verifica se o texto informado é válido.
        /// </summary>
        /// <param name="textToValidate">Texto a ser validado.</param>
        /// <returns>True se válido, false caso contrário.</returns>
        bool TextIsValid(string textToValidate);
    }
}
