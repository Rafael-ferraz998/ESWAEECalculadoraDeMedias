namespace OOPFoundation
{
    /// <summary>
    /// Interface para sanitização de strings.
    /// </summary>
    public interface ISanitization
    {
        /// <summary>
        /// Remove caracteres inválidos da string, mantendo apenas os permitidos.
        /// </summary>
        /// <param name="textToSanitize">Texto a ser sanitizado.</param>
        /// <returns>Texto sanitizado.</returns>
        string Sanitize(string textToSanitize);
    }
}
