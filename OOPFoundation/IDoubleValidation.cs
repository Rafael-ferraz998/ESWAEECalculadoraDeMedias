namespace OOPFoundation
{
    /// <summary>
    /// Interface para validação de valores double.
    /// </summary>
    public interface IDoubleValidation
    {
        /// <summary>
        /// Verifica se o valor double informado é válido.
        /// </summary>
        /// <param name="doubleToValidate">Valor a ser validado.</param>
        /// <returns>True se válido, false caso contrário.</returns>
        bool DoubleIsValid(double doubleToValidate);
    }
}
